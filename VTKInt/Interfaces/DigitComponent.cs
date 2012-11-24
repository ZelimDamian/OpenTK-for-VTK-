using System;
using VTKInt.Structues;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class DigitComponent : Component, IAnimatable
	{
		public DigitComponent (string meshName) : base(meshName)
		{
		}

		Animation animation;

		public VTKObject Animated
		{
			get { return this; }
			set {}
		}

		public Animation Animation
		{
			get { return animation; }
			set { animation = value; }
		}

		public bool IsAnimated
		{
			get { return animation != null; }
		}
	}
}

