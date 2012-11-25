using System;
using OpenTK;
using VTKInt.Models;
using VTKInt.Structues;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class Emblem : Model, ITouchable, IAnimatable
	{
		public Emblem ()
		{

		}

		Animation animation;

		public bool IsAnimated
		{
			get { return animation != null; }
		}

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

		public override void Update ()
		{
			if(!IsAnimated)
			{
				AnimationManager.Add(AnimationManager.AnimationType.Spin, this);
			}
			base.Update ();
		}

		public void Touch(Ray ray)
		{

		}
	}
}

