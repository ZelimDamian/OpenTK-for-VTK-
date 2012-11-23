using System;
using OpenTK;

namespace VTKInt.Structues
{
	public class BoundingBox
	{
		Vector3 min = Vector3.Zero, max = Vector3.Zero;

		public Vector3 Min
		{
			set {min = value;}
			get {return min;}
		}

		public Vector3 Max
		{
			set {max = value;}
			get {return max;}
		}

		public Vector3 Center
		{
			set {
				Vector3 dist = value - Center;
				min += dist;
				max += dist;
			}
			get { return (min + max) / 2.0f; }
		}

		public Vector3 HalfSize
		{
			set {
				Vector3 cent = Center;
				max = cent + value;
				min = cent - value;
			}
			get { return (max - min) / 2.0f; }
		}

		public BoundingBox ()
		{
		}

		public BoundingBox(BoundingBox box)
		{
			this.min = box.Min;
			this.max = box.Max;
		}

		public BoundingBox (Vector3 min, Vector3 max)
		{
			this.min = min;
			this.max = max;
		}

		public BoundingBox (Mesh mesh)
		{
			min = new Vector3(9999.0f, 9999.0f, 9999.0f);
			max = -min;

			foreach(Vector3 vec in mesh.PositionData )
			{
				if(max.X < vec.X)
					max.X = vec.X;
				if(max.Y < vec.Y)
					max.Y = vec.Y;
				if(max.Z < vec.Z)
					max.Z = vec.Z;

				if(min.X > vec.X)
					min.X = vec.X;
				if(min.Y > vec.Y)
					min.Y = vec.Y;
				if(min.Z > vec.Z)
					min.Z = vec.Z;
			}
		}
	}
}

