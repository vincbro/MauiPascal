using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPascal.Models;

namespace MauiPascal.ViewModels;

public partial class RoutePageViewModel : ObservableObject
{
	[ObservableProperty]
	private bool isBusy = true;
	
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ShowNoRouteMessage))]
	private bool routeFound = false;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ShowNoRouteMessage))]
	private Itinerary? itinerary = null;

	public bool ShowNoRouteMessage => !isBusy && !routeFound;

	[RelayCommand]
	private void ToggleLeg(Leg leg)
	{
		if(leg == null || leg.Stops == null || leg.Stops.Count == 0) return;
		leg.IsExpanded = !leg.IsExpanded;
	}
}
