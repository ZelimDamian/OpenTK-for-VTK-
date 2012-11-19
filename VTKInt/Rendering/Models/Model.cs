using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VTKInt.Structues;

namespace VTKInt.Models
{
	public class Model : Drawable
	{
		public Model ()
		{
		}

		public override void Render()
		{
			foreach(Mesh mesh in meshes)
			{
				SetVBOs(mesh);
				//GL.DrawElements(BeginMode.Triangles, mesh
			}
		}
	}
}

