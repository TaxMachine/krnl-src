// krnl_console_bootstrapper.Program
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using krnl_console_bootstrapper.Files;
using krnl_console_bootstrapper.Properties;
using krnl_console_bootstrapper.Utils;

internal class Program
{
	public static Dictionary<string, string> hashes = new Dictionary<string, string>();

	private static void killProcess(string name)
	{
		Process[] processesByName = Process.GetProcessesByName(name);
		for (int i = 0; i < processesByName.Length; i++)
		{
			if (processesByName[i].Id != Process.GetCurrentProcess().Id)
			{
				try
				{
					processesByName[i].Kill();
				}
				catch
				{
				}
			}
		}
	}

	private static void Main(string[] args)
	{
		killProcess("krnl_console_bootstrapper");
		if (File.Exists("krnl_console_bootstrapper.bak"))
		{
			try
			{
				File.Delete("krnl_console_bootstrapper.bak");
			}
			catch
			{
			}
		}
		int taskCompleted = 0;
		Console.Title = Constants.Title;
		DateTime now = DateTime.Now;
		bool failed = false;
		new Task(async delegate
		{
			while (!failed)
			{
				TimeSpan timeSpan = (DateTime.Now - now).Duration();
				if (hashes.Count != 0)
				{
					Console.Title = Constants.Title + " | " + Convert.ToString(taskCompleted) + "/" + Convert.ToString(hashes.Count) + " | " + $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2} Elapsed";
				}
				else
				{
					Console.Title = Constants.Title + " | " + $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2} Elapsed";
				}
				Queue.Dequeue();
				await Task.Delay(1);
			}
		}).Start();
		krnl_console_bootstrapper.Utils.Stream.info("KRNL Invite: " + Constants.inviteUrl);
		krnl_console_bootstrapper.Utils.Stream.info("Checking if KRNL is up-to-date...");
		string result = Request.Create(Constants.versionUrl).GetAwaiter().GetResult();
		if (result.IndexOf('<') != 0 && result.ToLower().IndexOf("error") == -1 && result.IndexOf("patch") != -1)
		{
			krnl_console_bootstrapper.Utils.Stream.error("KRNL is currently patched, please wait until we announce an update for the tool!\nPress any key to exit...");
			Task.Delay(500).GetAwaiter().GetResult();
			failed = true;
			Console.ReadKey();
			return;
		}
		krnl_console_bootstrapper.Utils.Stream.info("Checking if KRNL Console Bootstrapper is up-to-date...");
		string result2 = Request.Create(Constants.checksumUrl).GetAwaiter().GetResult();
		if (result2.IndexOf('<') == -1 && result2.ToLower().IndexOf("error") == -1)
		{
			if (Checksum.GetMD5(Process.GetCurrentProcess().MainModule.FileName) != result2)
			{
				krnl_console_bootstrapper.Utils.Stream.download("krnl_console_bootstrapper.exe");
				byte[] result3 = Request.CreateByteArray(Constants.bootstrapperUrl).GetAwaiter().GetResult();
				if (result3.Length != 0)
				{
					try
					{
						File.Move(Process.GetCurrentProcess().MainModule.FileName, "krnl_console_bootstrapper.bak");
						Resource.WriteFile("krnl_console_bootstrapper.exe", result3);
						Process.Start("krnl_console_bootstrapper.exe");
						return;
					}
					catch
					{
					}
				}
				krnl_console_bootstrapper.Utils.Stream.error("Failed to update the KRNL Console Bootstrapper...\nPress any key to exit.");
				Console.ReadKey();
				return;
			}
			krnl_console_bootstrapper.Utils.Stream.info("KRNL Console Bootstrapper is up-to-date...");
		}
		else
		{
			krnl_console_bootstrapper.Utils.Stream.error("Unable to confirm whether KRNL Console Bootstrapper is up-to-date...");
		}
		try
		{
			string currentDirectory = Environment.CurrentDirectory;
			if (Path.GetTempPath().IndexOf(currentDirectory) == 0)
			{
				throw new Exception();
			}
			if (Environment.GetFolderPath(Environment.SpecialFolder.SystemX86).IndexOf(currentDirectory) == 0)
			{
				throw new Exception();
			}
			if (Environment.GetFolderPath(Environment.SpecialFolder.System).IndexOf(currentDirectory) == 0)
			{
				throw new Exception();
			}
			if (Environment.GetFolderPath(Environment.SpecialFolder.Windows).IndexOf(currentDirectory) == 0)
			{
				throw new Exception();
			}
			File.WriteAllText("krnl_bootstrapper_console_test.txt", "here");
			File.Delete("krnl_bootstrapper_console_test.txt");
		}
		catch
		{
			Environment.CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}
		try
		{
			Resource.WriteFile("7za.exe", Resources._7za);
		}
		catch
		{
		}
		if (!Environment.CurrentDirectory.EndsWith("\\krnl"))
		{
			if (!Directory.Exists("krnl"))
			{
				Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "krnl"));
			}
			Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "krnl");
		}
		krnl_console_bootstrapper.Utils.Stream.info("Attempting to fetch checksums of KRNL files...");
		string result4 = Request.Create(Constants.hashUrl).GetAwaiter().GetResult();
		if (result4.IndexOf('<') != 0 && result4.ToLower().IndexOf("error") == -1)
		{
			string[] array = result4.Replace("\r", "").Split('\n');
			foreach (string text in array)
			{
				string key = text.Split('=')[0];
				string value = text.Split('=')[1];
				hashes[key] = value;
			}
		}
		if (hashes.Count == 0)
		{
			krnl_console_bootstrapper.Utils.Stream.error("Unable to fetch checksums of KRNL files...\nPress any key to exit...");
			Task.Delay(500).GetAwaiter().GetResult();
			failed = true;
			Console.ReadKey();
			return;
		}
		krnl_console_bootstrapper.Utils.Stream.info("Comparing checksums between client and server files...");
		Parallel.For(0, hashes.Count, delegate(int i)
		{
			string text2 = hashes.Keys.ToList()[i];
			if (!Checksum.CompareFile(text2))
			{
				if (text2 == "krnl.dll")
				{
					killProcess("RobloxPlayerBeta");
					if (File.Exists("krnl.dll"))
					{
						try
						{
							File.Delete("krnl.dll");
						}
						catch
						{
						}
					}
				}
				if (text2 == "krnlss.exe")
				{
					killProcess("krnlss");
					if (File.Exists("krnlss.exe"))
					{
						try
						{
							File.Delete("krnlss.exe");
						}
						catch
						{
						}
					}
				}
				krnl_console_bootstrapper.Utils.Stream.download(text2);
				byte[] result5 = Request.CreateByteArray(Constants.baseUrl + "/" + text2).GetAwaiter().GetResult();
				if (result5.Length != 0)
				{
					Resource.WriteFile(text2, result5);
					krnl_console_bootstrapper.Utils.Stream.success(text2 + " is now up-to-date.");
				}
				else
				{
					krnl_console_bootstrapper.Utils.Stream.error("Failed to install " + text2);
				}
			}
			else
			{
				krnl_console_bootstrapper.Utils.Stream.done(text2 + " is up-to-date.");
			}
			taskCompleted++;
		});
		while (taskCompleted != hashes.Count)
		{
			Task.Delay(1000).GetAwaiter().GetResult();
		}
		Task.Delay(1000).GetAwaiter().GetResult();
		Console.Write("Press any key to exit...");
		Task.Delay(500).GetAwaiter().GetResult();
		failed = true;
		Console.ReadKey();
		try
		{
			Process.Start("explorer.exe", "\"" + Environment.CurrentDirectory + "\"");
			File.Delete("7za.exe");
		}
		catch
		{
		}
		failed = true;
	}
}
