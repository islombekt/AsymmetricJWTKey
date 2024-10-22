using System.Security.Cryptography;
using System.Text;

namespace KeyGenerator
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string KeyDirectoryPath = Path.Combine(Environment.CurrentDirectory, "Keys");
            if (!Directory.Exists(KeyDirectoryPath))
            {
                Directory.CreateDirectory(KeyDirectoryPath);
            }
            var rsa = RSA.Create(2048);
            string privateKeyXml = rsa.ToXmlString(true);
            string publicKeyXml = rsa.ToXmlString(false);

            using var privateFile = File.Create(Path.Combine(KeyDirectoryPath, "PrivateKey.xml"));
            using var publicFile = File.Create(Path.Combine(KeyDirectoryPath, "PublicKey.xml"));

            privateFile.Write(Encoding.UTF8.GetBytes(privateKeyXml));
            publicFile.Write(Encoding.UTF8.GetBytes(publicKeyXml));

        }      
    
    }
}