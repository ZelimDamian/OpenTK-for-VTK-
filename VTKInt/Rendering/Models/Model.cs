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

			foreach(Component comp in components)
			{
				if(tmpMesh != comp.Mesh)
					SetVBOs(comp.Mesh, material.shader);

				Matrix4 meshTransform = comp.Transform;
				Matrix4 rotationMatirx = Matrix4.Rotate(this.Orientation) * Matrix4.Rotate(comp.Orientation);

				material.shader.insertUniform(Shader.Uniform.mesh_matrix, ref meshTransform);
				material.shader.insertUniform(Shader.Uniform.rotation_matrix, ref rotationMatirx);

				GL.DrawElements(BeginMode.Triangles, comp.Mesh.ElementsData.Length,
				                DrawElementsType.UnsignedInt, IntPtr.Zero);

				tmpMesh = comp.Mesh;
			}
		}

		public override void Update ()
		{
			base.Update ();
		}
	}
}

