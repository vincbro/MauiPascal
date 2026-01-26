using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Net.Http.Json;
using Models = MauiPascal.Models;

namespace MauiPascal.Service;

public class BlaiseService(HttpClient client)
{
	public async Task<List<Models::Location>> SearchAsync(string query, int count = 5)
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
			return await result.Content.ReadFromJsonAsync<List<Models::Location>>() ?? [];
		}
		else
		{
			Trace.WriteLine("Failed to fetch search");
			return [];
		}
	}

	public Task<Models::Itinerary?> RouteAsync(Models::Location from, Models::Location to)
	{

		var now = DateTime.Now;
		var hour = now.Hour.ToString("D2");
		var minute = now.Minute.ToString("D2");
		var second = now.Second.ToString("D2");
		var time = $"{hour}:{minute}:{second}";
		return RouteAsync(from, to, time, true);

	}
	public Task<Models::Itinerary?> RouteAsync(Models::Location from, Models::Location to, string time, bool isDeparture = true)
	{
		return RouteAsync(from, to, time, true);
	}

	public async Task<Models::Itinerary?> RouteAsync(string from, string to, string time, bool isDeparture = true)
	{
		var queryParams = new Dictionary<string, string?>
		{
			{ "from", from },
			{ "to", to },
			{ isDeparture ? "departure_at" : "arrive_at", time  },
		};
		var request = new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/routing", queryParams));
		var result = await client.SendAsync(request);
		if(result.IsSuccessStatusCode)
		{
			return await result.Content.ReadFromJsonAsync<Models::Itinerary>();
		}
		else
		{
			Trace.WriteLine("Failed to fetch search");
			return null;
		}
	}


}
