using System;
using OpenTK;
using VTKInt.Models;

namespace VTKInt.Interface
{
	public class Emblem : Model
	{
		public Emblem ()
		{

		}

		public override void Update ()
		{
			Orientation = Orientation * Quaternion.FromAxisAngle(Vector3.UnitY, SceneManager.FrameTime * (float)Math.PI / 2.0f);
			base.Update ();
		}
	}
}

