using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPascal.Models;
using System.Collections.ObjectModel;

namespace MauiPascal.ViewModels;


public partial class MainPageViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<Area> suggestions;

	[ObservableProperty]
	private Area? fromArea = null;

	[ObservableProperty]
	private Area? toArea = null;

	[ObservableProperty]
	private string hour = string.Empty;

	[ObservableProperty]
	private string minute = string.Empty;

	[ObservableProperty]
	private string second = string.Empty;

	public string Time => $"{Hour}:{Minute}:{Second}";

	private bool AreasSet => FromArea != null && ToArea != null;

	public MainPageViewModel()
	{
		SetTimeToNow();
		suggestions = [];
	}

	[RelayCommand]
	private void SetTimeToNow()
	{
		var now = DateTime.Now;
		Hour = now.Hour.ToString();
		Minute = now.Minute.ToString();
		Second = now.Second.ToString();
	}
}
