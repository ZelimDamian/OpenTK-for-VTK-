using System;
using System.Xml;
using System.Collections.Generic;
using OpenTK;

namespace VTKInt.Materials
{
	public class Material
	{

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

			while(reader.Read())
			{
				if(reader.Name == "material")
				{

				}
			}

		}
	}
}

