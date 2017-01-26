using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesReminderWPF
{
	public interface IWebRequest
	{
		bool GetWebResponse(string url, out string answer, out string error);
	}
}
