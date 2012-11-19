using System;
using System.Xml;
using System.Collections.Generic;
using OpenTK;
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
		Texture[] textures;
		public string Name;

		public enum TexType
		{
			baseTexture,
			base2Texture,
			base3Texture,
			normalTexture,
			emitTexture,
			reflectionTexture,
			emitMapTexture,
			specMapTexture,
			envMapTexture,
			envTexture,
			definfoTexture
		}

		public void SetTexture(TexType type, Texture texture)
		{
			textures[(int)type] = texture;
		}
	}
	
	public class MaterialLoader
	{
		public static List<Material> materials = new List<Material>();

		public static string ContentDir = "Content/Materials/";

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

					materials.Add(material);

					return material;
				}
			}
	}
}

