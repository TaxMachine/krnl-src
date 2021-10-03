// SharpCompress.StringExtensions
internal static class StringExtensions
{
	internal static bool EndsWith(this string text, char value)
	{
		if (text.Length > 0)
		{
			return text[text.Length - 1] == value;
		}
		return false;
	}

	internal static bool Contains(this string text, char value)
	{
		return text.IndexOf(value) > -1;
	}
}
