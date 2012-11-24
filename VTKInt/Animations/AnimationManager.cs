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
			Press
		}

		public AnimationManager ()
		{

		}

		public static Animation Add(AnimationType type, IAnimatable animatee)
		{
			Animation animation = new Animation(animatee, 2.0f);

			animation.AddFrame(animatee.Animated.Position);
			animation.AddFrame(animatee.Animated.Position - Vector3.UnitZ);
			animation.AddFrame(animatee.Animated.Position);

			animation.Start();

			animations.Add(animation);
			return animation;
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

