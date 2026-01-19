using System.Text.Json.Serialization;

namespace MauiPascal.Models;

public class Coordinate
{
	[JsonPropertyName("longitude")]
	public float Longitude { get; set; }

	[JsonPropertyName("latitude")]
	public float Latitude { get; set; }
}
