using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Net51Zhang.Common.Encryption
{
    public abstract class BaseEncryptionProvider : IEncryptionProvider
    {
        protected byte[] _keyBytes;
        protected byte[] _IVBytes;

        public byte[] Key
        {
            get
            {
                if (this._keyBytes == null)
                {
                    this.GenerateKeyIV();
                }

                return this._keyBytes;
            }
        }

        public byte[] IV
        {
            get
            {
                if (this._IVBytes == null)
                {
                    this.GenerateKeyIV();
                }

                return this._IVBytes;
            }
        }

        private Encoding _encoding;
        public Encoding Encoding
        {
            get { return this._encoding ?? Encoding.UTF8; }
            set { this._encoding = value; }
        }

        public string Encrypt(string data)
        {
             if (string.IsNullOrEmpty(data))
             {
                 throw new ArgumentNullException("data");
             }

            byte[] bytes = this.Encoding.GetBytes(data);

            var encodedBytes = this.EncryptImpl(bytes);

            return this.PostEncrypt(encodedBytes);
        }

        public string Decrypt(string encodeData)
        {
            if (string.IsNullOrEmpty(encodeData))
            {
                throw new ArgumentNullException("encodeData");
            }

            var bytes = this.PreDecrypt(encodeData);

            var decodeBytes = this.DecryptImpl(bytes);

            return this.Encoding.GetString(decodeBytes);
        }

        protected abstract byte[] EncryptImpl(byte[] bytes);
        protected abstract byte[] DecryptImpl(byte[] bytes);

        public virtual string PostEncrypt(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }

        public virtual byte[] PreDecrypt(string input)
        {
            return System.Convert.FromBase64String(input);
        }

        public abstract void GenerateKeyIV();
    }
}