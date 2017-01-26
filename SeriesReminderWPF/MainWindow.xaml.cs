using SeriesReminderWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeriesReminderWPF
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		SeriesFromFile seriesFileReader = new SeriesFromFile();
		SeriesChecker seriesChecker = new SeriesChecker(new RealWebRequest());
		private string searchedSeries;
		public string SearchedSeries
		{
			get { return searchedSeries; }
			set { searchedSeries = value; }
		}

		public ObservableCollection<Series> allSeries = new ObservableCollection<Series>();
		public MainWindow()
		{
			InitializeComponent();

			if (File.Exists("series.json"))
			{
				allSeries = seriesFileReader.LoadSeriesFromFile();
			}

			lblError.DataContext = seriesChecker;
			txtSeries.DataContext = this;
			dgSeries.ItemsSource = allSeries;
		}

		private void btn_findSeries_Click(object sender, RoutedEventArgs e)
		{
			Series series = new Series(searchedSeries);
			string seriesData = String.Empty;
			if (seriesChecker.FindSeries(searchedSeries, out seriesData))
			{
				series.FillSeriesProperties(seriesData);

				// adding new Series obj to Series collection
				if (!allSeries.Any(d => d.Name == series.Name))
				{
					allSeries.Add(series);
				}
				else
				{
					seriesChecker.LastErrorMessage = "Series already added";
				}
			}
			txtSeries.SelectAll();
		}

		private readonly BackgroundWorker worker = new BackgroundWorker();
		private void btn_Refresh_Click(object sender, RoutedEventArgs e)
		{
			var seriesToUpdate = allSeries.Where(s => s.Name == "Berlin Station");
			string seriesData = String.Empty;
			foreach (var series in allSeries)
			{
				if (seriesChecker.CheckDateAgain(series, out seriesData))
				{
					series.FillSeriesProperties(seriesData);
				}
			}
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			seriesFileReader.SaveSeriesToFile(allSeries);
		}


	}
}
