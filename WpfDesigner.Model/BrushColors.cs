using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Reflection;

namespace WpfRegexDesigner.Model
{
	public class BrushColors : Dictionary<string, Color>
	{
		/// <summary>
		/// Dictionary of basic colors obtained from the <see cref="T:System.Windows.Media.Colors">Colors</see> class.
		/// </summary>
		public BrushColors()
		{
			var props = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
			foreach (PropertyInfo pi in props)
			{
				Color color = (Color)pi.GetValue(null, BindingFlags.Static, null, null, null);
				this.Add(pi.Name, color);
			}
		}
	}
}
