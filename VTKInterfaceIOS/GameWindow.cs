using System;
using OpenTK;
using VTKInterfaceIOS;

using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;
using MonoTouch.UIKit;

namespace VTKInt
{
	public class GameWindow : EAGLView
	{
		public GameWindow (NSCoder coder) : base(coder)
		{
		}
	}
}

