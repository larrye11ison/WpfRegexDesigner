using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WpfRegexDesigner.Model
{
    /// <summary>
    /// A discrete Match resulting from execution of a regular expression.
    /// </summary>
    /// <remarks>
    /// Hierarchically binding the .net Match/Group/Capture objects directly to
    /// the WPF TreeView caused issues - I believe this is because the framework
    /// objects are part of an inheritance hierarchy (Match inherits from Group,
    /// which in turn inherits from Capture). By re-modeling these objects here,
    /// I avoided that issue, but there may be a cooler way to do the hierarchical
    /// binding to avoid it entirely (and just bind to the native match/group/capture
    /// objects instead).
    /// </remarks>
    public class RegexMatch : IRegexInfo
    {
        private List<RegexGroup> _Groups = new List<RegexGroup>();
        private int _Index;

        private int _Length;

        private string _Value;

        /// <summary>
        /// Initializes a new instance of the RegexMatch class.
        /// </summary>
        public RegexMatch(Match match, Regex regex)
        {
            int i = 0;
            foreach (Group g in match.Groups)
            {
                if (i != 0)
                {
                    string groupName = regex.GroupNameFromNumber(i);
                    _Groups.Add(new RegexGroup(g, groupName));
                }
                i++;
            }
            this._Value = match.Value;
            this._Index = match.Index;
            this._Length = match.Length;
        }

        /// <summary>
        /// Initializes a new instance of the RegexMatch class.
        /// </summary>
        private RegexMatch()
        {
        }

        public List<RegexGroup> Groups
        {
            get { return _Groups; }
        }

        public int Index
        {
            get { return _Index; }
        }

        public int Length
        {
            get { return _Length; }
        }

        public string Value
        {
            get { return _Value; }
        }
    }
}