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

		protected bool ReceiveShadows = false;

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

			if(ReceiveShadows)
				SetUpShadowReceiveMaterial();
		}

		public virtual void SetUpShadowReceiveMaterial()
		{
			
			Matrix4 lightView = SceneManager.Light.View;
			Matrix4 lightProj = SceneManager.Light.Projection;
			
			material.shader.insertUniform(Shader.Uniform.light_view, ref lightView);
			material.shader.insertUniform(Shader.Uniform.light_proj, ref lightProj);
			
			// Moving from unit cube [-1,1] to [0,1]  
			Matrix4 bias = new Matrix4(	
			                           0.5f, 0.0f, 0.0f, 0.0f,
			                           0.0f, 0.5f, 0.0f, 0.0f,
			                           0.0f, 0.0f, 0.5f, 0.0f,
			                           0.5f, 0.5f, 0.5f, 1.0f
			                           );
			
			bias = lightView * lightProj * bias;
			
			material.shader.insertUniform(Shader.Uniform.shadow_bias, ref bias);
			
			float lightFar = SceneManager.Light.Far;
			material.shader.insertUniform(Shader.Uniform.in_far, ref lightFar);

		}

		public virtual void SetUpShadowCastMaterial()
		{
			Shader shader = SceneManager.ShadowPassShader;
			GL.UseProgram(shader.handle);

			Matrix4 lightView = SceneManager.Light.View;
			Matrix4 lightProj = SceneManager.Light.Projection;

			shader.insertUniform(Shader.Uniform.light_view, ref lightView);
			shader.insertUniform(Shader.Uniform.light_proj, ref lightProj);

			float lightFar = SceneManager.Light.Far;
			shader.insertUniform(Shader.Uniform.in_far, ref lightFar);

		}
	}
}

