using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace VTKInt.Structues
{
	public class Mesh
	{
		public List<Face> FaceList;

		public uint PositionVboHandle,
					NormalVboHandle,
					TextureVboHandle,
					TangentVboHandle,
					ElementsVBOHandle;

		public Vector3[] PositionData,
						 NormalData,
						 TangentData;

		public Vector2[] TexCoordData;

		public int[]	 ElementsData;

		public List<Vector3> PositionDataList,
							 NormalDataList;
		public List<Vector2> TexCoordDataList;

		public string Directory;

		public string Name;

		public string URL{
			get { return Directory + Name ;}
		}

		public Mesh ()
		{

		}
	}

	public class MeshLoader
	{
		public static List<Mesh> meshes = new List<Mesh>();

		public Mesh GetMesh(string name)
		{
			foreach(Mesh mesh in meshes)
				if(mesh.Name == name)
					return mesh;

			return LoadMesh(name);
		}

		private Mesh LoadMesh(string name)
		{
			Mesh mesh = new Mesh();

			mesh.Name = name;

			LoadMesh(ref mesh);

			return mesh;
		}

		private void LoadMesh(ref Mesh curMesh)
		{
			if(curMesh.Name.Contains(".obj"))
				LoadObj(ref curMesh);
		}
		
		public void generateVBO(ref Mesh target)
		{
			GL.GenBuffers(1, out target.NormalVboHandle);
			GL.GenBuffers(1, out target.PositionVboHandle);
			GL.GenBuffers(1, out target.TextureVboHandle);
			GL.GenBuffers(1, out target.TangentVboHandle);
			GL.GenBuffers(1, out target.ElementsVBOHandle);
	
			
			GL.BindBuffer(BufferTarget.ArrayBuffer, target.NormalVboHandle);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
			                       new IntPtr(target.NormalData.Length * Vector3.SizeInBytes),
			                       target.NormalData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, target.PositionVboHandle);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
			                       new IntPtr(target.PositionData.Length * Vector3.SizeInBytes),
			                       target.PositionData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, target.TextureVboHandle);
			GL.BufferData<Vector2>(BufferTarget.ArrayBuffer,
			                       new IntPtr(target.TexCoordData.Length * Vector2.SizeInBytes),
			                       target.TexCoordData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, target.TangentVboHandle);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
			                       new IntPtr(target.TangentData.Length * Vector3.SizeInBytes),
			                       target.TangentData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, target.ElementsVBOHandle);
			GL.BufferData(BufferTarget.ElementArrayBuffer,
			              new IntPtr(sizeof(uint) * target.ElementsData.Length),
			              target.ElementsData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
		
		public void LoadObj(ref Mesh target)
		{
			List<Vector3> PositionDataList = new List<Vector3> { };
			List<Vector3> NormalDataList = new List<Vector3> { };
			List<Vector2> TexCoordDataList = new List<Vector2> { };
			List<Face> FaceList = new List<Face> { };
			List<Vertex> FpIndiceList = new List<Vertex> { };
			
			// Read the file and display it line by line.
			string line;
			System.IO.StreamReader file =
				new System.IO.StreamReader(target.URL);
			
			while ((line = file.ReadLine()) != null)
			{
				string[] sline = line.Split(new string[]{" "},10,StringSplitOptions.None);
				
				if (sline[0] == "v")
				{
					float X = float.Parse(sline[1]);
					float Y = float.Parse(sline[2]);
					float Z = float.Parse(sline[3]);
					PositionDataList.Add(new Vector3(X, Y, Z));
				}
				
				if (sline[0] == "vn")
				{
					float X = float.Parse(sline[1]);
					float Y = float.Parse(sline[2]);
					float Z = float.Parse(sline[3]);
					NormalDataList.Add(new Vector3(X, Y, Z));
					
				}
				
				if (sline[0] == "vt")
				{
					float X = float.Parse(sline[1]);
					float Y = 1-float.Parse(sline[2]);
					TexCoordDataList.Add(new Vector2(X, Y));
					
				}
				
				if (sline[0] == "f")
				{
					string[] segment = sline[1].Split(new string[] { "/" }, 10, StringSplitOptions.None);
					if (segment.Length == 3)
					{
						Vertex fp1 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						segment = sline[2].Split(new string[] { "/" }, 10, StringSplitOptions.None);
						Vertex fp2 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						segment = sline[3].Split(new string[] { "/" }, 10, StringSplitOptions.None);
						Vertex fp3 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						FaceList.Add(new Face(fp1, fp2, fp3));
					}
					else if (segment.Length == 3)
					{
						Vertex fp1 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						segment = sline[2].Split(new string[] { "/" }, 10, StringSplitOptions.None);
						Vertex fp2 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						segment = sline[3].Split(new string[] { "/" }, 10, StringSplitOptions.None);
						Vertex fp3 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						segment = sline[4].Split(new string[] { "/" }, 10, StringSplitOptions.None);
						Vertex fp4 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);
						
						FaceList.Add(new Face(fp1, fp2, fp3, fp4));
					}
				}
			}
			
			file.Close();
			
			target.PositionDataList = PositionDataList;
			target.NormalDataList = NormalDataList;
			target.TexCoordDataList = TexCoordDataList;
			target.FaceList = FaceList;
			
			parseFaceList(ref target, false);

			generateVBO(ref target);

			meshes.Add(target);
		
		}
		
		public void parseFaceList(ref Mesh target, bool genNormal)
		{
			List<Vector3> PositionDataList = target.PositionDataList;
			List<Vector3> NormalDataList = target.NormalDataList;
			List<Vector2> TexCoordDataList = target.TexCoordDataList;
			List<Face> faceList = target.FaceList;
			
			removeTemp(ref faceList);
			convertToTri(ref faceList);
			
			Vector3[] tmpNormalData = new Vector3[NormalDataList.Count];
			Vector3[] tmpTangentData = new Vector3[NormalDataList.Count];
			Vector3[] normalPositionData = new Vector3[NormalDataList.Count];
			Vector2[] normalUvData = new Vector2[NormalDataList.Count];
			
			List<Vector3> normalHelperList = new List<Vector3> { };
			List<Vector3> tangentHelperList = new List<Vector3> { };
			List<Vector2> normalUvHelperList = new List<Vector2> { };
			List<Vector3> positionHelperlist = new List<Vector3> { };
			
			int faceCount = faceList.Count;
			
			for (int i = 0; i < faceCount; i++)
			{
				// get all the information from Lists into Facelist
				Vector3[] vposition = new Vector3[3];
				Vector3[] vnormal = new Vector3[3];
				Vector2[] vtexture = new Vector2[3];
				for (int j = 0; j < 3; j++)
				{
					vposition[j] = PositionDataList[faceList[i].Vertice[j].Vi];
					vnormal[j] = NormalDataList[faceList[i].Vertice[j].Ni];
					vtexture[j] = TexCoordDataList[faceList[i].Vertice[j].Ti];
				}

				// calculating face normal and tangent
				Vector3 v1 = vposition[1] - vposition[0];
				Vector3 v2 = vposition[2] - vposition[0];
				
				Vector2 vtexture1 = vtexture[1] - vtexture[0];
				Vector2 vtexture2 = vtexture[2] - vtexture[0];
				
				Vector3 fnormal = Vector3.Cross(v1, v2);
				
				float s = 1f / (vtexture2.X - vtexture1.X * vtexture2.Y / vtexture1.Y);
				float r = 1f / (vtexture1.X - vtexture2.X * vtexture1.Y / vtexture2.Y);
				
				Vector3 tangent = Vector3.Normalize(r * v1 + s * v2);
				
				if(tangent == Vector3.Zero){
					Console.WriteLine("Tangent not generated");
				}
				
				Face curFace = faceList[i];
				
				// finding out if normal/tangent can be smoothed
				for (int j = 0; j < 3; j++)
				{
					Vertex curVert = curFace.Vertice[j];
					
					// if Normal[Normalindice] has not been assigned a uv coordinate do so and set normal
					if (normalUvData[curVert.Ni] == Vector2.Zero)
					{
						normalUvData[curVert.Ni] = vtexture[j];
						normalPositionData[curVert.Ni] = vposition[j];
						
						tmpNormalData[curVert.Ni] = fnormal;
						tmpTangentData[curVert.Ni] = tangent;
					}
					else
					{
						// if Normal[Normalindice] is of the same Uv and place simply add
						if (normalUvData[curVert.Ni] == vtexture[j] && normalPositionData[curVert.Ni] == vposition[j])
						{
							tmpNormalData[curVert.Ni] += fnormal;
							tmpTangentData[curVert.Ni] += tangent;
						}
						else
						{
							int helperCount = normalUvHelperList.Count;
							for (int k = 0; k < helperCount; k++)
							{
								// if Normalhelper[Normalindice] is of the same Uv and position simply add
								if (normalUvHelperList[k] == vtexture[j] && positionHelperlist[k] == vposition[j])
								{
									tangentHelperList[k] += tangent;
									normalHelperList[k] += fnormal;
									
									curVert.Normalihelper = k;
								}
							}
							// if matching Normalhelper has not been found create new one
							if (faceList[i].Vertice[j].Normalihelper == -1)
							{
								normalUvHelperList.Add(vtexture[j]);
								
								tangentHelperList.Add(tangent);
								normalHelperList.Add(fnormal);
								positionHelperlist.Add(vposition[j]);
								curVert.Normalihelper = normalUvHelperList.Count - 1;
							}
						}
					}
				}
			}
			
			// put Faces into DataSets (so we can easyly compare them)
			List<VerticeDataSet> vertList = new List<VerticeDataSet> { };
			
			for (int i = 0; i < faceCount; i++)
			{
				Face curFace = faceList[i];
				for (int j = 0; j < 3; j++)
				{
					Vertex oldVert = curFace.Vertice[j];
					
					VerticeDataSet curVert = new VerticeDataSet();
					
					curVert.position = PositionDataList[oldVert.Vi];
					curVert.normal = NormalDataList[oldVert.Ni];
					if (oldVert.Normalihelper != -1)
					{
						if(genNormal)
							curVert.normal = Vector3.Normalize(normalHelperList[oldVert.Normalihelper]); //-dont use calculated normal
						
						curVert.tangent = Vector3.Normalize(tangentHelperList[oldVert.Normalihelper]);
					}
					else
					{
						if (genNormal)
							curVert.normal = Vector3.Normalize(tmpNormalData[oldVert.Ni]); //-dont use calculated normal
						curVert.tangent = Vector3.Normalize(tmpTangentData[oldVert.Ni]);
					}

					curVert.texture = TexCoordDataList[oldVert.Ti];
					
					vertList.Add(curVert);
				}
			}
			
			//Remove unneded verts
			int noVerts = vertList.Count;
			List<VerticeDataSet> newVertList = new List<VerticeDataSet> {};
			List<int> newIndiceList = new List<int> { };
			
			for (int i = 0; i < noVerts; i++)
			{
				VerticeDataSet curVert = vertList[i];
				int curNewVertCount = newVertList.Count;
				int index = -1;
				
				for (int j = curNewVertCount - 1; j >= 0; j--)
				{
					if (newVertList[j].Equals(curVert))
					{
						index = j;
					}
				}
				if (index < 0)
				{
					index = curNewVertCount;
					newVertList.Add(curVert);
				}
				
				newIndiceList.Add(index);
			}
			
			//put Faces into Arrays
			int newIndiceCount = newIndiceList.Count;
			int[] indicesVboData = new int[newIndiceCount];
			
			int newVertCount = newVertList.Count;
			Vector3[] PositionData = new Vector3[newVertCount];
			Vector3[] NormalData = new Vector3[newVertCount];
			Vector3[] TangentData = new Vector3[newVertCount];
			Vector2[] TexCoordData = new Vector2[newVertCount];

			for (int i = 0; i < newVertCount; i++)
			{
				VerticeDataSet curVert = newVertList[i];
				
				PositionData[i] = curVert.position;
				NormalData[i] = curVert.normal;
				TangentData[i] = curVert.tangent;
				TexCoordData[i] = curVert.texture;
			}
			
			for (int i = 0; i < newIndiceCount; i++)
			{
				indicesVboData[i] = newIndiceList[i];
			}
			
//			//calculate a bounding Sphere
//			float sphere = 0;
//			foreach (var vec in PositionData)
//			{
//				float length = vec.Length;
//				if (length > sphere)
//					sphere = length;
//			}
			
			//deleting unneded
			target.PositionDataList = null;
			target.NormalDataList = null;
			target.TangentData = null;
			target.ElementsData = null;

			//returning mesh info ... DONE :D
			target.PositionData = PositionData;
			target.NormalData = NormalData;
			target.TangentData = TangentData;
			target.TexCoordData = TexCoordData;
			target.ElementsData = indicesVboData;
			//target.boundingSphere = sphere;
		}
		
		private void removeTemp(ref List<Face> FaceList)
		{
			int i = 0;
			while (i < FaceList.Count)
			{
				Face curFace = FaceList[i];
				if (curFace.isTemp)
					FaceList.Remove(curFace);
				else
					i++;
			}
		}
		
		private void convertToTri(ref List<Face> FaceList)
		{
			int faces = FaceList.Count;
			for (int i = 0; i < faces; i++)
			{
				Face curFace = FaceList[i];
				if (curFace.Vertice.Length > 3)
				{
					FaceList[i] = new Face(curFace.Vertice[0], curFace.Vertice[1], curFace.Vertice[2]);
					FaceList.Add(new Face(curFace.Vertice[2], curFace.Vertice[3], curFace.Vertice[0]));
				}
			}
		}
		
		public MeshLoader()
		{}
	}
	
	struct VerticeDataSet
	{
		public Vector3 position;
		public Vector3 normal;
		public Vector3 tangent;
		public float[] boneWeight;
		public int[] boneId;
		public Vector2 texture;
		
		public bool Equals(VerticeDataSet vert)
		{
			if ((this.position - vert.position).LengthFast < 0.0001f &&
			    (this.texture - vert.texture).LengthFast < 0.0001f &&
			    (this.normal - vert.normal).LengthFast < 0.001f)
				return true;
			else
				return false;
		}
	}
	
	public class Vertex
	{
		public int Vi = 0;
		public int Ti = 0;
		public int Ni = 0;
		public int Normalihelper = -1;
		
		public Vertex(int vi, int ti, int ni)
		{
			Vi = vi;
			Ti = ti;
			Ni = ni;
		}
		
		public Vertex()
		{
		}
	}
	
	public class Face
	{
		public Vertex[] Vertice;
		public bool isTemp;
		public int position;
		
		public Face(Vertex ind1, Vertex ind2, Vertex ind3)
		{
			Vertice = new Vertex[3];
			Vertice[0] = ind1;
			Vertice[1] = ind2;
			Vertice[2] = ind3;
			// Log.e("VboCube",Vi+"/"+Ti+"/"+Ni);
		}
		
		public Face(Vertex ind1, Vertex ind2, Vertex ind3, Vertex ind4)
		{
			Vertice = new Vertex[4];
			Vertice[0] = ind1;
			Vertice[1] = ind2;
			Vertice[2] = ind3;
			Vertice[3] = ind4;
			// Log.e("VboCube",Vi+"/"+Ti+"/"+Ni);
		}
		
		public Face(int vCount, int position)
		{
			Vertice = new Vertex[vCount];
			this.position = position;
			
			for (int i = 0; i < vCount; i++)
				Vertice[i] = new Vertex();
			
		}
	}
}