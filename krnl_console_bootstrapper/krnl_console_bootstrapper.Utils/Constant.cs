// krnl_console_bootstrapper.Utils.Constants
internal class Constants
{
	private static readonly string mainDomain = "https://krnl.ca";

	private static readonly string keySysDomain = "https://cdn.krnl.ca";

	private static readonly string bootstrapperDomain = "https://k-storage.com";

	public static readonly string inviteUrl = mainDomain + "/invite";

	public static readonly string Title = "Krnl Console Bootstrapper - Rewrite Edition";

	public static readonly string versionUrl = keySysDomain + "/version.txt";

	public static readonly string baseUrl = bootstrapperDomain + "/bootstrapper/files";

	public static readonly string bootstrapperUrl = bootstrapperDomain + "/krnl_console_bootstrapper.exe";

	public static readonly string checksumUrl = bootstrapperDomain + "/bootstrapperChecksum.txt";

	public static readonly string hashUrl = baseUrl + "/hashes.txt";
}
