// krnl_console_bootstrapper.Utils.Stream
using System;
using krnl_console_bootstrapper.Utils;

internal class Stream
{
	public static bool print(string content)
	{
		Console.Write(content);
		Console.ResetColor();
		return true;
	}

	public static bool info(string content)
	{
		Queue.Enqueue(delegate
		{
			print("[");
			Console.ForegroundColor = ConsoleColor.Magenta;
			print("INFO");
			print("] ");
			Console.WriteLine(content);
			return true;
		});
		return true;
	}

	public static bool error(string content)
	{
		Queue.Enqueue(delegate
		{
			print("[");
			Console.ForegroundColor = ConsoleColor.Red;
			print("ERR");
			print("] ");
			Console.WriteLine(content);
			return true;
		});
		return true;
	}

	public static bool download(string content)
	{
		Queue.Enqueue(delegate
		{
			print("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			print("INSTALL");
			print("] ");
			Console.WriteLine(content);
			return true;
		});
		return true;
	}

	public static bool done(string content)
	{
		Queue.Enqueue(delegate
		{
			print("[");
			Console.ForegroundColor = ConsoleColor.White;
			print("DONE");
			print("] ");
			Console.WriteLine(content);
			return true;
		});
		return true;
	}

	public static bool success(string content)
	{
		Queue.Enqueue(delegate
		{
			print("[");
			Console.ForegroundColor = ConsoleColor.Green;
			print("SUCCESS");
			print("] ");
			Console.WriteLine(content);
			return true;
		});
		return true;
	}
}
