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
			if(SceneManager.Window.Mouse[OpenTK.Input.MouseButton.Right])
				Touch(SceneManager.GetMouseRay());
			base.Update ();
		}

		public void Touch(Ray ray)
		{
			foreach(Component comp in components)
			{
				BoundingBox curBox = new BoundingBox(comp.Box);
				
				curBox.Center = Vector3.Transform(curBox.Center, this.Transform);
				curBox.Scale(this.scale);
				
				Vector3? intersection = ray.GetIntersectionPoint(curBox);
				
				if(intersection != null)
				{
					React();
					return;
				}
			}
		}

		public void React()
		{
			if(!IsAnimated)
			{
				AnimationManager.Add(AnimationManager.AnimationType.Spin, this);
			}
		}
	}
}

