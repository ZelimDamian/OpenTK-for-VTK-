using System;
using OpenTK;
using VTKInt.Structues;
using VTKInt.Web;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class ForwardButton: NumpadComponent
	{
		public ForwardButton (string meshName) : base(meshName)
		{
		}

		public override void Touch(Ray ray)
		{

		}

		public override void React()
		{
			if(IsAnimated)
				return;

			AnimationManager.Add(AnimationManager.AnimationType.Press, this);

			TransactionsManager.SendNumber(Numpad.Display.Digits);
		}
	}
}

