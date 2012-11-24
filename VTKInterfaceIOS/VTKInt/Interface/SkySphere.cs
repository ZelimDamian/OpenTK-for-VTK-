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
			Position = SceneManager.Camera.Eye;
			base.Update ();
		}
	}
}

