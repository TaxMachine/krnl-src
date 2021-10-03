// krnl_console_bootstrapper.Files.Resource
using System;
using System.IO;
using krnl_console_bootstrapper.Files;
using krnl_console_bootstrapper.Utils;

internal class Resource
{
	public static void WriteFile(string filename, byte[] bytes)
	{
		if (filename.IndexOf('/') != -1)
		{
			string path = filename.Split('/')[0];
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		if (filename.IndexOf(".zip") != -1 || filename.IndexOf(".7z") != -1)
		{
			krnl_console_bootstrapper.Utils.Stream.info("Extracting " + filename + " ...");
			ZipFile.Extract(filename, bytes);
		}
		using FileStream fileStream = new FileStream(Path.Combine(Environment.CurrentDirectory, filename), FileMode.OpenOrCreate, FileAccess.ReadWrite);
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
	}
}
