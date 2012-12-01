using System;
using System.Collections.Generic;

namespace VTKInt.Animations
{
	public interface IAnimatable
	{
		bool IsAnimated
		{
			get;
		}

		bool IsAnimatedWith(AnimationType type);

		VTKObject Animated
		{
			get;
			set;
		}

		List<Animation> Animations
		{
			get;
			set;
		}
	}
}

