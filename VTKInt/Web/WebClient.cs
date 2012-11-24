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
	}
}

