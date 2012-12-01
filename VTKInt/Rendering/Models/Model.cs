using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VTKInt;
using VTKInt.Structues;
using VTKInt.Rendering;
using VTKInt.Animations;

namespace VTKInt.Models
{
	public class Model : Drawable, IAnimatable
	{
		public Model ()
		{
		}

		public override void Render(RenderPass pass)
		{
			if(pass == RenderPass.Shadow )
			{
				if(CastShadows)
				{
					SetUpShadowCastMaterial();
					Render(SceneManager.ShadowPassShader);
				}
			}
			else if(pass == RenderPass.Render)
			{
				SetUpMaterial();
				Render();
			}
		}

		public override void Render()
		{
			Render(material.shader);
		}

		public override void Render(Shader shader)
		{
			Mesh tmpMesh = null;

			foreach(Component comp in components)
			{
				if(tmpMesh != comp.Mesh)
					SetVBOs(comp.Mesh, shader);

				Matrix4 meshTransform = comp.Transform;
				Matrix4 rotationMatirx = Matrix4.Rotate(this.Orientation) * Matrix4.Rotate(comp.Orientation);

				shader.insertUniform(Shader.Uniform.mesh_matrix, ref meshTransform);
				shader.insertUniform(Shader.Uniform.rotation_matrix, ref rotationMatirx);
				shader.insertUniform(Shader.Uniform.model_matrix, ref transform);

				GL.DrawElements(BeginMode.Triangles, comp.Mesh.ElementsData.Length,
				                DrawElementsType.UnsignedInt, IntPtr.Zero);

				tmpMesh = comp.Mesh;
			}
		}

		public override void Update ()
		{
			base.Update ();
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

		public bool IsAnimatedWith(AnimationType type)
		{
			foreach(Animation anim in Animations)
			{
				if(anim.Type == type)
					return true;
			}
			return false;
		}

		public List<Animation> Animations
		{
			get { return animations; }
			set { animations = value; }
		}
	}
}

