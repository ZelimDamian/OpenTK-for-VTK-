using System;
using OpenTK;

namespace VTKInt.Cameras
{
	public class Camera : VTKObject
	{
		int mouseXOld, mouseYOld;

		Matrix4 view,
				projection;

		Vector3 origin,
				eye,
				up = Vector3.UnitY;

		public float 	fov,
						aspect,
						near,
						far;

		public Vector3 Origin
		{
			set { 
				if(origin == value)
					return;

				origin = value;
				UpdateViewMatrix();
			}
			get { return origin;}
		}

		public Vector3 Eye
		{
			set {
				if(eye == value)
					return;
				this.eye = value;
				UpdateViewMatrix();
			}
			get { return eye;}
		}

		public Matrix4 View {
			get {return view;}
			set { view = value;}
		}

		public virtual void UpdateViewMatrix()
		{
			this.view = Matrix4.LookAt(eye, origin, up);
		}

		public virtual void UpdateProjMatrix()
		{
			this.projection = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
		}

		public Matrix4 Projection
		{
			get { return projection;}
			set { projection = value;}
		}

		public Vector3 Forward
		{
			get {
				return Transform.Column2.Xyz;
			}
		}

		public Vector3 Up
		{
			get {
				return Transform.Column1.Xyz;
			}
		}

		public Vector3 Right
		{
			get {
				return Transform.Column0.Xyz;
			}
		}

		public override void Update ()
		{
			//UpdateViewMatrix();
			base.Update ();
		}

		public Camera ()
		{

		}
	}
}

