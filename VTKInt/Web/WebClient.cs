using System;
using System.Net;
using System.IO;

namespace VTKInt.Web
{
	public class WebClient
	{
		public WebClient ()
		{
		}

		public static void SendMessage(string URL, string message)
		{
			WebRequest request = WebRequest.Create(URL + "?message=" + message);

			request.GetResponse();
		}

		public static string GetString(string URL)
		{
			WebRequest request = WebRequest.Create(URL);

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			Stream stream = response.GetResponseStream();

			return stream.ToString();
		}
	}
}

