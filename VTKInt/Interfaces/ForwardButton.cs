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

		public override void WhenPressed ()
		{
			TransactionsManager.SendNumber(Numpad.Display.Digits);
			base.WhenPressed ();
		}
	}
}

