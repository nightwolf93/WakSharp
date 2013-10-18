using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WakSharp.Utilities.Crypto
{
    public class RSA
    {
        public class RSAProvider
        {
            private RSACryptoServiceProvider _rsa { get; set; }

            public RSAProvider(int lenght)
            {
                CspParameters csp = new CspParameters()
                {
                    ProviderType = 1,
                    KeyNumber = 1
                };
                this._rsa = new RSACryptoServiceProvider(lenght, csp);
                this._rsa.PersistKeyInCsp = true;

            }

            public RSAProvider(string value)
            {
                CspParameters csp = new CspParameters()
                {
                    ProviderType = 1,
                    KeyNumber = 1
                };
                this._rsa = new RSACryptoServiceProvider(csp);
                this._rsa.FromXmlString(this.ImportKeyFromXml(value));
                this._rsa.PersistKeyInCsp = true;
            }

            public byte[] Encrypt(byte[] DataToEncrypt, bool DoOAEPPadding)
            {
                try
                {
                    byte[] encryptedData;
                    //Create a new instance of RSACryptoServiceProvider.
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                    {
                        //Import the RSA Key information. This only needs
                        //toinclude the public key information.
                        RSA.ImportParameters(this._rsa.ExportParameters(false));

                        //Encrypt the passed byte array and specify OAEP padding.  
                        //OAEP padding is only available on Microsoft Windows XP or
                        //later.  
                        encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                    }
                    return encryptedData;
                }
                //Catch and display a CryptographicException  
                //to the console.
                catch
                {
                    return null;
                }
            }

            public byte[] Decrypt(byte[] DataToDecrypt, bool DoOAEPPadding)
            {
                try
                {
                    byte[] decryptedData;
                    //Create a new instance of RSACryptoServiceProvider.
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                    {
                        //Import the RSA Key information. This needs
                        //to include the private key information.
                        RSA.ImportParameters(this._rsa.ExportParameters(true));

                        //Decrypt the passed byte array and specify OAEP padding.  
                        //OAEP padding is only available on Microsoft Windows XP or
                        //later.  
                        decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                    }
                    return decryptedData;
                }
                //Catch and display a CryptographicException  
                //to the console.
                catch
                {
                    return null;
                }
            }

            public byte[] GetKey()
            {
                return this._rsa.ExportCspBlob(false);
            }

            public void ExportKeyToXml(string path)
            {
                try
                {
                    var writer = new StreamWriter(path);
                    writer.Write(this._rsa.ToXmlString(true));
                    writer.Close();
                }
                catch { }
            }

            public byte[] ExportToX509()
            {
                try
                {
                    return AsnKeyBuilder.PublicKeyToX509(this._rsa.ExportParameters(false)).GetBytes();
                }
                catch
                {
                    return null;
                }
            }

            public string ImportKeyFromXml(string path)
            {
                try
                {
                    var key = ("");
                    var reader = new StreamReader(path);
                    key = reader.ReadToEnd();
                    reader.Close();
                    return key;
                }
                catch { return ""; }
            }

            public RSACryptoServiceProvider GetProvider
            {
                get
                {
                    return this._rsa;
                }
            }
        }
    }
}
