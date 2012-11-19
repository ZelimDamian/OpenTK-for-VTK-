using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace VTKInt.Structues
{
	public class Mesh
	{
		public uint PositionVboHandle,
					NormalVboHandle,
					TextureVboHandle,
					TangentVboHandle;

		public Vector3[] PositionData,
						 NormalData,
						 TangentData;

		public Vector2[] TexCoordData;

		public Mesh ()
		{

		}
	}
}