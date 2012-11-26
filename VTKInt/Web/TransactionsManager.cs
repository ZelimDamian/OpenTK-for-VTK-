using System;
using OpenTK;
using VTKInt.Interface;

namespace VTKInt.Web
{
	public class TransactionsManager
	{
		public TransactionsManager ()
		{
		}

		public static void SendNumber(string number)
		{
			WebManager.SendMessage(number);
		}
	}
}

