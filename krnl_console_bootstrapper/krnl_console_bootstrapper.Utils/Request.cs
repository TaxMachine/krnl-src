// krnl_console_bootstrapper.Utils.Request
using System;
using System.Net.Http;
using System.Threading.Tasks;

public static class Request
{
	public static async Task<string> Create(string URL)
	{
		int attempts = 0;
		do
		{
			try
			{
				using HttpClient client = new HttpClient();
				client.Timeout = TimeSpan.FromMinutes(5.0);
				HttpResponseMessage response = await client.GetAsync(URL);
				response.EnsureSuccessStatusCode();
				if ((await response.Content.ReadAsStringAsync()).IndexOf("cloudflare") != -1)
				{
					throw new Exception();
				}
				return await response.Content.ReadAsStringAsync();
			}
			catch
			{
				attempts++;
			}
		}
		while (attempts != 6);
		return "error";
	}

	public static async Task<byte[]> CreateByteArray(string URL)
	{
		int attempts = 0;
		do
		{
			try
			{
				using HttpClient client = new HttpClient();
				client.Timeout = TimeSpan.FromMinutes(5.0);
				HttpResponseMessage response = await client.GetAsync(URL);
				response.EnsureSuccessStatusCode();
				if ((await response.Content.ReadAsStringAsync()).IndexOf("cloudflare") != -1)
				{
					throw new Exception();
				}
				return await response.Content.ReadAsByteArrayAsync();
			}
			catch
			{
				attempts++;
			}
		}
		while (attempts != 6);
		return new byte[0];
	}
}
