using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
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
				
				GL.BindVertexArray(0);
		}

		public virtual void Render()
		{

		}
	}
}

