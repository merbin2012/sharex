﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2008-2013 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

#region License Information (MIT)

/*
The MIT License (MIT)

Copyright (c) 2013 Gregoire Pailler

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CG.Web.MegaApiClient
{
    public class MegaApiClient
    {
        private readonly IWebClient _webClient;

        private const uint BufferSize = 8192;
        private const int ApiRequestAttempts = 10;
        private const int ApiRequestDelay = 200;

        private static readonly Uri BaseApiUri = new Uri("https://g.api.mega.co.nz/cs");
        private static readonly Uri BaseUri = new Uri("https://mega.co.nz");

        private Node _trashNode;

        private string _sessionId;
        private byte[] _masterKey;
        private uint _sequenceIndex = (uint)(uint.MaxValue * new Random().NextDouble());

        #region Constructors
        
        internal MegaApiClient(IWebClient webClient)
        {
            if (webClient == null)
            {
                throw new ArgumentNullException("webClient");
            }

            this._webClient = webClient;
        }

        #endregion

        #region Public API

        public void Login(string email, string password)
        {
            this.Login(GenerateAuthInfos(email, password));
        }

        public static AuthInfos GenerateAuthInfos(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            // Retrieve password as UTF8 byte array
            byte[] passwordBytes = password.ToBytes();

            // Encrypt password to use password as key for the hash
            byte[] passwordAesKey = PrepareKey(passwordBytes);

            // Hash email and password to decrypt master key on Mega servers
            string hash = GenerateHash(email.ToLowerInvariant(), passwordAesKey);

            return new AuthInfos(email, hash, passwordAesKey);
        }

        public void Login(AuthInfos authInfos)
        {
            if (authInfos == null)
            {
                throw new ArgumentNullException("authInfos");
            }

            this.EnsureLoggedOut();

            // Request Mega Api
            LoginRequest request = new LoginRequest(authInfos.Email, authInfos.Hash);
            LoginResponse response = this.Request<LoginResponse>(request);

            // Decrypt master key using our password key
            byte[] cryptedMasterKey = response.MasterKey.FromBase64();
            this._masterKey = Crypto.DecryptKey(cryptedMasterKey, authInfos.PasswordAesKey);

            // Decrypt RSA private key using decrypted master key
            byte[] cryptedRsaPrivateKey = response.PrivateKey.FromBase64();
            BigInteger[] rsaPrivateKeyComponents = Crypto.GetRsaPrivateKeyComponents(cryptedRsaPrivateKey, this._masterKey);

            // Decrypt session id
            byte[] encryptedSid = response.SessionId.FromBase64();
            byte[] sid = Crypto.RsaDecrypt(encryptedSid.FromMPINumber(), rsaPrivateKeyComponents[0], rsaPrivateKeyComponents[1], rsaPrivateKeyComponents[2]);

            // Session id contains only the first 43 decrypted bytes
            this._sessionId = sid.CopySubArray(43).ToBase64();
        }

        public void LoginAnonymous()
        {
            this.EnsureLoggedOut();

            Random random = new Random();

            // Generate random master key
            this._masterKey = new byte[16];
            random.NextBytes(this._masterKey);

            // Generate a random password used to encrypt the master key
            byte[] passwordAesKey = new byte[16];
            random.NextBytes(passwordAesKey);

            // Generate a random session challenge
            byte[] sessionChallenge = new byte[16];
            random.NextBytes(sessionChallenge);

            byte[] encryptedMasterKey = Crypto.EncryptAes(this._masterKey, passwordAesKey);

            // Encrypt the session challenge with our generated master key
            byte[] encryptedSessionChallenge = Crypto.EncryptAes(sessionChallenge, this._masterKey);
            byte[] encryptedSession = new byte[32];
            Array.Copy(sessionChallenge, 0, encryptedSession, 0, 16);
            Array.Copy(encryptedSessionChallenge, 0, encryptedSession, 16, encryptedSessionChallenge.Length);

            // Request Mega Api to obtain a temporary user handle
            AnonymousLoginRequest request = new AnonymousLoginRequest(encryptedMasterKey.ToBase64(), encryptedSession.ToBase64());
            string userHandle = this.Request(request);

            // Request Mega Api to retrieve our temporary session id
            LoginRequest request2 = new LoginRequest(userHandle, null);
            LoginResponse response2 = this.Request<LoginResponse>(request2);

            this._sessionId = response2.TemporarySessionId;
        }

        public void Logout()
        {
            this.EnsureLoggedIn();

            // Reset values retrieved by Login methods
            this._masterKey = null;
            this._sessionId = null;
        }

        public IEnumerable<Node> GetNodes()
        {
            this.EnsureLoggedIn();

            GetNodesRequest request = new GetNodesRequest();
            GetNodesResponse response = this.Request<GetNodesResponse>(request, this._masterKey);

            Node[] nodes = response.Nodes;
            if (this._trashNode == null)
            {
                this._trashNode = nodes.First(n => n.Type == NodeType.Trash);
            }

            return nodes;
        }

        public void Delete(Node node, bool moveToTrash = true)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.Type != NodeType.Directory && node.Type != NodeType.File)
            {
                throw new ArgumentException("Invalid node type");
            }

            this.EnsureLoggedIn();

            if (moveToTrash)
            {
                this.Move(node, this._trashNode);
            }
            else
            {
                this.Request(new DeleteRequest(node));
            }
        }

        public Node CreateFolder(string name, Node parent)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (parent.Type == NodeType.File)
            {
                throw new ArgumentException("Invalid parent node");
            }

            this.EnsureLoggedIn();

            byte[] key = Crypto.CreateAesKey();
            byte[] attributes = Crypto.EncryptAttributes(new Attributes(name), key);
            byte[] encryptedKey = Crypto.EncryptAes(key, this._masterKey);

            CreateNodeRequest request = CreateNodeRequest.CreateFolderNodeRequest(parent, attributes.ToBase64(), encryptedKey.ToBase64());
            GetNodesResponse response = this.Request<GetNodesResponse>(request, this._masterKey);
            return response.Nodes[0];
        }

        public Uri GetDownloadLink(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.Type != NodeType.File)
            {
                throw new ArgumentException("Invalid node");
            }

            this.EnsureLoggedIn();

            GetDownloadLinkRequest request = new GetDownloadLinkRequest(node);
            string response = this.Request<string>(request);

            return new Uri(BaseUri, string.Format("/#!{0}!{1}", response, node.DecryptedFileKey.ToBase64()));
        }

        public void DownloadFile(Node node, string outputFile)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (string.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentNullException("outputFile");
            }

            using (Stream stream = this.Download(node))
            {
                using (FileStream fs = new FileStream(outputFile, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] buffer = new byte[BufferSize];
                    int len;
                    while ((len = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, len);
                    }
                }
            }
        }

        public Stream Download(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.Type != NodeType.File)
            {
                throw new ArgumentException("Invalid node");
            }

            this.EnsureLoggedIn();

            // Retrieve download URL
            DownloadUrlRequest downloadRequest = new DownloadUrlRequest(node);
            DownloadUrlResponse downloadResponse = this.Request<DownloadUrlResponse>(downloadRequest);

            Stream dataStream = this._webClient.GetRequestRaw(new Uri(downloadResponse.Url));
            return new MegaAesCtrStreamDecrypter(dataStream, downloadResponse.Size, node.Key, node.Iv, node.MetaMac);
        }

        public Node Upload(string filename, Node parent)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            this.EnsureLoggedIn();

            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return this.Upload(fileStream, Path.GetFileName(filename), parent);
            }
        }

        public Node Upload(Stream stream, string name, Node parent)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (parent.Type == NodeType.File)
            {
                throw new ArgumentException("Invalid parent node");
            }

            this.EnsureLoggedIn();

            // Retrieve upload URL
            UploadUrlRequest uploadRequest = new UploadUrlRequest(stream.Length);
            UploadUrlResponse uploadResponse = this.Request<UploadUrlResponse>(uploadRequest);

            using (MegaAesCtrStreamCrypter encryptedStream = new MegaAesCtrStreamCrypter(stream))
            {
                string completionHandle = this._webClient.PostRequestRaw(new Uri(uploadResponse.Url), encryptedStream);

                // Encrypt attributes
                byte[] cryptedAttributes = Crypto.EncryptAttributes(new Attributes(name), encryptedStream.FileKey);

                // Compute the file key
                byte[] fileKey = new byte[32];
                for (int i = 0; i < 8; i++)
                {
                    fileKey[i] = (byte)(encryptedStream.FileKey[i] ^ encryptedStream.Iv[i]);
                    fileKey[i + 16] = encryptedStream.Iv[i];
                }

                for (int i = 8; i < 16; i++)
                {
                    fileKey[i] = (byte)(encryptedStream.FileKey[i] ^ encryptedStream.MetaMac[i - 8]);
                    fileKey[i + 16] = encryptedStream.MetaMac[i - 8];
                }

                byte[] encryptedKey = Crypto.EncryptKey(fileKey, this._masterKey);

                CreateNodeRequest createNodeRequest = CreateNodeRequest.CreateFileNodeRequest(parent, cryptedAttributes.ToBase64(), encryptedKey.ToBase64(), completionHandle);
                GetNodesResponse createNodeResponse = this.Request<GetNodesResponse>(createNodeRequest, this._masterKey);
                return createNodeResponse.Nodes[0];
            }
        }

        public Node Move(Node node, Node destinationParentNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (destinationParentNode == null)
            {
                throw new ArgumentNullException("destinationParentNode");
            }

            if (node.Type != NodeType.Directory && node.Type != NodeType.File)
            {
                throw new ArgumentException("Invalid node type");
            }

            if (destinationParentNode.Type == NodeType.File)
            {
                throw new ArgumentException("Invalid destination parent node");
            }

            this.Request(new MoveRequest(node, destinationParentNode));
            return this.GetNodes().First(n => n.Equals(node));
        }

        #endregion

        #region Web

        private string Request(RequestBase request)
        {
            return this.Request<string>(request);
        }

        private TResponse Request<TResponse>(RequestBase request, object context = null)
            where TResponse : class
        {
            string dataRequest = JsonConvert.SerializeObject(new object[] { request });
            Uri uri = this.GenerateUrl();
            object jsonData = null;
            int currentAttempt = 0;
            while (true)
            {
                string dataResult = this._webClient.PostRequestJson(uri, dataRequest);

                jsonData = JsonConvert.DeserializeObject(dataResult);
                if (jsonData is long || (jsonData is JArray && ((JArray)jsonData)[0].Type == JTokenType.Integer))
                {
                    ApiResultCode apiCode = (jsonData is long)
                                                ? (ApiResultCode)Enum.ToObject(typeof(ApiResultCode), jsonData)
                                                : (ApiResultCode)((JArray)jsonData)[0].Value<int>();

                    if (apiCode == ApiResultCode.RequestFailedRetry)
                    {
                        if (currentAttempt == ApiRequestAttempts)
                        {
                            throw new NotSupportedException("Api not available");
                        }

                        Thread.Sleep(ApiRequestDelay);
                        currentAttempt++;
                        continue;
                    }

                    if (apiCode != ApiResultCode.Ok)
                    {
                        throw new ApiException(apiCode);
                    }
                }

                break;
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Context = new StreamingContext(StreamingContextStates.All, context);

            string data = ((JArray)jsonData)[0].ToString();
            return (typeof(TResponse) == typeof(string)) ? data as TResponse : JsonConvert.DeserializeObject<TResponse>(data, settings);
        }

        private Uri GenerateUrl()
        {
            UriBuilder builder = new UriBuilder(BaseApiUri);
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["id"] = (_sequenceIndex++ % uint.MaxValue).ToString(CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(this._sessionId))
            {
                query["sid"] = this._sessionId;
            }

            builder.Query = query.ToString();
            return builder.Uri;
        }

        #endregion

        #region Private methods

        private static string GenerateHash(string email, byte[] passwordAesKey)
        {
            byte[] emailBytes = email.ToBytes();
            byte[] hash = new byte[16];

            // Compute email in 16 bytes array
            for (int i = 0; i < emailBytes.Length; i++)
            {
                hash[i % 16] ^= emailBytes[i];
            }

            // Encrypt hash using password key
            for (int it = 0; it < 16384; it++)
            {
                hash = Crypto.EncryptAes(hash, passwordAesKey);
            }

            // Retrieve bytes 0-4 and 8-12 from the hash
            byte[] result = new byte[8];
            Array.Copy(hash, 0, result, 0, 4);
            Array.Copy(hash, 8, result, 4, 4);

            return result.ToBase64();
        }

        private static byte[] PrepareKey(byte[] data)
        {
            byte[] pkey = new byte[] { 0x93, 0xC4, 0x67, 0xE3, 0x7D, 0xB0, 0xC7, 0xA4, 0xD1, 0xBE, 0x3F, 0x81, 0x01, 0x52, 0xCB, 0x56 };

            for (int it = 0; it < 65536; it++)
            {
                for (int idx = 0; idx < data.Length; idx += 16)
                {
                    // Pad the data to 16 bytes blocks
                    byte[] key = data.CopySubArray(16, idx);

                    pkey = Crypto.EncryptAes(pkey, key);
                }
            }

            return pkey;
        }

        private void EnsureLoggedIn()
        {
            if (this._sessionId == null)
            {
                throw new NotSupportedException("Not logged in");
            }
        }

        private void EnsureLoggedOut()
        {
            if (this._sessionId != null)
            {
                throw new NotSupportedException("Already logged in");
            }
        }

        #endregion

        #region AuthInfos

        public class AuthInfos
        {
            public AuthInfos(string email, string hash, byte[] passwordAesKey)
            {
                this.Email = email;
                this.Hash = hash;
                this.PasswordAesKey = passwordAesKey;
            }

            [JsonProperty]
            public string Email { get; private set; }

            [JsonProperty]
            public string Hash { get; private set; }

            [JsonProperty]
            public byte[] PasswordAesKey { get; private set; }
        }

        #endregion
    }
}