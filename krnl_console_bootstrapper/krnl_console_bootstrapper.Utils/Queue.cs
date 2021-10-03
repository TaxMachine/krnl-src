// krnl_console_bootstrapper.Utils.Queue
using System;
using System.Collections.Generic;

internal class Queue
{
	public static List<Func<bool>> queues = new List<Func<bool>>();

	public static void Enqueue(Func<bool> func)
	{
		queues.Add(func);
	}

	public static bool Dequeue()
	{
		bool result = false;
		if (queues.Count > 0)
		{
			result = queues[0]();
			queues.RemoveAt(0);
		}
		return result;
	}
}
