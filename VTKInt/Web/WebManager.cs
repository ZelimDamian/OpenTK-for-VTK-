using System;

namespace VTKInt.Web
{
	public class WebManager
	{
		public WebManager ()
		{
		}

		public static string LocalHostStateFile = "http://localhost/~Zelom/VTKTest/storeState.php";

		public static void SendMessage(string message)
		{
			WebClient.SendMessage(LocalHostStateFile, message);
		}
	}
}

