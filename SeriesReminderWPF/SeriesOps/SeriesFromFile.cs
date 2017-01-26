using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using SeriesReminderWPF;
using System.Collections.ObjectModel;

namespace SeriesReminderWPF
{
	class SeriesFromFile
	{
		public ObservableCollection<Series> LoadSeriesFromFile()
		{
			string jsonData = File.ReadAllText("series.json");

			return JsonConvert.DeserializeObject<ObservableCollection<Series>>(jsonData);
		}

		public void SaveSeriesToFile(ObservableCollection<Series> seriesData)
		{
			string outputJson = JsonConvert.SerializeObject(seriesData);
			File.WriteAllText("series.json", outputJson);		
		}
	}
}
