using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace MauiPascal.Models;

public partial class Leg : ObservableObject
{
	[JsonPropertyName("from")]
	public Location From { get; set; } = default!;

	[JsonPropertyName("to")]
	public Location To { get; set; } = default!;

	[JsonPropertyName("departue_time")]
	public int DepartureTime { get; set; } = 0;

	[JsonPropertyName("arrival_time")]
	public int ArrivalTime { get; set; } = 0;

	[JsonPropertyName("stops")]
	public List<Stop> Stops { get; set; } = [];

	[JsonPropertyName("mode")]
	public string Mode { get; set; } = string.Empty;

	[JsonPropertyName("head_sign")]
	public string? HeadSign { get; set; } = null;

	[JsonPropertyName("long_name")]
	public string? LongName { get; set; } = null;

	[JsonPropertyName("short_name")]
	public string? ShortName { get; set; } = null;
	[ObservableProperty]
	private bool isExpanded;

	/// <summary>
	/// Duration of the leg in minutes
	/// </summary>
	public int DurationMinutes => (ArrivalTime - DepartureTime) / 60;
}
