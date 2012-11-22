using System;
using OpenTK;

namespace VTKInt.Cameras
{
	public class CameraFPS : Camera
	{
		float movementSpeed = 10.0f;

		public float MovementSpeed
		{
			get { return movementSpeed; }
			set { movementSpeed = value; }
		}

		public CameraFPS () : base()
		{

		}

		public void UpdateMouse()
		{
			float xDelta = (float)(SceneManager.Window.Mouse.X - mouseXOld);
			float yDelta = (float)(SceneManager.Window.Mouse.Y - mouseYOld);

			if(SceneManager.Window.Mouse[OpenTK.Input.MouseButton.Left])
			{
				Origin = Origin - Up * yDelta * ySens;
				Origin = Origin + Right * xDelta * xSens;
			}

			mouseXOld = SceneManager.Window.Mouse.X;
			mouseYOld = SceneManager.Window.Mouse.Y;
		}

		private OpenTK.Input.KeyboardDevice Key
		{
			get { return SceneManager.Window.Keyboard; }
		}

		private void MoveByVector(Vector3 vector)
		{
			vector = vector * movementSpeed * SceneManager.FrameTime;

			Eye = Eye + vector;
			Origin = Origin + vector;
		}

		public override void Update ()
		{
			if(Key[OpenTK.Input.Key.W])
			{
				MoveByVector(Forward);
			}
			else if(Key[OpenTK.Input.Key.S])
			{
				MoveByVector(-Forward);
			}
			else if(Key[OpenTK.Input.Key.D])
			{
				MoveByVector(Right);
			}
			else if(Key[OpenTK.Input.Key.A])
			{
				MoveByVector(-Right);
			}

			UpdateMouse();

			base.Update ();
		}
	}
}

