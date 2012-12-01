using System;
using VTKInt.Structues;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class DigitComponent : NumpadComponent
	{
		public DigitComponent (string meshName) : base(meshName)
		{
		}

		public override void WhenPressed ()
		{
			Numpad.Display.AddDigit(this.NameClean);
			base.WhenPressed ();
		}
	}
}

