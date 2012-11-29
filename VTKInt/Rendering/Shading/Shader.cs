using System;
using System.Xml;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace VTKInt
{
	public class Shader
{
		//stuff to be saved
		public string name;
		public string vShader;
		public string fShader;
		
		//generic
		public string[] URL;
		public Type type;
		public int handle;
		public int identifier;
		public bool loaded;
		
		public enum Uniform
		{
			projection_matrix,
			projection_rev_matrix,
			modelview_matrix,
			rotation_matrix,
			model_matrix,
			rotation_matrix2,
			model_matrix2,
			mesh_matrix,
			light_view,
			light_proj,
			light_matrix,
			shadow_bias,

			in_eyepos,
			in_time,
			in_pass,
			in_waterlevel,
			in_vector,
			in_screensize,
			in_rendersize,
			in_lightambient,
			in_lightsun,
			in_light,
			//shadow_quality,
			
			in_particlepos,
			in_particlesize,
			
			in_color,
			in_mod,
			
			use_emit,
			emit_a_base,
			emit_a_normal,
			in_emitcolor,
			
			use_spec,
			spec_a_base,
			spec_a_normal,
			in_speccolor,
			in_specexp,
			
			use_env,
			env_a_base,
			env_a_normal,
			env_tint,
			
			use_alpha,
			ref_size,
			blur_size,
			fresnel_str,
			
			in_near,
			in_far,
			
			in_hudsize,
			in_hudpos,
			in_hudcolor,
			in_hudvalue,
			
			in_no_lights,
			curLight,
			uni_no_bones,
			invMVPMatrix,
			defDirection,
			defColor,
			defMatrix,
			defInnerMatrix,
			invPMatrix,
			defPosition,
			defInvPMatrix,
			invMMatrix,
			useTexture,
			viewUp,
			viewRight,
			viewDirection,
			viewPosition,
			fresnelExp,
			fresnelStr,
			shadowQuality
		}
		
		int[] locations;
		
		public void insertUniform(Uniform uni, ref float value)
		{
			int location = locations[(int)uni];
			if (location != -1)
				GL.Uniform1(location, 1, ref value);
		}
		
		internal void insertUniform(Uniform uni, ref int value)
		{
			int location = locations[(int)uni];
			if (location != -1)
				GL.Uniform1(location, 1, ref value);
		}
		
		internal void insertUniform(Uniform uni, ref Vector4 value)
		{
			int location = locations[(int)uni];
			if (location != -1)
				GL.Uniform4(location, ref value);
		}
		
		public void insertUniform(Uniform uni, ref Vector3 value)
		{
			int location = locations[(int)uni];
			if (location != -1)
				GL.Uniform3(location, ref value);
		}
		
		internal void insertUniform(Uniform uni, ref Vector2 value)
		{
			int location = locations[(int)uni];
			if (location != -1)
				GL.Uniform2(location, ref value);
		}
		
		public void insertUniform(Uniform uni, ref Matrix4 value)
		{
			int location = locations[(int)uni];
			if (location != -1)
				GL.UniformMatrix4(location, false, ref value);
		}
		
		public void generateLocations()
		{
			string[] names = Enum.GetNames(typeof(Uniform));
			
			int handlesCount = names.Length;
			locations = new int[handlesCount];
			
			for (int i = 0; i < handlesCount; i++)
			{
				locations[i] = GL.GetUniformLocation(handle, names[i]);
			}
		}
	}

	public class ShaderLoader
	{
		public static List<Shader> shaders = new List<Shader>();
		public static string ContentDirectory = "../../Content/Shaders/";

		public static Shader GetShader(string name)
		{
			foreach(Shader shader in shaders)
				if(shader.name == name)
					return shader;

			return new ShaderLoader().LoadShader(name);
		}

		public Shader LoadShader(string name)
		{
			Shader shader = new Shader();
			shader.name = name;
			loadShaderXml(ref shader);
			return shader;
		}

		public void loadShaderXml(ref Shader target)
		{
			XmlTextReader reader = new XmlTextReader(ContentDirectory + target.name);

			target.URL = new string[2];
			
			while (reader.Read())
			{
				// parsing data in material tag
				if (reader.Name == "shaderpair" && reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						if (reader.Name == "vertex")
							target.URL[0] = ContentDirectory + reader.Value;
						
						else if (reader.Name == "fragment")
							target.URL[1] = ContentDirectory + reader.Value;
					}
					reader.MoveToElement();
				}
			}
			
			loadShaderFromFile(ref target);
		}
		
		public void loadShaderFromFile(ref Shader target)
		{
			string vfile = target.URL[0];
			string ffile = target.URL[1];
			
			target.vShader = readFile(vfile);
			target.fShader = readFile(ffile);
			
			loadShaderFromCache(ref target);
		}
		
		public void loadShaderFromCache(ref Shader target)
		{
			int shaderProgramHandle;
			
			int vertexShaderHandle,
			fragmentShaderHandle;

			vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
			fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
			
			GL.ShaderSource(vertexShaderHandle, target.vShader);
			GL.ShaderSource(fragmentShaderHandle, target.fShader);

			GL.CompileShader(vertexShaderHandle);
			GL.CompileShader(fragmentShaderHandle);

			Console.WriteLine(GL.GetShaderInfoLog(vertexShaderHandle));
			Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderHandle));
			
			// Create program
			shaderProgramHandle = GL.CreateProgram();
			
			GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
			GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);
			
			GL.LinkProgram(shaderProgramHandle);
			
			Console.WriteLine(GL.GetProgramInfoLog(shaderProgramHandle));

			target.handle = shaderProgramHandle;
			
			getHandles(ref target);
			
			target.loaded = true;
			
			shaders.Add(target);
		}

		public void getHandles(ref Shader target)
		{
			//int shaderProgramHandle = target.handle;
			
			// Set uniforms
			target.generateLocations();
		}
		
		private string readFile(string filename)
		{
			string line;
			string wholeFile = "";
			
			System.IO.StreamReader file =
				new System.IO.StreamReader(filename);
			while ((line = file.ReadLine()) != null)
			{
					wholeFile += line + Environment.NewLine;
			}

			return wholeFile;
		}
	}
}

