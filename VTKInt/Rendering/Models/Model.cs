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
			SetMaterial(material);

			foreach(Mesh mesh in meshes)
			{
				SetVBOs(mesh, material.shader);
				GL.DrawElements(BeginMode.Triangles, mesh.ElementsData.Length, DrawElementsType.UnsignedInt, mesh.ElementsData);
			}
		}
	}
}

