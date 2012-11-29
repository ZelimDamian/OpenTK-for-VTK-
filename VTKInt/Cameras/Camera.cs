using System;
using OpenTK;

namespace VTKInt.Cameras
{
	public class Camera : VTKObject
	{
		protected int mouseXOld, mouseYOld;
		protected float xSens = 0.1f,
						ySens = 0.1f;


		protected Matrix4 view,
				projection;

		protected Vector3 origin,
				eye,
				up = Vector3.UnitY;

		protected float 	fov = (float)MathHelper.DegreesToRadians(60.0f),
						aspect = 1.0f,
						near = 0.1f,
						far = 450.0f;

		public float Fov
		{
			get { return fov; }
			set {
				if(fov == value)
					return;
				fov = value;
				UpdateProjMatrix();
			}
		}

		public float Aspect
		{
			get { return aspect; }
			set {
				if(aspect == value)
					return;
				aspect = value;
				UpdateProjMatrix();
			}
		}

		public float Near
		{
			get { return near; }
			set {
				if(near == value)
					return;
				near = value;
				UpdateProjMatrix();
			}
		}

		public float Far
		{
			get { return far; }
			set {
				if(far == value)
					return;
				far = value;
				UpdateProjMatrix();
			}
		}

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

		public Matrix4 Matrix
		{
			get { return View * Projection; }
		}

		public virtual void UpdateViewMatrix()
		{

		}

		public virtual void UpdateProjMatrix()
		{
		}

		public Matrix4 Projection
		{
			get { return projection;}
			set { projection = value;}
		}

		public Vector3 Forward
		{
			get {
				return -View.Column2.Xyz;
			}
		}

		public Vector3 Up
		{
			get {
				return -View.Column1.Xyz;
			}
		}

		public Vector3 Right
		{
			get {
				return View.Column0.Xyz;
			}
		}

		public override void Update ()
		{
			base.Update ();
		}

		public Camera ()
		{
			mouseXOld = SceneManager.Window.Mouse.X;
			mouseYOld = SceneManager.Window.Mouse.Y;

			UpdateProjMatrix();
			UpdateViewMatrix();
		}
	}
}

