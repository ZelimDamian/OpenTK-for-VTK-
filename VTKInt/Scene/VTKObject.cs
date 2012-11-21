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

		Vector3 position;
		Vector3 scale;
		Quaternion orientation;
		Matrix4 transform; 

		public Vector3 Position
		{
			get { return position; }
			set {
				if(position == value)
					return;
				position = value;
				UpdateTransform();
			}
		}

		public Vector3 Scale
		{
			set {
				if(scale == value)
					return;
				scale = value;
				UpdateTransform();
			}
			get { return scale; }
		}

		public Quaternion Orientation
		{
			get { return orientation; }
			set {
				if(orientation == value )
					return;
				orientation = value;
				UpdateTransform();
			}
		}

		public Matrix4 Transform
		{
			get { return transform; }
			set { transform = value; }
		}

		public void UpdateTransform()
		{
			transform = Matrix4.CreateTranslation(position) * Matrix4.Rotate(orientation) * Matrix4.Scale(scale);
		}
	}
}

