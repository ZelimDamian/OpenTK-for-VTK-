using System;
using OpenTK;

namespace VTKInt.Lights
{
	public class Light : Cameras.CameraFPS
	{
		public Light () : base()
		{
			this.Origin = new Vector3(40.0f, 0.0f, 30.0f);
			this.Eye = new Vector3(45.0f, 10.0f, 20.0f);

			UpdateViewMatrix();
			UpdateProjMatrix();
		}
	}
}

