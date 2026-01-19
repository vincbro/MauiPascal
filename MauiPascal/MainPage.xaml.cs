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
			vm.FromArea = null;
			UpdateFindRoute();
			searchCts?.Cancel();
			searchCts = new CancellationTokenSource();
			var token = searchCts.Token;

			try
			{
				await Task.Delay(150, token);
				var results = await blaise.SearchAsync(From.Text);
				if(token.IsCancellationRequested) return;
				vm.FromSuggestions.Clear();
				foreach(var result in results)
				{
					vm.FromSuggestions.Add(result);
				}
			}
			catch(TaskCanceledException)
			{
			}
		}
		private void FromSuggestionTapped(object sender, TappedEventArgs e)
		{
			if(e.Parameter != null && e.Parameter is Area)
			{
				var area = (Area)e.Parameter;
				From.Text = area.ToString();
				vm.FromArea = area;
				FromSuggestions.IsVisible = false;
				Trace.WriteLine($"From: {area.Name}");
				UpdateFindRoute();
			}
		}

		private void From_Focused(object sender, FocusEventArgs e)
		{
			FromSuggestions.IsVisible = true;
		}

		private void From_Unfocused(object sender, FocusEventArgs e)
		{
			FromSuggestions.IsVisible = false;
		}


		private async void To_TextChanged(object sender, TextChangedEventArgs e)
		{
			vm.ToArea = null;
			UpdateFindRoute();
			searchCts?.Cancel();
			searchCts = new CancellationTokenSource();
			var token = searchCts.Token;
			try
			{
				await Task.Delay(150, token);
				var results = await blaise.SearchAsync(To.Text);
				if(token.IsCancellationRequested) return;
				vm.ToSuggestions.Clear();
				foreach(var result in results)
				{
					vm.ToSuggestions.Add(result);
				}
			}
			catch(TaskCanceledException)
			{
			}
		}

		private void ToSuggestionTapped(object sender, TappedEventArgs e)
		{
			if(e.Parameter != null && e.Parameter is Area)
			{
				var area = (Area)e.Parameter;
				To.Text = area.ToString();
				vm.ToArea = area;
				ToSuggestions.IsVisible = false;
				Trace.WriteLine($"To: {area.Name}");
				UpdateFindRoute();

			}
		}

		private void To_Focused(object sender, FocusEventArgs e)
		{
			ToSuggestions.IsVisible = true;
		}

		private void To_Unfocused(object sender, FocusEventArgs e)
		{
			ToSuggestions.IsVisible = false;
		}

		private void UpdateFindRoute()
		{
			FindRoute.IsEnabled = vm.FromArea != null && vm.ToArea != null;
		}

		private void FindRoute_Clicked(object sender, EventArgs e)
		{

		}
	}
}
