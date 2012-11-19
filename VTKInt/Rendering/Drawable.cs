using System;
using System.Collections.Generic;
using OpenTK;
using VTKInt.Structues;

namespace VTKInt.Rendering
{
	public class Drawable
	{
		protected List<Mesh> meshes = new List<Mesh>();
		protected Shader shader;

		public Drawable ()
		{
		}

		public virtual void SetVBOs(Mesh mesh, Shader shader)
		{

		}

		public virtual void Render()
		{

		}
	}
}

