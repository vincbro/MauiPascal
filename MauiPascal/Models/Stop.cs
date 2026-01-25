using System.Text.Json.Serialization;

namespace MauiPascal.Models;

public class Stop
{
	[JsonPropertyName("location")]
	public Location Location { get; set; } = default!;

	[JsonPropertyName("departure_time")]
	public int DepartureTime { get; set; } = 0;

	[JsonPropertyName("arrival_time")]
	public int ArrivalTime { get; set; } = 0;
}

