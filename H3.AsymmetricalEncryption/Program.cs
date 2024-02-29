using System.Security.Cryptography;
using System.Text;

namespace H3.AsymmetricalEncryption
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter exponent: ");
			string exponent = Console.ReadLine();

			Console.WriteLine("Enter modulus: ");
			string modulus = Console.ReadLine();

			Console.WriteLine("Enter message: ");
			string message = Console.ReadLine();

			// Encrypt the message using the supplied exponent and modulus
			byte[] encryptedBytes = Encrypt(Encoding.UTF8.GetBytes(message), exponent, modulus);

			// Convert the encrypted bytes to hex
			string encryptedMessage = BitConverter.ToString(encryptedBytes);

			Console.WriteLine("Encrypted message: " + encryptedMessage);

			Console.ReadKey();
		}

		static byte[] Encrypt(byte[] messageBytes, string exponent, string modulus)
		{
			using RSA rsa = RSA.Create();
			RSAParameters rsaParams = new RSAParameters
			{
				Exponent = ParseHexBytes(exponent),
				Modulus = ParseHexBytes(modulus)
			};
			rsa.ImportParameters(rsaParams);

			return rsa.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA256);
		}

		static byte[] ParseHexBytes(string hex)
		{
			hex = hex.Replace("-", "");
			byte[] bytes = new byte[hex.Length / 2];
			for (int i = 0; i < hex.Length; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}
			return bytes;
		}
	}
}
