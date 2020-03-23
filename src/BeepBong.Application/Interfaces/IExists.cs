using System;
using System.Collections.Generic;
using System.Text;

namespace BeepBong.Application.Interfaces
{
	public interface IExists<T>
	{
		bool Exists(T model);
	}
}
