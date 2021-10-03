// SharpCompress.Algorithms.Adler32
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static class Adler32
{
	public const uint SeedValue = 1u;

	private const uint BASE = 65521u;

	private const uint NMAX = 5552u;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint Calculate(ReadOnlySpan<byte> buffer)
	{
		return Calculate(1u, buffer);
	}

	public static uint Calculate(uint adler, ReadOnlySpan<byte> buffer)
	{
		if (buffer.IsEmpty)
		{
			return 1u;
		}
		return CalculateScalar(adler, buffer);
	}

	private static uint CalculateScalar(uint adler, ReadOnlySpan<byte> buffer)
	{
		uint num = adler & 0xFFFFu;
		uint num2 = (adler >> 16) & 0xFFFFu;
		ref byte reference = ref MemoryMarshal.GetReference(buffer);
		uint num3 = (uint)buffer.Length;
		int num4 = 0;
		while (num3 != 0)
		{
			uint num5 = ((num3 < 5552) ? num3 : 5552u);
			num3 -= num5;
			while (true)
			{
				switch (num5)
				{
				default:
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num += Unsafe.Add(ref reference, num4++);
					num2 += num;
					num5 -= 16;
					continue;
				case 1u:
				case 2u:
				case 3u:
				case 4u:
				case 5u:
				case 6u:
				case 7u:
				case 8u:
				case 9u:
				case 10u:
				case 11u:
				case 12u:
				case 13u:
				case 14u:
				case 15u:
					do
					{
						num += Unsafe.Add(ref reference, num4++);
						num2 += num;
					}
					while (--num5 != 0);
					goto case 0u;
				case 0u:
					num %= 65521u;
					num2 %= 65521u;
					break;
				}
				break;
			}
		}
		return (num2 << 16) | num;
	}
}
