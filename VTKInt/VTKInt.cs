using System;
using OpenTK;

namespace VTKInt
{
	public class VTKInt : OpenTK.GameWindow
	{
		public VTKInt ()
		{
		}
	
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
		}

		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);
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

