using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Net51Zhang.Common.Encryption
{
    public interface IEncryptionProvider
    {
        byte[] Key { get; }
        byte[] IV { get; }
        Encoding Encoding { get; }
        string Encrypt(string data);
        string Decrypt(string encodeData);
    }
}