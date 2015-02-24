using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WpfRegexDesigner.Model
{
    /// <summary>
    /// A discrete Group resulting from execution of a regular expression.
    /// </summary>
    /// <remarks>
    /// Hierarchically binding the .net Match/Group/Capture objects directly to
    /// the WPF TreeView caused issues - I believe this is because the framework
    /// objects are part of an inheritance hierarchy (Match inherits from Group,
    /// which in turn inherits from Capture). By re-modeling these objects here,
    /// I avoided that issue, but there may be a cooler way to do the hierarchical
    /// binding to avoid it entirely.
    /// </remarks>
    public class RegexGroup : IRegexInfo
    {
        private List<RegexCapture> _Captures = new List<RegexCapture>();
        private int _Index;

        private int _Length;

        private string _Name;

        private string _Value;

        public RegexGroup(Group group, string name)
        {
            foreach (Capture c in group.Captures)
            {
                _Captures.Add(new RegexCapture(c));
            }
            this._Name = name;
            this._Value = group.Value;
            this._Index = group.Index;
            this._Length = group.Length;
        }

        /// <summary>
        /// Initializes a new instance of the RegexGroup class.
        /// </summary>
        private RegexGroup()
        {
        }

        public List<RegexCapture> Captures
        {
            get { return _Captures; }
        }

        public int Index
        {
            get { return _Index; }
        }

        public int Length
        {
            get { return _Length; }
        }

        public string Name
        {
            get { return _Name; }
        }

        public string Value
        {
            get { return _Value; }
        }
    }
}