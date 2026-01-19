using MauiPascal.Models;
using Microsoft.AspNetCore.WebUtilities;
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
		var request = new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/search", queryParams));
		var result = await client.SendAsync(request);
		return await result.Content.ReadFromJsonAsync<List<Area>>() ?? [];
	}
}
