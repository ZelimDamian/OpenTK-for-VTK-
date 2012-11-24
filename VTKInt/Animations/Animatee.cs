using System;
using OpenTK;
using VTKInt;

namespace VTKInt.Animations
{
	public class Animatee : IAnimatable
	{
		Animation animation;
		VTKObject animated;

		public Animatee (VTKObject animated, Animation animation)
		{
			this.animation = animation;
			this.animated = animated;
		}

		public Animation Animation
		{
			get { return animation; }
			set { animation = value; }
		}

		public VTKObject Animated
		{
			get { return animated; }
			set { animated = value; }
		}

		public bool IsAnimated
		{
			get { return animation != null; }
		}
	}
}

