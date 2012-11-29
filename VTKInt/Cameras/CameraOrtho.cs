using System;
using OpenTK;

namespace VTKInt.Cameras
{
	public class CameraOrtho : Camera
	{
		public CameraOrtho ()
		{
		}

		
		public override void UpdateViewMatrix()
		{
			this.view = Matrix4.LookAt(eye, origin, up);
		}
		
		public override void UpdateProjMatrix()
		{
			this.projection = Matrix4.CreateOrthographicOffCenter(0.0f, SceneManager.Window.Width, 0.0f, SceneManager.Window.Height, -1.0f, 100.0f);
		}
	}
}

