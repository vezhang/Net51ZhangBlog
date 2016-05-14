using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net51Zhang.Common.Encryption
{
    public class EOREncryptionProvider : BaseEncryptionProvider
    {
        private string _key;
        public EOREncryptionProvider(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            this._key = key;
        }

        public override void GenerateKeyIV()
        {
            this._keyBytes = this.Encoding.GetBytes(this._key);
            this._IVBytes = this.Encoding.GetBytes(this._key);
        }

        protected override byte[] EncryptImpl(byte[] dataBytes)
        {
            int dataLength = dataBytes.Length;
            int keyLength = this.Key.Length;

            for (var i = 0; i < dataLength; i++)
            {
                if (i < keyLength)
                {
                    dataBytes[i] ^= this.Key[i];
                }
                else
                {
                    dataBytes[i] ^= this.Key[keyLength - 1];
                }
            }

            return dataBytes;
        }

        protected override byte[] DecryptImpl(byte[] dataBytes)
        {
            int dataLength = dataBytes.Length;
            int IVLength = this.IV.Length;

            for (var i = 0; i < dataLength; i++)
            {
                if (i < IVLength)
                {
                    dataBytes[i] ^= this.IV[i];
                }
                else
                {
                    dataBytes[i] ^= this.IV[IVLength - 1];
                }
            }

            return dataBytes;
        }
    }
}