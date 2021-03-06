using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.ES20;

namespace VTKInt.Textures
{
	public class Texture
	{
		public string Name;
		public Materials.Material.TexType Type;
		public int id;
	}

	public class TextureLoader
	{
		public static List<Texture> textures = new List<Texture>();

		public const string ContentDir = "../../Content/Textures/";

		public TextureLoader ()
		{
		}

		public static Texture GetTexture(string name)
		{
			foreach(Texture texture in textures)
				if(texture.Name == name)
					return texture;

			return LoadTexture(name);
		}

		public static Texture LoadTexture(string name)
	{
			Texture texture = new Texture();
			texture.Name = name;

			Bitmap bmp = new Bitmap(ContentDir + name);

			texture.id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, texture.id);

			 bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
			              OpenTK.Graphics.ES20.PixelFormat.Rgba, PixelType.UnsignedByte, bmp_data.Scan0);
			
			bmp.UnlockBits(bmp_data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

			textures.Add(texture);

			return texture;
		}
	}
}

