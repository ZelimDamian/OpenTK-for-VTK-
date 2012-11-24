using System;
using OpenTK;
using VTKInt.Structues;
using VTKInt.Animations;
using VTKInt.Web;

namespace VTKInt.Interface
{
	public class Numpad : Models.Model, ITouchable
	{
		public Numpad (string materialName, float width, float height)
		{
			AddMaterial(Materials.MaterialLoader.GetMaterial(materialName));
			
			for(int i = 1; i <= 10; i++)
			{
				string meshName = "";
				
				if(i < 10)
					meshName += i;
				else
					meshName += 0;
				
				meshName += ".obj";
				
				Component comp = new DigitComponent(meshName);
				
				Vector3 pos = new Vector3( (float)((i-1) % 3) * width / 3.0f, (float)Math.Floor((float)(i-1) / 3.0f) * height / 4.0f, 0.0f );
				
				if(i == 10)
					pos.X = 0.0f;
				
				comp.Position = pos;

				this.Position = Vector3.UnitY * 4.0f;

				AddComponent(comp);
			}
		}
		
		public override void Update ()
		{
			if(SceneManager.Window.Mouse[OpenTK.Input.MouseButton.Left])
				Touch(SceneManager.GetMouseRay());

			base.Update ();
		}

		public bool IsPressed
		{
			get {

				foreach(Component comp in components)
				{
					try{
						if(((DigitComponent)comp).IsAnimated)
							return true;
					}
					catch(Exception) {}
				}

				return false;
			}
		}

		public void Touch(Ray ray)
		{
			if(IsPressed)
				return;

			foreach(Component comp in components)
			{
				if(!(comp is DigitComponent))
					return;

				BoundingBox curBox = new BoundingBox(comp.Box);
				
				curBox.Center = Vector3.Transform(curBox.Center, this.Transform);
				
				Vector3? intersection = ray.GetIntersectionPoint(curBox);
				
				if(intersection != null)
				{
					AnimationManager.Add(AnimationManager.AnimationType.Press, (DigitComponent)comp);
					WebManager.SendMessage(comp.Name);
					return;
				}
			}
		}
	}
}