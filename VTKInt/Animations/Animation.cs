using System;
using System.Collections.Generic;
using OpenTK;

namespace VTKInt.Animations
{
	public class Animation : IEquatable<Animation>
	{
		public List<AnimFrame> frames = new List<AnimFrame>();

		IAnimatable animatee;

		public IAnimatable Animatee
		{
			get { return animatee; }
			set { animatee = value; }
		}

		float length;

		public float Length
		{
			get { return this.length; }
			set { this.length = value; }
		}

		float startTime;

		public AnimationType Type;

		bool running = false;

		public Animation (IAnimatable animatee, List<AnimFrame> frames, float length)
		{
			this.animatee = animatee;
			animatee.Animations.Add(this);
			this.frames = frames;
			this.length = length;
		}

		public Animation(IAnimatable animatee, float length)
		{
			this.animatee = animatee; //new Animatee(animated, this);
			animatee.Animations.Add(this); 
			this.length = length;
		}

		public Animation(IAnimatable animatee)
		{
			this.animatee = animatee; //new Animatee(animated, this);
			animatee.Animations.Add(this); 
		}

		public void AddFrame(AnimFrame frame)
		{
			frames.Add(frame);
		}

		public void Start()
		{
			running = true;
			startTime = SceneManager.RunningTime;
		}

		public virtual void Dispose()
		{
			animatee.Animations.Remove(this);
		}

		public bool Finished
		{
			get {
				if(!animatee.Animations.Contains(this))
					return true;

				if(running)
					return CurrentTime >= length; 
				else return false;
			}
		}

		public float CurrentTime
		{
			get { return SceneManager.RunningTime - startTime; }
		}

		public void Update()
		{
			if(!Finished)
			{
				float index = (CurrentTime / length * (float)(frames.Count - 1));
				float lerp = index - (float)Math.Floor(index);

				AnimFrame current = frames[(int) index];
				AnimFrame next = frames[(int) index + 1];

				if(current.IsAnimatedPos && next.IsAnimatedPos)
					animatee.Animated.Position = Vector3.Lerp(current.Position, next.Position, lerp);
				if(current.IsAnimatedOrientation && next.IsAnimatedOrientation)
					animatee.Animated.Orientation = Quaternion.Slerp(current.Orientation, next.Orientation, lerp);
				if(current.IsAnimatedScale && current.IsAnimatedScale)
					animatee.Animated.Scale = Vector3.Lerp(current.Scale, next.Scale, lerp);
			}
		}

		public static bool Equals(Animation animA, Animation animB)
		{
			if(animA == null || animB == null)
				return false;
			return animA.Equals(animB);
		}

		public bool Equals(Animation anim)
		{
			if(anim == null)
				return false;

			return anim.Type == this.Type && anim.Animatee.Equals(Animatee);
		}
	}
}

