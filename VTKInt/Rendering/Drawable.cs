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

		public Drawable ()
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

		public virtual void SetUpMaterial(Materials.Material material)
		{
			GL.UseProgram(material.shader.handle);

			material.activateUniforms();

			Vector3 lightPos = new Vector3(10.0f, 100.0f, 10.0f);

			material.shader.insertUniform(Shader.Uniform.model_matrix, ref transform);
			material.shader.insertUniform(Shader.Uniform.in_light, ref lightPos);
			
			material.activateTextures();
		}
	}
}

