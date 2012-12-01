using System;
using OpenTK;
using System.Collections.Generic;
using VTKInt.Animations;

namespace VTKInt.Structues
{
	public class Component : VTKObject, IAnimatable
	{
		public Component (string meshName)
		{
			Mesh = MeshLoader.GetMesh(meshName);
			Box = new BoundingBox(Mesh);

		}

		public Component (string meshName, BoundingBox box)
		{
			Mesh = MeshLoader.GetMesh(meshName);
			Box = box;
		}

		public Component ( Mesh mesh )
		{
			Mesh = mesh;
			Box = new BoundingBox(mesh);
		}

		public Component (Mesh mesh, BoundingBox box)
		{
			Mesh = mesh;
			Box = box;
		}

		public override Vector3 Position {
			get {
				return base.Position;
			}
			set {
				Box.Center = value;
				base.Position = value;
			}
		}

		public override Quaternion Orientation {
			get {
				return base.Orientation;
			}
			set {
				base.Orientation = value;
			}
		}

		public override Vector3 Scale {
			get {
				return base.Scale;
			}
			set {
				Box.Scale(value);
				base.Scale = value;
			}
		}

		List<Animation> animations = new List<Animation>();
		
		public bool IsAnimated
		{
			get { return animations.Count != 0; }
		}
		
		public VTKObject Animated
		{
			get { return this; }
			set {}
		}
		
		public List<Animation> Animations
		{
			get { return animations; }
			set { animations = value; }
		}

		public BoundingBox Box;
		public Mesh Mesh;

		public String NameClean
		{
			get { return Mesh.Name.Replace(".obj", ""); }
		}

		public bool IsAnimatedWith(AnimationType type)
		{
			foreach(Animation anim in Animations)
			{
				if(anim.Type == type)
					return true;
			}
			return false;
		}
	}
}