// SharpCompress.Utility
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using SharpCompress;
using SharpCompress.Common;
using SharpCompress.Readers;

internal static class Utility
{
	public static ReadOnlyCollection<T> ToReadOnly<T>(this ICollection<T> items)
	{
		return new ReadOnlyCollection<T>(items);
	}

	public static int URShift(int number, int bits)
	{
		if (number >= 0)
		{
			return number >> bits;
		}
		return (number >> bits) + (2 << ~bits);
	}

	public static long URShift(long number, int bits)
	{
		if (number >= 0)
		{
			return number >> bits;
		}
		return (number >> bits) + (2L << ~bits);
	}

	public static void SetSize(this List<byte> list, int count)
	{
		if (count > list.Count)
		{
			list.Capacity = count;
			for (int i = list.Count; i < count; i++)
			{
				list.Add(0);
			}
		}
		else
		{
			list.RemoveRange(count, list.Count - count);
		}
	}

	public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
	{
		foreach (T item in items)
		{
			action(item);
		}
	}

	public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
	{
		if (sourceIndex > int.MaxValue || sourceIndex < int.MinValue)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (destinationIndex > int.MaxValue || destinationIndex < int.MinValue)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (length > int.MaxValue || length < int.MinValue)
		{
			throw new ArgumentOutOfRangeException();
		}
		Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
	}

	public static IEnumerable<T> AsEnumerable<T>(this T item)
	{
		yield return item;
	}

	public static void CheckNotNull(this object obj, string name)
	{
		if (obj == null)
		{
			throw new ArgumentNullException(name);
		}
	}

	public static void CheckNotNullOrEmpty(this string obj, string name)
	{
		obj.CheckNotNull(name);
		if (obj.Length == 0)
		{
			throw new ArgumentException("String is empty.", name);
		}
	}

	public static void Skip(this Stream source, long advanceAmount)
	{
		if (source.CanSeek)
		{
			source.Position += advanceAmount;
			return;
		}
		byte[] transferByteArray = GetTransferByteArray();
		try
		{
			int num = 0;
			int num2 = 0;
			do
			{
				num2 = transferByteArray.Length;
				if (num2 > advanceAmount)
				{
					num2 = (int)advanceAmount;
				}
				num = source.Read(transferByteArray, 0, num2);
				if (num <= 0)
				{
					break;
				}
				advanceAmount -= num;
			}
			while (advanceAmount != 0L);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(transferByteArray);
		}
	}

	public static void Skip(this Stream source)
	{
		byte[] transferByteArray = GetTransferByteArray();
		try
		{
			while (source.Read(transferByteArray, 0, transferByteArray.Length) == transferByteArray.Length)
			{
			}
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(transferByteArray);
		}
	}

	public static DateTime DosDateToDateTime(ushort iDate, ushort iTime)
	{
		int year = (int)iDate / 512 + 1980;
		int num = (int)iDate % 512 / 32;
		int num2 = (int)iDate % 512 % 32;
		int hour = (int)iTime / 2048;
		int minute = (int)iTime % 2048 / 32;
		int second = (int)iTime % 2048 % 32 * 2;
		if (iDate == ushort.MaxValue || num == 0 || num2 == 0)
		{
			year = 1980;
			num = 1;
			num2 = 1;
		}
		if (iTime == ushort.MaxValue)
		{
			hour = (minute = (second = 0));
		}
		try
		{
			return new DateTime(year, num, num2, hour, minute, second, DateTimeKind.Local);
		}
		catch
		{
			return default(DateTime);
		}
	}

	public static uint DateTimeToDosTime(this DateTime? dateTime)
	{
		if (!dateTime.HasValue)
		{
			return 0u;
		}
		DateTime dateTime2 = dateTime.Value.ToLocalTime();
		return (uint)((dateTime2.Second / 2) | (dateTime2.Minute << 5) | (dateTime2.Hour << 11) | (dateTime2.Day << 16) | (dateTime2.Month << 21) | (dateTime2.Year - 1980 << 25));
	}

	public static DateTime DosDateToDateTime(uint iTime)
	{
		return DosDateToDateTime((ushort)(iTime / 65536u), (ushort)(iTime % 65536u));
	}

	public static DateTime UnixTimeToDateTime(long unixtime)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixtime);
	}

	public static long TransferTo(this Stream source, Stream destination)
	{
		byte[] transferByteArray = GetTransferByteArray();
		try
		{
			long num = 0L;
			int count;
			while (ReadTransferBlock(source, transferByteArray, out count))
			{
				num += count;
				destination.Write(transferByteArray, 0, count);
			}
			return num;
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(transferByteArray);
		}
	}

	public static long TransferTo(this Stream source, Stream destination, Entry entry, IReaderExtractionListener readerExtractionListener)
	{
		byte[] transferByteArray = GetTransferByteArray();
		try
		{
			int num = 0;
			long num2 = 0L;
			int count;
			while (ReadTransferBlock(source, transferByteArray, out count))
			{
				num2 += count;
				destination.Write(transferByteArray, 0, count);
				num++;
				readerExtractionListener.FireEntryExtractionProgress(entry, num2, num);
			}
			return num2;
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(transferByteArray);
		}
	}

	private static bool ReadTransferBlock(Stream source, byte[] array, out int count)
	{
		return (count = source.Read(array, 0, array.Length)) != 0;
	}

	private static byte[] GetTransferByteArray()
	{
		return ArrayPool<byte>.Shared.Rent(81920);
	}

	public static bool ReadFully(this Stream stream, byte[] buffer)
	{
		int num = 0;
		int num2;
		while ((num2 = stream.Read(buffer, num, buffer.Length - num)) > 0)
		{
			num += num2;
			if (num >= buffer.Length)
			{
				return true;
			}
		}
		return num >= buffer.Length;
	}

	public static bool ReadFully(this Stream stream, Span<byte> buffer)
	{
		int num = 0;
		int num2;
		while ((num2 = stream.Read(buffer.Slice(num, buffer.Length - num))) > 0)
		{
			num += num2;
			if (num >= buffer.Length)
			{
				return true;
			}
		}
		return num >= buffer.Length;
	}

	public static string TrimNulls(this string source)
	{
		return source.Replace('\0', ' ').Trim();
	}
}
