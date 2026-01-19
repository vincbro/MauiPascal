using CommunityToolkit.Mvvm.ComponentModel;
using MauiPascal.Models;
using System.Collections.ObjectModel;

namespace MauiPascal.ViewModels;


public partial class MainPageViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<Area> fromSuggestions;
	[ObservableProperty]
	private ObservableCollection<Area> toSuggestions;

	[ObservableProperty]
	private Area? fromArea = null;

	[ObservableProperty]
	private Area? toArea = null;

	private bool AreasSet => FromArea != null && ToArea != null;

	public MainPageViewModel()
	{
		fromSuggestions = [];
		toSuggestions = [];
	}
}
