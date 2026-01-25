using MauiPascal.Service;
using MauiPascal.ViewModels;

namespace MauiPascal;

public partial class RoutePage : ContentPage, IQueryAttributable
{
	private RoutePageViewModel vm;
	private readonly BlaiseService blaise;

	public RoutePage(BlaiseService blaise)
	{
		this.blaise = blaise;
		vm = new RoutePageViewModel();
		BindingContext = vm;
		InitializeComponent();
	}

	// This is where the magic happens!
	public async void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		// 1. Grab all the info you passed
		if(query.TryGetValue("from", out var rawFrom) &&
			query.TryGetValue("to", out var rawTo) &&
			query.TryGetValue("time", out var rawTime) &&
			query.TryGetValue("is_departure", out var rawIsDeparture))
		{
			var from = (string)rawFrom ?? string.Empty;
			var to = (string)rawTo ?? string.Empty;
			var time = (string)rawTime ?? string.Empty;
			var isDeparture = bool.Parse((string)rawIsDeparture ?? string.Empty);
			vm.IsBusy = true;
			vm.Itinerary = await blaise.RouteAsync(from, to, time, isDeparture);
			vm.IsBusy = false;
			vm.RouteFound = vm.Itinerary != null;
		}
	}
}