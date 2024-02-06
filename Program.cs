using Org.BouncyCastle.Crypto.Digests;

DirectoryInfo di = new ("Files");
FileInfo[] files = di.GetFiles("*.data");

List<string> hashList = [];

foreach (FileInfo file in files)
{
    // Console.WriteLine(file.Name);
    using FileStream fileStream = new(file.FullName, FileMode.Open, FileAccess.Read);
    byte[] buffer = new byte[1024];

    fileStream.Read(buffer, 0, buffer.Length);

    // Create a SHA3-256 digest instance
    var digest = new Sha3Digest(256);

    // Calculate the hash
    digest.BlockUpdate(buffer, 0, buffer.Length);
    byte[] result = new byte[digest.GetDigestSize()];
    digest.DoFinal(result, 0);

    // Convert the hash to a hexadecimal string
    string hexString = BitConverter.ToString(result).Replace("-", "").ToLower();

    hashList.Add(hexString);
}

hashList.Sort();

string combinedString = string.Join("", hashList);
string canCatWithEmail = "s.azodov.software.engineer@gmail.com" + combinedString;

byte[] data = System.Text.Encoding.UTF8.GetBytes(canCatWithEmail);
var digest2 = new Sha3Digest(256);

digest2.BlockUpdate(data, 0, data.Length);
byte[] result2 = new byte[digest2.GetDigestSize()];
digest2.DoFinal(result2, 0);

string hexString2 = BitConverter.ToString(result2).Replace("-", "").ToLowerInvariant();
Console.WriteLine(hexString2);
