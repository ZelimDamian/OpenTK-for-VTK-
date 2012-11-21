using System;
using OpenTK;

namespace VTKInt.Cameras
{
	public class CameraFPS : Camera
	{
		public CameraFPS () : base()
		{

		}

		private OpenTK.Input.KeyboardDevice Key
		{
			get { return SceneManager.Window.Keyboard; }
		}

		public override void Update ()
		{
			if(Key[OpenTK.Input.Key.W])
			{
				Eye += Forward * SceneManager.FrameTime;
				Origin += Forward * SceneManager.FrameTime;
			}
			else if(Key[OpenTK.Input.Key.S])
			{
				Eye -= Forward * SceneManager.FrameTime;
				Origin -= Forward * SceneManager.FrameTime;
			}
			else if(Key[OpenTK.Input.Key.D])
			{
				Eye += Right * SceneManager.FrameTime;
				Origin += Right * SceneManager.FrameTime;
			}
			else if(Key[OpenTK.Input.Key.A])
			{
				Eye -= Right * SceneManager.FrameTime;
				Origin -= Right * SceneManager.FrameTime;
			}

			base.Update ();
		}
	}
}

