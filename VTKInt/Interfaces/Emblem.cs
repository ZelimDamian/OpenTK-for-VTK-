using System;
using OpenTK;
using VTKInt.Models;
using VTKInt.Structues;
using VTKInt.Animations;

namespace VTKInt.Interface
{
	public class Emblem : Model, ITouchable
	{
		public Emblem ()
		{
			CastShadows = true;
		}

		public override void Update ()
		{
			if(SceneManager.Window.Mouse[OpenTK.Input.MouseButton.Right])
				Touch(SceneManager.GetMouseRay());

				AnimationManager.Add(AnimationType.Jump, this);

			base.Update();
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
			AnimationManager.Add(AnimationType.Spin, this);
		}
	}
}

