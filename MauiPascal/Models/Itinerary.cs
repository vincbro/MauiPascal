using System.Text.Json.Serialization;

namespace MauiPascal.Models;

public class Itinerary
{
	[JsonPropertyName("from")]
	public Location From { get; set; } = default!;

	[JsonPropertyName("to")]
	public Location To { get; set; } = default!;

	[JsonPropertyName("legs")]
	public List<Leg> Legs { get; set; } = [];
}
