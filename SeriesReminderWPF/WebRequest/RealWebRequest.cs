using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeriesReminderWPF
{
	public class RealWebRequest : IWebRequest
	{
		public bool GetWebResponse(string url, out string webRequestResult, out string webRequestError)
		{
			webRequestError = webRequestResult = null;
			bool success = true;
			try
			{
				HttpWebRequest request = WebRequest.CreateHttp(url);
				WebResponse response = request.GetResponse();

				using (Stream responseStream = response.GetResponseStream())
				using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
				{
					webRequestResult = reader.ReadToEnd();
				}
			}
			catch (WebException ex)
			{
				success = false;
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					var response = ex.Response as HttpWebResponse;
					if (response != null)
					{
						webRequestError = response.StatusCode.ToString();
					}
					else
					{
						webRequestError = "An unknown error has occured";
					}
				}
				else
				{
					webRequestError = "An unknown error has occured";
				}
			}

			return success;
		}
	}
}
