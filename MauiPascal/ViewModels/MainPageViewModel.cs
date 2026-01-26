using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiPascal.ViewModels;


public partial class MainPageViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<Models.Location> suggestions;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CanSearch))]
	private Models.Location? fromArea = null;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CanSearch))]
	private Models.Location? toArea = null;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsArrival))]
	private bool isDeparture = true;

	public bool IsArrival
	{
		get => !isDeparture;
		set
		{
			if (value)
			{
				IsDeparture = false;
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Time))]
	private TimeSpan selectedTime;

	public string Time => selectedTime.ToString(@"hh\:mm\:ss");

	public bool CanSearch => FromArea != null && ToArea != null;

	public MainPageViewModel()
	{
		SetTimeToNow();
		suggestions = [];
	}

	[RelayCommand]
	private void SetTimeToNow()
	{
		var now = DateTime.Now;
		SelectedTime = now.TimeOfDay;
	}
}
