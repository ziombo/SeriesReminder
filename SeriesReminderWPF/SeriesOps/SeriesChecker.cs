using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel;

namespace SeriesReminderWPF
{
	public class SeriesChecker : INotifyPropertyChanged
	{
		private IWebRequest webRequest;
		private string lastErrorMessage;
		public string LastErrorMessage
		{
			get { return lastErrorMessage; }
			set
			{
				lastErrorMessage = value;
				OnPropertyChanged("LastErrorMessage");
			}
		}

		public SeriesChecker(RealWebRequest WebRequest)
		{
			webRequest = WebRequest;
		}

		public bool FindSeries(string searchedSeries, out string webResult)
		{
			bool success = true;
			string url = "http://api.tvmaze.com/singlesearch/shows?q=" + searchedSeries + "&embed=nextepisode";
			string error;

			success = webRequest.GetWebResponse(url, out webResult, out error);
			LastErrorMessage = error; //workaround to somehow fire PropertyChanged event for binding bcs. cant out properties

			return success;
		}

		public bool CheckDateAgain(Series series, out string webResult)
		{
			bool success = true;
			string url = "http://api.tvmaze.com/singlesearch/shows?q=" + series.SearchedName + "&embed=nextepisode";
			string error;

			success = webRequest.GetWebResponse(url, out webResult, out error);
			LastErrorMessage = error; //workaround to somehow fire PropertyChanged event for binding bcs. cant out properties

			return success;
		}

		#region PropertyChanged
		// Create the OnPropertyChanged method to raise the event 
		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
