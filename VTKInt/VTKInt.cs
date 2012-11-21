using System;
using System.Threading;
using OpenTK;

namespace VTKInt
{
	public class VTKInt : OpenTK.GameWindow
	{
		public VTKInt ()
		{
			SceneManager.Window = this;
		}
	
		protected override void OnLoad (EventArgs e)
		{
			SceneManager.Scene.Load();

			base.OnLoad (e);
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			SceneManager.FrameTime = (float) e.Time;
			SceneManager.RunningTime += SceneManager.FrameTime;
			SceneManager.Scene.Render();

			OpenTK.Graphics.OpenGL.GL.Flush();
			Thread.Sleep(1);
			base.OnRenderFrame (e);
		}

		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			SceneManager.Scene.Update();
			base.OnUpdateFrame (e);
		}

		[STAThread]
		public static void Main()
		{
			using(VTKInt app = new VTKInt())
			{
				app.Run(30.0, 30.0);
			}
		}
	}
}

