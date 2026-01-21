using MauiPascal.Models;
using MauiPascal.Service;
using MauiPascal.ViewModels;
using System.Diagnostics;

namespace MauiPascal
{
	public partial class MainPage : ContentPage
	{
		private MainPageViewModel vm;
		private readonly BlaiseService blaise;
		private bool fromFocused = true;
		private bool toFocused = false;
		private List<Area> fromSuggestions = [];
		private List<Area> toSuggestions = [];

		public MainPage(BlaiseService blaise)
		{
			this.blaise = blaise;
			vm = new MainPageViewModel();
			BindingContext = vm;
			InitializeComponent();
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
				await Task.Delay(150, token);
				fromSuggestions = await blaise.SearchAsync(From.Text, 15);
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

			From.IsVisible = true;
			FromArea.IsVisible = false;
			vm.FromArea = null;
		}


		private async void To_TextChanged(object sender, TextChangedEventArgs e)
		{
			searchCts?.Cancel();
			searchCts = new CancellationTokenSource();
			var token = searchCts.Token;
			try
			{
				await Task.Delay(150, token);
				toSuggestions = await blaise.SearchAsync(To.Text, 20);
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
			To.IsVisible = true;
			ToArea.IsVisible = false;
			vm.ToArea = null;
		}

		private void SuggestionTapped(object sender, TappedEventArgs e)
		{
			if(e.Parameter != null && e.Parameter is Area)
			{
				var area = (Area)e.Parameter;

				if(fromFocused)
				{
					vm.FromArea = area;
					From.Text = area.ToString();
					From.IsVisible = false;
					FromArea.IsVisible = true;
					FromAreaText.Text = area.ToString();
				}
				else if(toFocused)
				{
					vm.ToArea = area;
					To.Text = area.ToString();
					To.IsVisible = false;
					ToArea.IsVisible = true;
					ToAreaText.Text = area.ToString();
				}
				UpdateFindRoute();
			}

		}


		private void UpdateFindRoute()
		{
			FindRoute.IsEnabled = vm.FromArea != null && vm.ToArea != null;
		}

		private void FindRoute_Clicked(object sender, EventArgs e)
		{
			Trace.WriteLine($"From {vm.FromArea!.Id} to {vm.ToArea!.Id} @ {vm.Time}");
		}
	}
}
