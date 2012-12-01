using System;
using System.Xml;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VTKInt.Cameras;
using VTKInt.Lights;
using VTKInt.Models;
using VTKInt.Interface;
using VTKInt.Structues;
using VTKInt.Materials;
using VTKInt.Animations;
using VTKInt.Framebuffers;

namespace VTKInt
{
	public class Scene
	{
		public Scene ()
		{
		}

		List<VTKObject> objects = new List<VTKObject>();
		Camera camera;

		public Camera Camera
		{
			get {return camera;}
		}

		const string SceneFile = "../../Content/Scene.xml";


		public void Load(string filename = SceneFile)
		{
			InitilizeGL();

			XmlReader sceneReader = XmlTextReader.Create(filename);

			while(sceneReader.Read ())
			{
				if(sceneReader.Name == "camera")
				{
					Camera camera = new CameraFPS();

					if(sceneReader.HasAttributes)
						while(sceneReader.MoveToNextAttribute())
						{
							if(sceneReader.Name == "eye")
							{
								camera.Eye = ParseVector(sceneReader.Value);
							}
							else if(sceneReader.Name == "origin")
							{
								camera.Origin = ParseVector(sceneReader.Value);
							}
						}

					sceneReader.MoveToElement();

					this.camera = camera;
					objects.Add(camera);
				}

				if(sceneReader.Name == "model" && sceneReader.HasAttributes)
				{
					Model model = new Model();

					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "mesh")
						{
							model.AddComponent(MeshLoader.GetMesh(sceneReader.Value));
						}
						else if(sceneReader.Name == "material")
						{
							model.AddMaterial(MaterialLoader.GetMaterial(sceneReader.Value));
						}
						else if(sceneReader.Name == "position")
						{
							model.Position = ParseVector(sceneReader.Value);
						}
						else if(sceneReader.Name == "orientation")
						{
							model.Orientation = ParseQuaternion(sceneReader.Value);
						}
					}

					sceneReader.MoveToElement();
					this.objects.Add(model);
				}

				if(sceneReader.Name == "emblem" && sceneReader.HasAttributes)
				{
					Emblem model = new Emblem();
					
					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "mesh")
						{
							model.AddComponent(MeshLoader.GetMesh(sceneReader.Value));
						}
						else if(sceneReader.Name == "material")
						{
							model.AddMaterial(MaterialLoader.GetMaterial(sceneReader.Value));
						}
						else if(sceneReader.Name == "position")
						{
							model.Position = ParseVector(sceneReader.Value);
						}
						else if(sceneReader.Name == "orientation")
						{
							model.Orientation = ParseQuaternion(sceneReader.Value);
						}
					}
					
