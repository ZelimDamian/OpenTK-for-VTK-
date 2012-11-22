using System;
using OpenTK;

namespace VTKInt
{
	public class VTKMath
	{
		public static Vector3 Unproject(Vector3 source, Matrix4 projection, Matrix4 view, Matrix4 world, float Width, float Height)
		{
			Matrix4 matrix = Matrix4.Invert(Matrix4.Mult(Matrix4.Mult(world, view), projection));

			source.X = ((source.X / Width) * 2f) - 1f;
			source.Y = -(((source.Y / Height) * 2f) - 1f);
			source.Z = source.Z * 2.0f - 1.0f;

			Vector3 vector = Vector3.Transform(source, matrix);

			float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;

			vector = vector / a;

			return vector;
			
		}
	}
}

