using System.Windows;
using System.Windows.Media;

namespace WpfRegexDesigner.Model
{
    public class RegexDesignerApplicationOptions : PropertyChangeNotificationBase
    {
        private TextWrapping _InputBoxTextWrapping = TextWrapping.NoWrap;
        private Color _MatchedTextBackgroundColor;

        private Color _MatchedTextForegroundColor;

        public TextWrapping InputBoxTextWrapping
        {
            get { return _InputBoxTextWrapping; }
            set { _InputBoxTextWrapping = value; }
        }

        public Color MatchedTextBackgroundColor
        {
            get { return _MatchedTextBackgroundColor; }
            set { _MatchedTextBackgroundColor = value; }
        }

        public Color MatchedTextForegroundColor
        {
            get { return _MatchedTextForegroundColor; }
            set { _MatchedTextForegroundColor = value; }
        }
    }
}