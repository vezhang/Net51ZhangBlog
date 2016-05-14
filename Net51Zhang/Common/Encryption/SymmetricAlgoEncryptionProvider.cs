using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Net51Zhang.Common.Encryption
{
    public class SymmetricAlgoEncryptionProvider : BaseEncryptionProvider
    {
        private SymmetricAlgorithm _symmetricAlgorithm;
        public SymmetricAlgoEncryptionProvider(SymmetricAlgorithm providerImpl)
        {
            if (providerImpl == null)
            {
                throw new ArgumentNullException("providerImpl");
            }

            this._symmetricAlgorithm = providerImpl;
            this._symmetricAlgorithm.Padding = PaddingMode.ISO10126;
        }

        protected override byte[] EncryptImpl(byte[] bytes)
        {
            byte[] encryptedData;

            using (var input = new MemoryStream(bytes))
            using (var output = new MemoryStream())
            {
                var encryptor = this._symmetricAlgorithm.CreateEncryptor(this.Key, this.IV);

                using (var cryptStream = new CryptoStream(output, encryptor, CryptoStreamMode.Write))
                {
                    var buffer = new byte[1024];
                    var read = input.Read(buffer, 0, buffer.Length);
                    while (read > 0)
                    {
                        cryptStream.Write(buffer, 0, read);
                        read = input.Read(buffer, 0, buffer.Length);
                    }
                    cryptStream.FlushFinalBlock();
                    encryptedData = output.ToArray();
                }
            }

            return encryptedData;
        }

        protected override byte[] DecryptImpl(byte[] bytes)
        {
            byte[] result;
            using (var input = new MemoryStream(bytes))
            using (var output = new MemoryStream())
            {
                var decryptor = this._symmetricAlgorithm.CreateDecryptor(this.Key, this.IV);
                using (var cryptStream = new CryptoStream(input, decryptor, CryptoStreamMode.Read))
                {
                    var buffer = new byte[1024];
                    var read = cryptStream.Read(buffer, 0, buffer.Length);
                    while (read > 0)
                    {
                        output.Write(buffer, 0, read);
                        read = cryptStream.Read(buffer, 0, buffer.Length);
                    }
                    cryptStream.Flush();
                    result = output.ToArray();
                }
            }

            return result;
        }

        public override void GenerateKeyIV()
        {
            this._symmetricAlgorithm.GenerateKey();
            this._symmetricAlgorithm.GenerateIV();

            this._keyBytes = this._symmetricAlgorithm.Key;
            this._IVBytes = this._symmetricAlgorithm.IV;
        }
    }
}