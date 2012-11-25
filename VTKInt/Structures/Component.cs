using System;
using OpenTK;

namespace VTKInt.Structues
{
	public class Component : VTKObject
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

		public BoundingBox Box;
		public Mesh Mesh;

		public String NameClean
		{
			get { return Mesh.Name.Replace(".obj", ""); }
		}
	}
}