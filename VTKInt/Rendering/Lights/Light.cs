using System;
using OpenTK;

namespace VTKInt.Lights
{
	public class Light
	{
		Vector3 position;
		
		public Vector3 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Light ()
		{
		}
	}
}

