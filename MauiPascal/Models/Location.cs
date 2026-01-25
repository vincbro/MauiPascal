using System.Text.Json.Serialization;

namespace MauiPascal.Models;

public class Location
{
	[JsonPropertyName("type")]
	public string? Type { get; set; } = null;

	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;
	[JsonPropertyName("coordinate")]
	public Coordinate Coordinate { get; set; } = new();

	public override string ToString()
	{
		return Name;
	}
}
