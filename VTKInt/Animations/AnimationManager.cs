using System;
using System.Collections.Generic;
using OpenTK;

namespace VTKInt.Animations
{
	public class AnimationManager
	{
		static List<Animation> animations = new List<Animation>();

		public enum AnimationType
		{
			Press,
			Spin
		}

		public AnimationManager ()
		{

		}

		public static Animation Add(AnimationType type, IAnimatable animatee)
		{
			switch(type)
			{
				case AnimationType.Press : 
				{
					Animation animation = new Animation(animatee, 0.5f);

					animation.AddFrame(new AnimFrame(animatee.Animated.Position));
					animation.AddFrame(new AnimFrame(animatee.Animated.Position - Vector3.UnitZ / 3.0f));
					animation.AddFrame(new AnimFrame(animatee.Animated.Position));

					animation.Start();

					animations.Add(animation);
					return animation;
				}
				case AnimationType.Spin :
				{
					Animation animation = new Animation(animatee, 3.0f);

					animation.AddFrame(new AnimFrame(animatee.Animated.Orientation));
					animation.AddFrame(new AnimFrame(animatee.Animated.Orientation * Quaternion.FromAxisAngle(Vector3.UnitY, (float)Math.PI)));

					animatee.Animation = animation;

					animation.Start();
					
					animations.Add(animation);
					return animation;
				}
			}

			return null;
		}

		public static void Update()
		{
			foreach(Animation anim in animations)
			{
				anim.Update();
			}

//			foreach(Animation anim in animations)
//			{
//				if(anim.Finished)
//					animations.Remove(anim);
//			}

			animations.ForEach(delegate(Animation obj) {
				if(obj.Finished)
				{
					obj.Dispose();
					animations.Remove(obj);
				}
			});
		}
	}
}

