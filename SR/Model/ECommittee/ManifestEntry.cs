using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommittees.Model
{
	[Table("manifest_entries")]
	public partial class ManifestEntry
	{
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

		[JsonProperty(PropertyName = "manifest_entry_token")]
		public string ManifestEntryToken { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "plain_sha_256")]
		public string PlainSha256 { get; set; }

		[JsonProperty(PropertyName = "encrypted_sha_256")]
		public string EncryptedSha256 { get; set; }

		[JsonProperty(PropertyName = "decryption_key")]
		public string DecryptionKey { get; set; }

		//    public async Task<byte[]> DecryptContent()
		//    {
		//        var cred = IdentityService.Instance.KeyPair;
		//        var buffEncrypt = CryptographicBuffer.DecodeFromBase64String(DecryptionKey);
		//        if (cred != null)
		//        {
		//            var tmpToken = new Token();
		//            var result = DataProviderService.Instance.DecryptionKey.TryGetValue(PlainSha256, out tmpToken);

		//            if (!result)
		//            {
		//                return null;
		//            }

		//            var symmKeyDecryptedString = tmpToken.SymmetricKey;
		//            // Decrypt the symmetric key first
		//            if (string.IsNullOrEmpty(symmKeyDecryptedString))
		//            {
		//                var symmKeyDecrypted = CryptographicEngine.Decrypt(cred, buffEncrypt, null);

		//                Debug.WriteLine($"filename: {PlainSha256}; encryption_key(utf8): {CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, symmKeyDecrypted)}; encryption_key(base64): {CryptographicBuffer.EncodeToBase64String(symmKeyDecrypted)}");

		//                symmKeyDecryptedString = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, symmKeyDecrypted);
		//            }

		//            // Deal with the file content now
		//            var inputFile = await ApplicationData.Current.LocalFolder.GetFileAsync(PlainSha256);
		//            byte[] ibuff = (await FileIO.ReadBufferAsync(inputFile)).ToArray();

		//            var InBuf = ibuff.ToArray();
		//            var fileHeader = new Header();
		//            fileHeader.Version = ibuff[0];
		//            fileHeader.Option = (Option)ibuff[1];
		//            //Read the salt
		//            for (int i = 2; i < 10; i++)
		//            {
		//                fileHeader.Salt[i - 2] = InBuf[i];
		//            }
		//            for (int i = 10; i < 18; i++)
		//            {
		//                fileHeader.HmaCSalt[i - 10] = InBuf[i];
		//            }

		//            for (int i = 18; i < 33; i++)
		//            {
		//                fileHeader.Iv[i - 18] = InBuf[i];
		//            }
		//            uint encryptBufLen = (uint)ibuff.Length - 32 - 34;

		//            var cypherBytes = new byte[encryptBufLen];
		//            Array.Copy(InBuf, 34, cypherBytes, 0, (int)encryptBufLen);

		//            var inputBuffer = cypherBytes.AsBuffer();

		//            var objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
		//            var keyArray = Convert.FromBase64String(symmKeyDecryptedString);
		//            var key = objAlg.CreateSymmetricKey(keyArray.AsBuffer());

		//            var ivBuf = fileHeader.Iv.AsBuffer();

		//            try
		//            {
		//                var decrypted = CryptographicEngine.Decrypt(key, inputBuffer, fileHeader.Iv.AsBuffer());
		//                var outBytes = decrypted.ToArray();
		//                return outBytes;
		//            }
		//            catch 
		//            {
		//                return null;
		//            }
		//        }
		//        else
		//        {
		//            return null;
		//        }
		//    }
		//}

		public enum Option : byte
		{
			NoPassword = 0,
			UsePassword = 1
		}

		public class Header
		{
			public byte Version { get; set; }
			public Option Option { get; set; }
			public byte[] Salt { get; set; } = new byte[8];
			public byte[] HmaCSalt { get; set; } = new byte[8];
			public byte[] Iv { get; set; } = new byte[16];

			public override string ToString()
			{
				return JsonConvert.SerializeObject(this);
			}
		}

		public class Token
		{
			public string SymmetricKey { get; set; }
			public bool IsTokenDeactivated { get; set; }
		}
	}
}