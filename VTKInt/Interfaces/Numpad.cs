using System;
using OpenTK;
using VTKInt.Structues;
using VTKInt.Animations;
using VTKInt.Web;

namespace VTKInt.Interface
{
	public class Numpad : Models.Model, ITouchable
	{
		DigitDisplay display;
		float width, height, distToDisplay;

		public Numpad (string materialName, float width, float height)
		{
			this.width = width;
			this.height = height;
			this.distToDisplay = 1.0f;

			AddMaterial(Materials.MaterialLoader.GetMaterial(materialName));

			display = new DigitDisplay(0.5f, materialName);

			for(int i = 1; i <= 10; i++)
			{
				string meshName = "";
				
				if(i < 10)
					meshName += i;
				else
					meshName += 0;
				
				meshName += ".obj";
				
				DigitComponent comp = new DigitComponent(meshName);

				comp.Numpad = this;

				Vector3 pos = new Vector3( - this.width / 2.0f, 0.0f, 0.0f);

				pos += new Vector3( (float)((i-1) % 3) * width / 3.0f, height - (float)Math.Floor((float)(i-1) / 3.0f) * height / 4.0f, 0.0f );
				
				if(i == 10)
					pos.X =  - this.width / 2.0f + (float)((2-1) % 3) * width / 3.0f ;
				
				comp.Position = pos;

				this.Position = Vector3.UnitY * 4.0f;

				this.Scale = new Vector3(3.0f, 3.0f, 3.0f);

				this.Orientation *= Quaternion.FromAxisAngle(Vector3.UnitX, 0.03f);

				AddComponent(comp);

				CastShadows = true;
			}

			NumpadComponent forwards = new ForwardButton("ForwardButton.obj");
			forwards.Numpad = this;
			forwards.Position = width * 0.25f * Vector3.UnitX + Vector3.UnitY * 0.5f;
			AddComponent(forwards);

			NumpadComponent backwards = new BackwardButton("BackwardButton.obj");
			backwards.Numpad = this;
			backwards.Position = -width * 0.45f * Vector3.UnitX + Vector3.UnitY * 0.5f;
			AddComponent(backwards);
		}
		
		public override void Update ()
		{
			if(SceneManager.Window.Mouse[OpenTK.Input.MouseButton.Left])
				Touch(SceneManager.GetMouseRay());

			this.display.Position = Vector3.Transform((this.height + distToDisplay) * Vector3.UnitY, this.Transform);

			base.Update ();
		}

		public bool IsPressed
		{
			get {

				foreach(Component comp in components)
				{
					try{
						if(((NumpadComponent)comp).IsAnimated)
							return true;
					}
					catch(Exception) {}
				}

				return false;
			}
		}

		public override void Render ()
		{
			display.Render();

			base.Render ();
		}

		public void Touch(Ray ray)
		{
			if(IsPressed)
				return;

			foreach(Component comp in components)
			{
				if(!(comp is NumpadComponent))
					return;

				BoundingBox curBox = new BoundingBox(comp.Box);
				
				curBox.Center = Vector3.Transform(curBox.Center, this.Transform);
				curBox.Scale(this.scale);

				Vector3? intersection = ray.GetIntersectionPoint(curBox);
				
				if(intersection != null)
				{
					((ITouchable)comp).React();
					return;
				}
			}
		}

		public void Forward()
		{

		}

		public void React()
		{}

		public DigitDisplay Display
		{
			get { return display; }
			set { display = value; }
		}

		public override Vector3 Scale {
			get {
				return base.Scale;
			}
			set {
				this.display.Scale = value;
				base.Scale = value;
			}
		}
	}
}