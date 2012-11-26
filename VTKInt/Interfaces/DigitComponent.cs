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

		public override void Touch(Ray ray)
		{

		}

		public override void React()
		{
			AnimationManager.Add(AnimationManager.AnimationType.Press, this);
			Numpad.Display.AddDigit(this.NameClean);
		}
	}
}

