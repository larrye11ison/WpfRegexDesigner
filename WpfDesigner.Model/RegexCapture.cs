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
        private int _Index;

        private int _Length;

        private string _Value;

        public RegexCapture(System.Text.RegularExpressions.Capture capture)
        {
            this._Value = capture.Value;
            this._Index = capture.Index;
            this._Length = capture.Length;
        }

        /// <summary>
        /// Initializes a new instance of the RegexCapture class.
        /// </summary>
        private RegexCapture()
        {
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