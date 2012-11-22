using System;
using OpenTK;

namespace VTKInt.Cameras
{
	public class CameraFPS : Camera
	{
		public CameraFPS () : base()
		{

		}

		public void UpdateMouse()
		{
			float xDelta = (float)(SceneManager.Window.Mouse.X - mouseXOld);
			float yDelta = (float)(SceneManager.Window.Mouse.Y - mouseYOld);

			if(SceneManager.Window.Mouse[OpenTK.Input.MouseButton.Left])
			{
				Origin = Origin + Up * yDelta * ySens;
				Origin = Origin + Right * xDelta * xSens;
			}

			mouseXOld = SceneManager.Window.Mouse.X;
			mouseYOld = SceneManager.Window.Mouse.Y;
		}

		private OpenTK.Input.KeyboardDevice Key
		{
			get { return SceneManager.Window.Keyboard; }
		}

		public override void Update ()
		{
			if(Key[OpenTK.Input.Key.W])
			{
				Eye = Eye + Forward * SceneManager.FrameTime;
				Origin = Origin + Forward * SceneManager.FrameTime;
			}
			else if(Key[OpenTK.Input.Key.S])
			{
				Eye = Eye - Forward * SceneManager.FrameTime;
				Origin = Origin - Forward * SceneManager.FrameTime;
			}
			else if(Key[OpenTK.Input.Key.D])
			{
				Eye = Eye + Right * SceneManager.FrameTime;
				Origin = Origin + Right * SceneManager.FrameTime;
			}
			else if(Key[OpenTK.Input.Key.A])
			{
				Eye = Eye - Right * SceneManager.FrameTime;
				Origin = Origin - Right * SceneManager.FrameTime;
			}

			UpdateMouse();

			base.Update ();
		}
	}
}

