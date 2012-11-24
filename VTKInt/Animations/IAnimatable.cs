using System;

namespace VTKInt.Animations
{
	public interface IAnimatable
	{
		bool IsAnimated
		{
			get;
		}

		VTKObject Animated
		{
			get;
			set;
		}

		Animation Animation
		{
			get;
			set;
		}
	}
}

