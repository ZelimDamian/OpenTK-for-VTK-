using System;
using OpenTK;
using VTKInt.Structues;
using VTKInt.Web;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class BackwardButton: NumpadComponent
	{
		public BackwardButton (string meshName) : base(meshName)
		{
		}

		public override void WhenPressed ()
		{
			Numpad.Display.RemoveLastDigit();
			base.WhenPressed ();
		}
	}
}

