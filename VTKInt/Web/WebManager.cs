using System;

namespace VTKInt.Web
{
	public class WebManager
	{
		public WebManager ()
		{
		}

		public static string LocalHostStoreState = "http://localhost/~Zelom/VTKTest/storeState.php";
		public static string LocalHostFetchState = "http://localhost/~Zelom/VTKTest/getState.php";

		public static void SendMessage(string message)
		{
			WebClient.SendMessage(LocalHostStoreState, message);
		}

		public static String GetMessage()
		{
			return WebClient.GetString(LocalHostFetchState);
		}
	}
}

