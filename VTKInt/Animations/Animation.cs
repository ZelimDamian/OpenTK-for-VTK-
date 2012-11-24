using System;
using System.Collections.Generic;
using OpenTK;

namespace VTKInt.Animations
{
	public class Animation
	{
		public List<Vector3> frames = new List<Vector3>();

		IAnimatable animatee;

		float length;
		float startTime;

		bool running = false;

		public Animation (IAnimatable animatee, List<Vector3> frames, float length)
		{
			this.animatee = animatee;
			animatee.Animation = this;
			this.frames = frames;
			this.length = length;
		}

		public Animation(IAnimatable animatee, float length)
		{
			this.animatee = animatee; //new Animatee(animated, this);
			animatee.Animation = this; 
			this.length = length;
		}

		public void AddFrame(Vector3 frame)
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
			animatee.Animation = null;
		}

		public bool Finished
		{
			get {
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

				animatee.Animated.Position = Vector3.Lerp(frames[(int)index], frames[(int)index + 1], lerp);
			}
		}
	}
}

