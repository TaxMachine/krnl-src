// Interop
using System;
using System.Runtime.InteropServices;

internal static class Interop
{
	internal static class Libraries
	{
		internal const string Advapi32 = "advapi32.dll";

		internal const string BCrypt = "BCrypt.dll";

		internal const string Crypt32 = "crypt32.dll";

		internal const string CryptUI = "cryptui.dll";

		internal const string Gdi32 = "gdi32.dll";

		internal const string HttpApi = "httpapi.dll";

		internal const string IpHlpApi = "iphlpapi.dll";

		internal const string Kernel32 = "kernel32.dll";

		internal const string Mswsock = "mswsock.dll";

		internal const string NCrypt = "ncrypt.dll";

		internal const string NtDll = "ntdll.dll";

		internal const string Odbc32 = "odbc32.dll";

		internal const string Ole32 = "ole32.dll";

		internal const string OleAut32 = "oleaut32.dll";

		internal const string PerfCounter = "perfcounter.dll";

		internal const string Secur32 = "secur32.dll";

		internal const string Shell32 = "shell32.dll";

		internal const string SspiCli = "sspicli.dll";

		internal const string User32 = "user32.dll";

		internal const string Version = "version.dll";

		internal const string WebSocket = "websocket.dll";

		internal const string WinHttp = "winhttp.dll";

		internal const string WinMM = "winmm.dll";

		internal const string Wldap32 = "wldap32.dll";

		internal const string Ws2_32 = "ws2_32.dll";

		internal const string Wtsapi32 = "wtsapi32.dll";

		internal const string CompressionNative = "clrcompression.dll";

		internal const string MsQuic = "msquic.dll";

		internal const string HostPolicy = "hostpolicy.dll";
	}

	internal enum BOOL
	{
		FALSE,
		TRUE
	}

	internal static class Kernel32
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CPINFOEXW
		{
			internal uint MaxCharSize;

			internal unsafe fixed byte DefaultChar[2];

			internal unsafe fixed byte LeadByte[12];

			internal char UnicodeDefaultChar;

			internal uint CodePage;

			internal unsafe fixed char CodePageName[260];
		}

		internal const int MAX_PATH = 260;

		internal const uint CP_ACP = 0u;

		internal const uint WC_NO_BEST_FIT_CHARS = 1024u;

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private unsafe static extern BOOL GetCPInfoExW(uint CodePage, uint dwFlags, CPINFOEXW* lpCPInfoEx);

		internal unsafe static int GetLeadByteRanges(int codePage, byte[] leadByteRanges)
		{
			int num = 0;
			CPINFOEXW cPINFOEXW = default(CPINFOEXW);
			if (GetCPInfoExW((uint)codePage, 0u, &cPINFOEXW) != 0)
			{
				for (int i = 0; i < 10 && leadByteRanges[i] != 0; i += 2)
				{
					leadByteRanges[i] = cPINFOEXW.LeadByte[i];
					leadByteRanges[i + 1] = cPINFOEXW.LeadByte[i + 1];
					num++;
				}
			}
			return num;
		}

		internal unsafe static bool TryGetACPCodePage(out int codePage)
		{
			CPINFOEXW cPINFOEXW = default(CPINFOEXW);
			if (GetCPInfoExW(0u, 0u, &cPINFOEXW) != 0)
			{
				codePage = (int)cPINFOEXW.CodePage;
				return true;
			}
			codePage = 0;
			return false;
		}

		[DllImport("kernel32.dll")]
		internal unsafe static extern int WideCharToMultiByte(uint CodePage, uint dwFlags, char* lpWideCharStr, int cchWideChar, byte* lpMultiByteStr, int cbMultiByte, IntPtr lpDefaultChar, IntPtr lpUsedDefaultChar);
	}
}
