using System;
using OpenTK;
using VTKInt.Models;
using VTKInt.Structues;
using System.Collections.Generic; 

namespace VTKInt.Interface
{
	public class EmblemPanel : VTKObject
	{
		float distanceBetween;
		List<Emblem> emblems = new List<Emblem>();

		public EmblemPanel () : base()
		{
		}

		public float DistanceBetween
		{
			get {return distanceBetween;}
			set {
				this.distanceBetween = value;
				UpdateEmblemPositions();
			}
		}

		public void AddEmblem (Emblem mesh)
		{
			this.emblems.Add(mesh);
			UpdateEmblemPositions();
		}

		public override void Render (RenderPass pass)
		{
			foreach(Emblem emblem in emblems)
			{
				emblem.Render(pass);
			}
			base.Render (pass);
		}

		public override void Update ()
		{
			UpdateEmblemPositions();

			foreach(Emblem emblem in emblems)
			{
				emblem.Position += this.Position;
				emblem.Update();
			}
			base.Update ();
		}

		public void UpdateEmblemPositions()
		{
			for(int i = 0; i < emblems.Count; i ++)
			{
				Vector3 pos = new Vector3(- emblems.Count * distanceBetween / 2.0f, 0.0f, 0.0f);
				pos.X += i * distanceBetween;
				emblems[i].Position = pos;
			}
		}

//		public void Load()
//		{
//			XDocument doc = XDocument.Load("../../Content/Emblems.xml"); 
//			var b1 = doc.Descendants("book")
//				.Where(b => b.Elements("author") 
//				       .Elements("first") 
//				       .Any(f => (string)f == "Serge"));
//		}
	}
}

