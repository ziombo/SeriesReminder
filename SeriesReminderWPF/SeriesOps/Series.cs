using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SeriesReminderWPF
{
	public class Series
	{
		private string searchedName; // If user changes the name of series this will remain unchanged for web requests
		private string name;
		private string status;
		private string nextEpisodeDate;
		private int? daysLeft;

		public string SearchedName
		{
			get { return searchedName; }
			set { searchedName = value; }
		}
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (value.Length > 0 && value.Length < 255)
				{
					name = value;
				}

			}
		}
		public string Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}
		public string NextEpisodeDate
		{
			get
			{
				return nextEpisodeDate;
			}
			set
			{
				nextEpisodeDate = value;
			}
		}
		public int? DaysLeft
		{
			get
			{
				return daysLeft;
			}

			set
			{
				daysLeft = value;
			}
		}

		public Series(string userInput)
		{
			searchedName = userInput;
		}

		public void FillSeriesProperties(string seriesData)
		{
			dynamic seriesDataParsed = JObject.Parse(seriesData);

			name = seriesDataParsed.name;
			status = seriesDataParsed.status;

			if(seriesDataParsed._embedded != null)
			{
				nextEpisodeDate = seriesDataParsed._embedded.nextepisode.airdate;
				daysLeft = (int)DateTime.Parse(nextEpisodeDate).Subtract(DateTime.Today).TotalDays;
			}
			else if(status == "Running")
			{
				nextEpisodeDate = "Unknown";
				daysLeft = null;
			}
			else
			{
				daysLeft = null;
			}
		}
	}
}
