using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfRegexDesigner.Model
{
	/// <summary>
	/// A discrete Capture resulting from execution of a regular expression.
	/// </summary>
	/// <remarks>
	/// Hierarchically binding the .net Match/Group/Capture objects directly to
	/// the WPF TreeView caused issues - I believe this is because the framework 
	/// objects are part of an inheritance hierarchy (Match inherits from Group, 
	/// which in turn inherits from Capture). By re-modeling these objects here,
	/// I avoided that issue, but there may be a cooler way to do the hierarchical
	/// binding to avoid it entirely.
	/// </remarks>
	public class RegexCapture : IRegexInfo
	{
		/// <summary>
		/// Initializes a new instance of the RegexCapture class.
		/// </summary>
		private RegexCapture()
		{
		}
		public RegexCapture(System.Text.RegularExpressions.Capture capture)
		{
			this._Value = capture.Value;
			this._Index = capture.Index;
			this._Length = capture.Length;
		}
		private string _Value;
		public string Value
		{
			get { return _Value; }
		}
		private int _Length;
		public int Length
		{
			get { return _Length; }
		}
		private int _Index;
		public int Index
		{
			get { return _Index; }
		}
	}
}
