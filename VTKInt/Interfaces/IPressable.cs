using System;

namespace VTKInt
{
	public interface IPressable
	{
		bool IsPressed
		{
			set;
			get;
		}

		void Press();
		void Release();

		float PressDuration
		{
			set;
			get;
		}
	}
}

