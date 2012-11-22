using System;
using OpenTK;
using VTKInt.Structues;

namespace VTKInt.Interface
{
	public interface ITouchable
	{
		void Touch(Ray ray);
	}
}

