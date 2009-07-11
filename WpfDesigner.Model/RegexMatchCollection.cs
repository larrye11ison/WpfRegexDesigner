using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WpfRegexDesigner.Model
{
	public class RegexMatchCollection : List<RegexMatch>
	{
		private RegexMatchCollection()
		{
		}
		private static RegexMatchCollection tehEmptyCollection = new RegexMatchCollection();
		public static RegexMatchCollection Empty
		{
			get
			{
				return tehEmptyCollection;
			}
		}

		/// <summary>
		/// Initializes a new instance of the RegexMatchCollection class.
		/// </summary>
		public RegexMatchCollection(MatchCollection matches, Regex regex)
		{
			foreach (Match match in matches)
			{
				this.Add(new RegexMatch(match, regex));
			}
		}
	}
}
