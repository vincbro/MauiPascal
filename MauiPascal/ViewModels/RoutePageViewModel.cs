using CommunityToolkit.Mvvm.ComponentModel;
using MauiPascal.Models;

namespace MauiPascal.ViewModels;

public partial class RoutePageViewModel : ObservableObject
{
	[ObservableProperty]
	private bool isBusy = true;
	[ObservableProperty]
	private bool routeFound = false;


	[ObservableProperty]
	private Itinerary? itinerary = null;
}
