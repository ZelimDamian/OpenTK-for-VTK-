using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VTKInt.Structues;
using VTKInt.Rendering;

namespace VTKInt.Models
{
	public class Model : Drawable
	{
		public Model ()
		{
		}

		public override void Render()
		{
			SetUpMaterial(material);

			Mesh tmpMesh = null;

			foreach(Mesh mesh in meshes)
			{
				if(tmpMesh != mesh)
					SetVBOs(mesh, material.shader);

				GL.DrawElements(BeginMode.Triangles, mesh.ElementsData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

				tmpMesh = mesh;
			}
		}
	}
}

