using System;
using OpenTK;

namespace VTKInt
{
	public class VTKObject
	{
		public VTKObject ()
		{
		}

		public virtual void Render()
		{

		}

		public virtual void Update()
		{

		}

		protected Vector3 position = new Vector3();
		protected Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
		protected Quaternion orientation = Quaternion.Identity;
		protected Matrix4 transform = Matrix4.Identity; 

		public virtual Vector3 Position
		{
			get { return position; }
			set {
				if(position == value)
					return;

				position = value;
				UpdateTransform();
			}
		}

		public virtual Vector3 Scale
		{
			set {
				if(scale == value)
					return;
				scale = value;
				UpdateTransform();
			}
			get { return scale; }
		}

		public virtual Quaternion Orientation
		{
			get { return orientation; }
			set {
				if(orientation == value )
					return;
				orientation = value;
				UpdateTransform();
			}
		}

		public virtual Matrix4 Transform
		{
			get { return transform; }
			set { transform = value; }
		}

		public void UpdateTransform()
		{
			transform = Matrix4.Rotate(orientation) * Matrix4.Scale(scale) * Matrix4.CreateTranslation(position);
		}
	}
}

