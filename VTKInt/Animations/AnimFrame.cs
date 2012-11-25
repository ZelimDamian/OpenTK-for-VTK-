using System;
using OpenTK;

namespace VTKInt.Animations
{
	public class AnimFrame
	{
		Vector3? position = null;
		Vector3? scale = null;
		Quaternion? orientation = null;
		float Time;

		public AnimFrame (Vector3 position, Vector3 scale, Quaternion orientation)
		{
			this.position = position;
			this.scale = scale;
			this.orientation = orientation;
		}

		public AnimFrame(Vector3 position)
		{
			this.position = position;
		}

		public AnimFrame(Vector3 position, float scale) : this(position, new Vector3(scale, scale, scale)) {}

		public AnimFrame(Quaternion orientation)
		{
			this.orientation = orientation;
		}

		public AnimFrame(Vector3 position, Vector3 scale)
		{
			this.position = position;
			this.scale = scale;
		}

		public bool IsAnimatedPos
		{
			get { return this.position != null; }
		}

		public Vector3 Position
		{
			get { return (Vector3) position; }
			set { position = value; }
		}

		public bool IsAnimatedScale
		{
			get {return scale != null; }
		}

		public Vector3 Scale
		{
			get { return (Vector3) scale; }
			set { this.scale = value; } 
		}

		public bool IsAnimatedOrientation
		{
			get { return this.orientation != null; }
		}

		public Quaternion Orientation
		{
			get { return (Quaternion) orientation; }
			set { this.orientation = value; }
		}
	}
}

