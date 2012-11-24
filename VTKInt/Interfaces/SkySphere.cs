using System;
using OpenTK;
using VTKInt.Models;

namespace VTKInt.Interface
{
	public class SkySphere : Model
	{
		public SkySphere ()
		{
		}

		public override void Update ()
		{
			Scale = new Vector3(10.0f, 10.0f, 10.0f);
			Position = SceneManager.Camera.Eye;
			base.Update ();
		}
	}
}

