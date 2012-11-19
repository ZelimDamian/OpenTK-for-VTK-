using System;
using OpenTK;

namespace VTKInt
{
	public class VTKInt : OpenTK.GameWindow
	{
		public VTKInt ()
		{
		}
	
		public static void Main()
		{
			using(VTKInt app = new VTKInt())
			{
				app.Run(30.0, 30.0);
			}
		}
	}
}

