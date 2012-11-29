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

			try
			{
				request.GetResponse();
			}
			catch(Exception)
			{
				Console.WriteLine("Unable to connect to " + URL + " to send message " + message);
			}
		}

		public static string GetString(string URL)
		{
			WebRequest request = WebRequest.Create(URL);

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			try
			{
				Stream stream = response.GetResponseStream();
				return stream.ToString();
			}
			catch(Exception)
			{
				Console.WriteLine("Unable to connect to " + URL + " to get message");
				return "";
			}

		}
	}
}

