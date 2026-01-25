using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiPascal.ViewModels;


public partial class MainPageViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<Models.Location> suggestions;

	[ObservableProperty]
	private Models.Location? fromArea = null;

	[ObservableProperty]
	private Models.Location? toArea = null;

	[ObservableProperty]
	private bool isDeparture = true;
	[ObservableProperty]
	private bool isArrival = false;

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
