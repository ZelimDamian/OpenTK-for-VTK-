using System;
using VTKInt.Structues;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class NumpadComponent : Component, ITouchable, IPressable
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
			Press();
		}
	
		//bool pressed;

		public bool IsPressed
		{
			set {}
			get { return SceneManager.RunningTime < PressTime + PressDuration; }
		}

		float pressTime;

		public float PressTime
		{
			set { pressTime = value; }
			get { return pressTime;  }
		}

		public virtual void Press()
		{
			if(!IsPressed)
			{
				AnimationManager.Add(AnimationType.Press, this, PressDuration);
				pressTime = SceneManager.RunningTime;
				WhenPressed();
			}
		}

		public virtual void WhenPressed()
		{

		}

		public void Release()
		{

		}

		float pressDuration = 0.5f;

		public float PressDuration
		{
			set { pressDuration = value; }
			get { return pressDuration;  }
		}

		public override void Update ()
		{
			base.Update ();
		}
	}
}

