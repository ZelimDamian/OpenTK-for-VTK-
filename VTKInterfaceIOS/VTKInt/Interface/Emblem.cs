using System;
using OpenTK;
using VTKInt.Models;
using VTKInt.Structues;

namespace VTKInt.Interface
{
	public class Emblem : Model, ITouchable
	{
		public Emblem ()
		{

		}

		public override void Update ()
		{
			Orientation = Orientation * Quaternion.FromAxisAngle(Vector3.UnitY, SceneManager.FrameTime * (float)Math.PI / 2.0f);
			base.Update ();
		}

		public void Touch(Ray ray)
		{

		}
	}
}

