using System;
using VTKInt.Structues;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class NumpadComponent : Component, IAnimatable, ITouchable
	{
		public NumpadComponent (string meshName) : base(meshName)
		{
		}

		Numpad numPad;
		
		public Numpad Numpad
		{
			get { return numPad; }
			set { numPad = value; }
		}

		public virtual void Touch(Ray ray)
		{

		}

		public virtual void React()
		{

		}

		Animation animation;
		
		public virtual VTKObject Animated
		{
			get { return this; }
			set {}
		}
		
		public virtual Animation Animation
		{
			get { return animation; }
			set { animation = value; }
		}
		
		public virtual bool IsAnimated
		{
			get { return animation != null; }
		}
	}
}

