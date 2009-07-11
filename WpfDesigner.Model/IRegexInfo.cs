using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfRegexDesigner.Model
{
	public interface IRegexInfo
	{
		string Value
		{
			get;
		}
		int Length
		{
			get;
		}
		int Index
		{
			get;
		}
	}
}
