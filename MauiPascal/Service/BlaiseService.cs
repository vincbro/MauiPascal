using MauiPascal.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Net.Http.Json;

namespace MauiPascal.Service;

public class BlaiseService(HttpClient client)
{
	public async Task<List<Area>> SearchAsync(string query, int count = 5)
	{
		var queryParams = new Dictionary<string, string?>
		{
			{ "q", query },
			{ "count", count.ToString() },
		};
		var request = new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/search/area", queryParams));
		var result = await client.SendAsync(request);
		if(result.IsSuccessStatusCode)
		{
			return await result.Content.ReadFromJsonAsync<List<Area>>() ?? [];
		}
		else
		{
			Trace.WriteLine("Failed to fetch search");
			return [];
		}
	}
}
