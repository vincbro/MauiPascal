using MauiPascal.Service;
using MauiPascal.ViewModels;

namespace MauiPascal
{
	public partial class MainPage : ContentPage
	{
		private MainPageViewModel vm;
		private readonly BlaiseService blaise;
		private bool fromFocused = true;
		private bool toFocused = false;
		private List<Models.Location> fromSuggestions = [];
		private List<Models.Location> toSuggestions = [];

		public MainPage(BlaiseService blaise)
		{
			this.blaise = blaise;
			InitializeComponent();
			vm = new MainPageViewModel();
			BindingContext = vm;
			UpdateFindRoute();
		}


		private CancellationTokenSource searchCts = new();

		private async void From_TextChanged(object sender, TextChangedEventArgs e)
		{
			searchCts?.Cancel();
			searchCts = new CancellationTokenSource();
			var token = searchCts.Token;

			try
			{
				await Task.Delay(300, token);
				if(string.IsNullOrWhiteSpace(From.Text))
				{
					vm.Suggestions.Clear();
					return;
				}

				fromSuggestions = await blaise.SearchAsync(From.Text, 10);
				if(token.IsCancellationRequested) return;

				vm.Suggestions.Clear();
				foreach(var result in fromSuggestions)
				{
					vm.Suggestions.Add(result);
				}
			}
			catch(TaskCanceledException)
			{
			}
		}

		private void From_Focused(object sender, FocusEventArgs e)
		{
			fromFocused = true;
			toFocused = false;

			vm.Suggestions.Clear();
			foreach(var result in fromSuggestions)
			{
				vm.Suggestions.Add(result);
			}
		}

		private void RemoveFrom_Clicked(object sender, EventArgs e)
		{
			vm.FromArea = null;
			From.Text = string.Empty;
			fromSuggestions.Clear();
			vm.Suggestions.Clear();
			UpdateFindRoute();
		}


		private async void To_TextChanged(object sender, TextChangedEventArgs e)
		{
			searchCts?.Cancel();
			searchCts = new CancellationTokenSource();
			var token = searchCts.Token;

			try
			{
				await Task.Delay(300, token);
				if(string.IsNullOrWhiteSpace(To.Text))
				{
					vm.Suggestions.Clear();
					return;
				}

				toSuggestions = await blaise.SearchAsync(To.Text, 10);
				if(token.IsCancellationRequested) return;

				vm.Suggestions.Clear();
				foreach(var result in toSuggestions)
				{
					vm.Suggestions.Add(result);
				}
			}
			catch(TaskCanceledException)
			{
			}
		}

		private void To_Focused(object sender, FocusEventArgs e)
		{
			fromFocused = false;
			toFocused = true;

			vm.Suggestions.Clear();
			foreach(var result in toSuggestions)
			{
				vm.Suggestions.Add(result);
			}
		}

		private void RemoveTo_Clicked(object sender, EventArgs e)
		{
			vm.ToArea = null;
			To.Text = string.Empty;
			toSuggestions.Clear();
			vm.Suggestions.Clear();
			UpdateFindRoute();
		}

		private void SuggestionTapped(object sender, TappedEventArgs e)
		{
			if(e.Parameter is Models.Location area)
			{
				if(fromFocused)
				{
					vm.FromArea = area;
					From.Text = string.Empty;
					From.Unfocus();
				}
				else if(toFocused)
				{
					vm.ToArea = area;
					To.Text = string.Empty;
					To.Unfocus();
				}

				vm.Suggestions.Clear();
				UpdateFindRoute();
			}
		}

	private void SwapStations_Clicked(object sender, EventArgs e)
	{
		// Only swap if both stations are selected
		if (vm.FromArea == null || vm.ToArea == null)
			return;

		// Swap the stations
		var temp = vm.FromArea;
		vm.FromArea = vm.ToArea;
		vm.ToArea = temp;

		// Swap the suggestion lists
		var tempList = fromSuggestions;
		fromSuggestions = toSuggestions;
		toSuggestions = tempList;

		UpdateFindRoute();
	}

		private void UpdateFindRoute()
		{
			FindRoute.IsEnabled = vm.CanSearch;
		}

		private async void FindRoute_Clicked(object sender, EventArgs e)
		{
			if(!vm.CanSearch) return;

			await Shell.Current.GoToAsync($"{nameof(RoutePage)}?from={vm.FromArea!.Id}&to={vm.ToArea!.Id}&time={vm.Time}&is_departure={vm.IsDeparture}");
		}
	}
}
