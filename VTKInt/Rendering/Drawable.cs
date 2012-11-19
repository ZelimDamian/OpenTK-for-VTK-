using System;
using System.Collections.Generic;
using OpenTK;
using VTKInt.Structues;

namespace VTKInt.Rendering
{
	public class Drawable
	{
		List<Mesh> meshes = new List<Mesh>();

		public Drawable ()
		{
		}

		public virtual void Render()
		{

		}
	}
}

