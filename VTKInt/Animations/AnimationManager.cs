using System;
using System.Collections.Generic;
using OpenTK;

namespace VTKInt.Animations
{
	public enum AnimationType
	{
		Press,
		Jump,
		Spin,
		RollUp,
		RollDown
	}

	public class AnimationManager
	{
		static List<Animation> animations = new List<Animation>();



		public AnimationManager ()
		{

		}

		public static void Add(AnimationType type, IAnimatable animatee, float length = 1.5f)
		{
			if(animatee.IsAnimatedWith(type))
				return;

			Animation animation = new Animation(animatee);
			animation.Type = type;
			animation.Length = length;

			switch(type)
			{
			case AnimationType.Press : 
			{

				animation.AddFrame(new AnimFrame(animatee.Animated.Position));
				animation.AddFrame(new AnimFrame(animatee.Animated.Position - Vector3.UnitZ / 3.0f));
				animation.AddFrame(new AnimFrame(animatee.Animated.Position));

				break;
			}
			case AnimationType.Spin :
			{

				animation.AddFrame(new AnimFrame(animatee.Animated.Orientation));
				animation.AddFrame(new AnimFrame(animatee.Animated.Orientation * Quaternion.FromAxisAngle(Vector3.UnitY, (float)Math.PI)));
				animation.AddFrame(new AnimFrame(animatee.Animated.Orientation * Quaternion.FromAxisAngle(Vector3.UnitY, (float)Math.PI * 2.0f)));
				

				break;
			}
			case AnimationType.Jump :
			{

				animation.AddFrame(new AnimFrame(animatee.Animated.Position));
				animation.AddFrame(new AnimFrame(animatee.Animated.Position + new Vector3(0.0f, -2.0f, 0.0f)));
				animation.AddFrame(new AnimFrame(animatee.Animated.Position));

				break;
			}
			case AnimationType.RollUp :
			{

				animation.AddFrame(new AnimFrame(animatee.Animated.Position));
				animation.AddFrame(new AnimFrame(new Vector3(0.0f, 1.0f, 4.0f)));

				break;
			}
			case AnimationType.RollDown :
			{

				animation.AddFrame(new AnimFrame(animatee.Animated.Position));
				animation.AddFrame(new AnimFrame(new Vector3(0.0f, -10.0f, 0.0f)));

				break;
			}

			}

			animations.Add(animation);

			animation.Start();

			return;
		}



		public static void Update()
		{
			foreach(Animation anim in animations)
			{
				anim.Update();
			}
		
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

