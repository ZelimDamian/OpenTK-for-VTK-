using System;
using System.Xml;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VTKInt.Textures;

namespace VTKInt.Materials
{
	public class Material
	{
		public Material()
		{
			int count = Enum.GetValues(typeof(TexType)).Length;
			textures = new Texture[count];
		}

		public Shader shader;
		public Texture[] textures;
		public string Name;
		Vector4 color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

		public Vector4 Color
		{
			get { return color; }
			set { color = value; }
		}

		public enum TexType
		{
			baseTexture,
			base2Texture,
			envMapTexture,
			base3Texture,
			normalTexture,
			emitTexture,
			reflectionTexture,
			emitMapTexture,
			specMapTexture,
			envTexture,
			definfoTexture,
			lightTexture
		}

		public void activateUniforms()
		{
			Vector3 eye = SceneManager.Camera.Eye;
			Matrix4 view = SceneManager.Camera.View;
			Matrix4 proj = SceneManager.Camera.Projection;
			Vector3 lightPos = SceneManager.Light.Eye;

			shader.insertUniform(Shader.Uniform.in_eyepos, ref eye);
			shader.insertUniform(Shader.Uniform.projection_matrix, ref proj);
			shader.insertUniform(Shader.Uniform.modelview_matrix, ref view);
			shader.insertUniform(Shader.Uniform.in_light, ref lightPos);
		}

		public void activateTextures()
		{
			int texUnit = 0;
			for(int i = 0; i < textures.Length; i++)
			{
				if(textures[i] == null)
					continue;

				GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
				GL.BindTexture(TextureTarget.Texture2D, textures[i].id);

				string texName = textures[i].Type.ToString();
				GL.Uniform1(GL.GetUniformLocation(shader.handle, texName), texUnit);

				texUnit ++;
			}
		}

		public void SetTexture(TexType type, Texture texture)
		{
			texture.Type = type;
			int typeint = (int)type;
			textures[typeint] = texture;
		}
	}
	
	public class MaterialLoader
	{
		public static List<Material> materials = new List<Material>();

		public static string ContentDir = "../../Content/Materials/";

		public MaterialLoader ()
		{
		}

		public static Material GetMaterial(string name)
		{
			foreach(Material material in materials)
				if(material.Name == name)
					return material;

			return LoadMaterial(name);
		}

		public static Material LoadMaterial(string name)
		{
			XmlReader reader = XmlReader.Create(ContentDir + name);

			Material material = new Material();
			material.Name = name;

			while (reader.Read())
				{
					// parsing data in material tag
					if (reader.Name == "material" && reader.HasAttributes)
					{
						while (reader.MoveToNextAttribute())
						{
							if (reader.Name == "shader")
								material.shader = ShaderLoader.GetShader(reader.Value);

						}
						reader.MoveToElement();
					}
					
					// parsing textures
					if (reader.Name == "textures" && reader.HasAttributes)
					{
						while (reader.MoveToNextAttribute())
						{
							Texture tmpTex = TextureLoader.GetTexture(reader.Value);
							
							if (reader.Name == "base")
								material.SetTexture(Material.TexType.baseTexture, tmpTex);

							if (reader.Name == "base2")
								material.SetTexture(Material.TexType.base2Texture, tmpTex);
						
							if (reader.Name == "envmap")
								material.SetTexture(Material.TexType.envMapTexture, tmpTex);

							if (reader.Name == "lightMap")
								material.SetTexture(Material.TexType.lightTexture, tmpTex);
						}
					reader.MoveToElement();
					}
					
					// parsing envmap data
					if (reader.Name == "envmap")
					{
						
						if (reader.HasAttributes)
						{
							while (reader.MoveToNextAttribute())
							{
								if (reader.Name == "source")
								{
									material.SetTexture(Material.TexType.envMapTexture, TextureLoader.GetTexture(reader.Value));	
								}
							}
							reader.MoveToElement();
						}
					}

				}
			materials.Add(material);
			
			return material;
		}
	}
}

