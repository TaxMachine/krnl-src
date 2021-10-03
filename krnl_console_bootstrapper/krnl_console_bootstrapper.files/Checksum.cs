// krnl_console_bootstrapper.Files.Checksum
using System;
using System.IO;
using System.Security.Cryptography;
using krnl_console_bootstrapper;

internal class Checksum
{
	public static string GetMD5(string filename)
	{
		using FileStream fileStream = File.OpenRead(filename);
		string result = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
		fileStream.Close();
		return result;
	}

	public static bool CompareFile(string filename)
	{
		if (File.Exists(filename))
		{
			return GetMD5(filename) == Program.hashes[filename];
		}
		return false;
	}
}
