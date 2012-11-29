using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VTKInt.Structues;
using VTKInt.Materials;

namespace VTKInt.Rendering
{
	public class Drawable : VTKObject
	{
		protected List<Component> components = new List<Component>();
		protected Material material;

		protected bool CastShadows = false;


		public Drawable ()
		{
		}

		public override void Render(RenderPass pass)
		{
			
		}

		public virtual void SetVBOs(Mesh curMesh, Shader shader)
		{
				int shaderHandle = shader.handle;
				
				int normalIndex = GL.GetAttribLocation(shaderHandle, "in_normal");
				int positionIndex = GL.GetAttribLocation(shaderHandle, "in_position");
				int tangentIndex = GL.GetAttribLocation(shaderHandle, "in_tangent");
				int textureIndex = GL.GetAttribLocation(shaderHandle, "in_texture");
		

				if (normalIndex != -1)
				{
					GL.EnableVertexAttribArray(normalIndex);
					GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.NormalVboHandle);
					GL.VertexAttribPointer(normalIndex, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
				}
				
				if (positionIndex != -1)
				{
					GL.EnableVertexAttribArray(positionIndex);
					GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.PositionVboHandle);
					GL.VertexAttribPointer(positionIndex, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
				}
				
				if (tangentIndex != -1)
				{
					GL.EnableVertexAttribArray(tangentIndex);
					GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.TangentVboHandle);
					GL.VertexAttribPointer(tangentIndex, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
				}
				
				if (textureIndex != -1)
				{
					GL.EnableVertexAttribArray(textureIndex);
					GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.TextureVboHandle);
					GL.VertexAttribPointer(textureIndex, 2, VertexAttribPointerType.Float, true, Vector2.SizeInBytes, 0);
				}
				
				
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, curMesh.ElementsVBOHandle);
		}

		public virtual void AddComponent(Mesh mesh)
		{
			this.components.Add(new Component(mesh));
		}

		public virtual void AddComponent(Component component)
		{
			this.components.Add(component);
		}

		public virtual void AddMaterial(Material material)
		{
			this.material = material;
		}

		public virtual void SetUpMaterial()
		{
			GL.UseProgram(material.shader.handle);
			
			material.activateUniforms();
			material.activateTextures();
		}

		public virtual void SetUpShadowMaterial()
		{
			Shader shader = SceneManager.ShadowPassShader;
			GL.UseProgram(shader.handle);
//
//			Vector3 eye = SceneManager.Camera.Eye;
//			Matrix4 view = SceneManager.Camera.View;
//			Matrix4 proj = SceneManager.Camera.Projection;
//			Vector3 lightPos = SceneManager.Light.Eye;
//
//			shader.insertUniform(Shader.Uniform.in_eyepos, ref eye);
//			shader.insertUniform(Shader.Uniform.projection_matrix, ref proj);
//			shader.insertUniform(Shader.Uniform.modelview_matrix, ref view);
//			shader.insertUniform(Shader.Uniform.in_light, ref lightPos);

			Matrix4 lightView = SceneManager.Light.View;
			Matrix4 lightProj = SceneManager.Light.Projection;

			shader.insertUniform(Shader.Uniform.light_view, ref lightView);
			shader.insertUniform(Shader.Uniform.light_proj, ref lightProj);

			float lightFar = SceneManager.Light.Far;
			shader.insertUniform(Shader.Uniform.in_far, ref lightFar);

		}
	}
}