					sceneReader.MoveToElement();
					this.objects.Add(model);
				}

				if(sceneReader.Name == "sky" && sceneReader.HasAttributes)
				{
					SkySphere model = new SkySphere();
					
					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "mesh")
						{
							model.AddComponent(MeshLoader.GetMesh(sceneReader.Value));
						}
						else if(sceneReader.Name == "material")
						{
							model.AddMaterial(MaterialLoader.GetMaterial(sceneReader.Value));
						}
						else if(sceneReader.Name == "position")
						{
							model.Position = ParseVector(sceneReader.Value);
						}
						else if(sceneReader.Name == "orientation")
						{
							model.Orientation = ParseQuaternion(sceneReader.Value);
						}
					}
					
					sceneReader.MoveToElement();
					this.objects.Add(model);
				}

				if(sceneReader.Name == "field" && sceneReader.HasAttributes)
				{
					String meshName = "", materialName = "";
					int x = 0, y = 0;
					Vector3 position;

					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "mesh")
						{
							meshName = sceneReader.Value;
						}
						else if(sceneReader.Name == "material")
						{
							materialName = sceneReader.Value;
						}
						else if(sceneReader.Name == "x")
						{
							x = int.Parse(sceneReader.Value);
						}
						else if(sceneReader.Name == "z")
						{
							y = int.Parse(sceneReader.Value);
						}
						else if(sceneReader.Name == "position")
						{
							position = ParseVector(sceneReader.Value);
						}
					}
					
					Field model = new Field(x, y, meshName, materialName);
					model.Position = position;

					sceneReader.MoveToElement();
					this.objects.Add(model);
				}

				if(sceneReader.Name == "numPad" && sceneReader.HasAttributes)
				{
					String materialName = "";
					float width = 0.0f, height = 0.0f;
					Vector3 position;
					Quaternion orientation;

					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "material")
						{
							materialName = sceneReader.Value;
						}
						else if(sceneReader.Name == "width")
						{
							width = float.Parse(sceneReader.Value);
						}
						else if(sceneReader.Name == "height")
						{
							height = float.Parse(sceneReader.Value);
						}
						else if(sceneReader.Name == "position")
						{
							position = ParseVector(sceneReader.Value);
						}
						else if(sceneReader.Name == "orientation")
						{
							orientation = ParseQuaternion(sceneReader.Value);
						}
					}

					Numpad numpad = new Numpad(materialName, width, height);
						numpad.Position = position;
						numpad.Orientation = orientation;

					this.objects.Add(numpad);

					sceneReader.MoveToElement();
				}

				if(sceneReader.Name == "light")
				{
					Light light = new Light();

					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "position")
						{
							light.Position = ParseVector(sceneReader.Value);
						}
						else if(sceneReader.Name == "origin")
						{
							light.Origin = ParseVector(sceneReader.Value);
						}
					}

					SceneManager.Light = light;

					this.objects.Add(light);
					sceneReader.MoveToElement();
				}

				if(sceneReader.Name == "emblems" && sceneReader.HasAttributes)
				{
					EmblemPanel panel = new EmblemPanel();

					while(sceneReader.MoveToNextAttribute())
					{
						if(sceneReader.Name == "distanceBetween")
						{
							panel.DistanceBetween = float.Parse(sceneReader.Value);
						} else if(sceneReader.Name == "position")
						{
							panel.Position = ParseVector(sceneReader.Value);
						}
					}

 					sceneReader.MoveToElement();
					sceneReader.ReadToDescendant("emblem");

					do
					{
						Emblem model = new Emblem();
						
						while(sceneReader.MoveToNextAttribute())
						{
							if(sceneReader.Name == "mesh")
							{
								model.AddComponent(MeshLoader.GetMesh(sceneReader.Value));
							}
							else if(sceneReader.Name == "material")
							{
								model.AddMaterial(MaterialLoader.GetMaterial(sceneReader.Value));
							}
							else if(sceneReader.Name == "position")
							{
								model.Position = ParseVector(sceneReader.Value);
							}
							else if(sceneReader.Name == "orientation")
							{
								model.Orientation = ParseQuaternion(sceneReader.Value);
							}
						}
						
						//sceneReader.MoveToElement();

						panel.AddEmblem(model);

					} while(sceneReader.ReadToNextSibling("emblem"));

					this.objects.Add(panel);
					
					sceneReader.MoveToElement();
				}
			}
		}

		private Vector3 ParseVector(string str)
		{
			string [] coords = str.Split(' ');
			return new Vector3(float.Parse(coords[0]),
                             float.Parse(coords[1]),
                             float.Parse(coords[2]));
		}

		private Quaternion ParseQuaternion(string str)
		{
			string [] angles = str.Split(' ');
			return Quaternion.FromAxisAngle(ParseVector(str),
			                                MathHelper.DegreesToRadians(float.Parse(angles[3])));
		}

		public void Render(RenderPass pass)
		{
			switch(pass)
			{
			case RenderPass.Render : SceneManager.DefaultFramebuffer.enable(true); break;
			case RenderPass.Shadow : SceneManager.LightFramebuffer.enable(true); break;
			}

			foreach(VTKObject obj in objects)
			{
				obj.Render(pass);
			}
		}

		public void Update()
		{
			foreach(VTKObject obj in objects)
			{
				obj.Update();
			}

			AnimationManager.Update();

			if(SceneManager.Window.Keyboard[OpenTK.Input.Key.Escape])
				SceneManager.Window.Exit();
		}

		public void InitilizeGL()
		{
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

//			GL.Enable(EnableCap.CullFace);
//			GL.CullFace(CullFaceMode.Back);
		}
	}
	
		public class SceneTypeArgs : EventArgs
		{
			private string message;
			
			public SceneTypeArgs(string message)
			{
				this.message = message;
			}
			
			// This is a straightforward implementation for 
			// declaring a public field
			public string Message
			{
				get
				{
					return message;
				}
			}
	}

	public static class SceneManager
	{
		public static float FrameTime;
		public static float RunningTime;

		static FramebufferCreator fbCreator;
		public static Framebuffer LightFramebuffer;
		public static Framebuffer DefaultFramebuffer
		{
			get { return fbCreator.defaultFb; }
		}

		public static Shader ShadowPassShader;
		public static Light Light;

		public static GameWindow Window;

		public static Camera Camera
		{
			get { return Scene.Camera; }
		}

		public static Scene Scene = new Scene();

		public static void OnResize()
		{
			Camera.Aspect = (float)Window.Width / Window.Height;
		}

		public static SceneType sceneType = SceneType.Emblems;

		public delegate void SceneChangeHandler(object myobj, SceneTypeArgs args);
		public static event SceneChangeHandler OnSceneTypeChange;

		public static SceneType SceneType
		{
			get { return sceneType; }
			set {
				sceneType = value;
				OnSceneTypeChange.Invoke(new object(), new SceneTypeArgs("Scene changed"));
			}
		}

		public static void Load()
		{
			ShadowPassShader = ShaderLoader.GetShader("Shadow.xsp");
			fbCreator = new FramebufferCreator();
			
			LightFramebuffer= fbCreator.createFrameBuffer("LightFramebuffer", SceneManager.Window.Width, SceneManager.Window.Height, PixelInternalFormat.Rgba16f, false);
			LightFramebuffer.clearColor = new OpenTK.Graphics.Color4(0f, 0f, 0f, 0f);

			Scene.Load();

			GL.MatrixMode(MatrixMode.Projection);
			GL.Ortho(0.0, (double)Window.Width, 0.0, (double)Window.Height, -10.0, 10.0);
			GL.MatrixMode(MatrixMode.Modelview);
		}

		public static void Render()
		{
			Scene.Render(RenderPass.Shadow);
			Scene.Render(RenderPass.Render);

			if(Window.Keyboard[OpenTK.Input.Key.P])
			{
				fbCreator.defaultFb.enable(true);
				DrawTextureOnScreenQuad(VTKInt.Textures.TextureLoader.GetTexture("LightFramebufferColor").id, 0.0f, 1.0f);///.LightFramebuffer.ColorTexture, 0.0f);
			}
		}

		public static Ray GetMouseRay()
		{
			int mouseX = Window.Mouse.X;
			int mouseY = Window.Mouse.Y;

			Vector3 sourceNear = new Vector3(mouseX, mouseY, 0.0f);
			Vector3 sourceFar  = new Vector3(mouseX, mouseY, 1.0f);

			Vector3 near = VTKMath.Unproject(sourceNear, Camera.Projection, Camera.View,
			                                 Matrix4.Identity, (float)Window.Width, (float)Window.Height);
			Vector3 far  = VTKMath.Unproject(sourceFar , Camera.Projection, Camera.View,
			                                 Matrix4.Identity, (float)Window.Width, (float)Window.Height);

			return new Ray(Camera.Eye, Vector3.Normalize(far - near));
		}

		public static void Update()
		{
			if(Window.Keyboard[OpenTK.Input.Key.E])
				sceneType = SceneType.Emblems;
			else if(Window.Keyboard[OpenTK.Input.Key.N])
				sceneType = SceneType.Numad;

			Scene.Update();
		}
		
		public static void DrawTextureOnScreenQuad(int textureId, float s, float e)
		{

			//GL.UseProgram(0);

			Shader shader = ShaderLoader.GetShader("TestQuad.xsp");
			GL.UseProgram(shader.handle);
			GL.Uniform1(GL.GetUniformLocation(shader.handle, "texture"), 0);

			GL.ActiveTexture(TextureUnit.Texture0);
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, textureId);

			GL.Begin(BeginMode.Quads);
				GL.TexCoord2(0.0f, 0.0f);
				//GL.Color3(Vector3.UnitX);
				GL.Vertex3((float)Window.Width * s, 0.0f, 0.0f);
				GL.TexCoord2(1.0f, 0.0f);
				//GL.Color3(Vector3.UnitY);
				GL.Vertex3((float)Window.Width * s + (float)Window.Width * e, 0.0f, 0.0f);
				GL.TexCoord2(1.0f, 1.0f);
				//GL.Color3(Vector3.UnitZ);	
				GL.Vertex3((float)Window.Width * s + (float)Window.Width * e, (float)Window.Height, 0.0f);
				GL.TexCoord2(0.0f, 1.0f);
				//GL.Color3(Vector3.UnitZ);
				GL.Vertex3((float)Window.Width * s, (float)Window.Height, 0.0f);
			GL.End();
		}
	}

	public enum SceneType
	{
		Emblems,
		Numad
	}

	public enum RenderPass
	{
		Render,
		Shadow
	}

}

