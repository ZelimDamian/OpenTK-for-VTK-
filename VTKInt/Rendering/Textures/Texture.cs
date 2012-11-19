using System;
using System.Collections.Generic;
using OpenTK;

namespace VTKInt.Textures
{
	public class Texture
	{

	}

	public class TextureLoader
	{
		List<Texture> textures = new List<Texture>();

		public TextureLoader ()
		{
		}

		public Texture GetTexture(string name)
		{
			foreach(Texture texture in textures)
				if(texture.Name == name)
					return texture;

			return LoadTexture(name);
		}

		public Texture LoadTexture(string name)
		{

		}
	}
}

