// krnl_console_bootstrapper.Files.ZipFile
using System;
using System.IO;
using System.Linq;
using krnl_console_bootstrapper.Files;
using krnl_console_bootstrapper.Utils;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;

internal class ZipFile
{
	private enum ArchiveType
	{
		SevenZip,
		Zip
	}

	public static void Extract(string filename, byte[] bytes)
	{
		try
		{
			string text = Path.Combine(Environment.CurrentDirectory, filename.Split('/')[0]);
			ArchiveType archiveType = (filename.EndsWith(".zip") ? ArchiveType.Zip : ArchiveType.SevenZip);
			string path = filename.Split('/').ToList().Last()
				.Split('.')[0];
			string path2 = Path.Combine(text, path);
			if (!Directory.Exists(path2))
			{
				Directory.CreateDirectory(path2);
			}
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				switch (archiveType)
				{
				case ArchiveType.SevenZip:
				{
					IReader reader2 = SevenZipArchive.Open(stream).ExtractAllEntries();
					while (reader2.MoveToNextEntry())
					{
						if (!reader2.Entry.IsDirectory)
						{
							reader2.WriteEntryToDirectory(text, new ExtractionOptions
							{
								ExtractFullPath = true,
								Overwrite = true
							});
						}
					}
					break;
				}
				case ArchiveType.Zip:
				{
					IReader reader = ZipArchive.Open(stream).ExtractAllEntries();
					while (reader.MoveToNextEntry())
					{
						if (!reader.Entry.IsDirectory)
						{
							reader.WriteEntryToDirectory(text, new ExtractionOptions
							{
								ExtractFullPath = true,
								Overwrite = true
							});
						}
					}
					break;
				}
				}
			}
			krnl_console_bootstrapper.Utils.Stream.done("Successfully extracted the archive file named " + filename);
		}
		catch
		{
			krnl_console_bootstrapper.Utils.Stream.error("Failed to extract the archive file named " + filename);
		}
	}
}
