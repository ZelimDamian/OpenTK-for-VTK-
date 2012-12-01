using System;
using OpenTK;

namespace VTKInt.Lights
{
	public class Light : Cameras.CameraFPS
	{
		public Light () : base()
		{
		}

		public override Vector3 Position {
			get {
				return Eye;
			}
			set {
				Eye = value;
				base.Position = value;
			}
		}

		public override void Update ()
		{

		}
	}
}

