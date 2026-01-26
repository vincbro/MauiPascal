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

	/// <summary>
	/// Total duration of the itinerary in minutes
	/// </summary>
	public int TotalDurationMinutes
	{
		get
		{
		if (Legs == null || Legs.Count == 0) return 0;
		var firstLeg = Legs.First();
		var lastLeg = Legs.Last();
		return (lastLeg.ArrivalTime - firstLeg.DepartureTime) / 60;
		}
	}
}
