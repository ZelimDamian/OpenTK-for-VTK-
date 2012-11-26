using System;
using OpenTK;
using VTKInt.Models;
using VTKInt.Structues;

namespace VTKInt.Interface
{
	public class DigitDisplay : Model
	{
		string digits;

		float distBetween;

		public DigitDisplay (float distBetween, string materialName)
		{
			AddMaterial(Materials.MaterialLoader.GetMaterial(materialName));
			this.distBetween = distBetween;
		}

		public void AddDigit(string digit)
		{
			digits += digit;
			UpdateDigits();
		}

		public void RemoveLastDigit()
		{
			digits = digits.Remove(digits.Length - 1);
			UpdateDigits();
		}

		public void UpdateDigits()
		{
			components.Clear();

			for(int i = 0; i < digits.Length; i ++)
			{
				Vector3 pos = - digits.Length * distBetween / 2.0f * Vector3.UnitX;
				pos += Vector3.UnitX * i * distBetween;

				Component comp = new Component(digits[i] + ".obj");

					comp.Position = pos;

				components.Add(comp);
			}
		}

		public String Digits
		{
			get {
				return digits;
			}
			set {
				digits = value;
				UpdateDigits();
			}
		}
	}
}

