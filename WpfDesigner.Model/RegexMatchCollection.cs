using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WpfRegexDesigner.Model
{
    public class RegexMatchCollection : List<RegexMatch>
    {
        private static RegexMatchCollection tehEmptyCollection = new RegexMatchCollection();

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

        private RegexMatchCollection()
        {
        }

        public static RegexMatchCollection Empty
        {
            get
            {
                return tehEmptyCollection;
            }
        }
    }
}