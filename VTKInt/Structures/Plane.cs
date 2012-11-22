using System;
using OpenTK;

namespace VTKInt.Structues
{
	public class Plane
	{
		Vector3 normal;
		Vector3 point;

		public Vector3 Normal
		{
			set { normal = value;}
			get {return normal;}
		}

		public Vector3 Point
		{
			set { point = value;}
			get {return point;}
		}

		public Plane (Vector3 normal, Vector3 point)
		{
			this.normal = normal;
			this.point = point;
		}

		public Plane(Vector3 normal, float d) : this(normal, normal * d)
		{}
	}
}

