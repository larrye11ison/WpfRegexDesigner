using System;
using System.Windows;
using System.Windows.Media;

namespace WpfRegexDesigner.Model
{
	public class RegexDesignerApplicationOptions : PropertyChangeNotificationBase
	{
		private TextWrapping _InputBoxTextWrapping = TextWrapping.NoWrap;
		public TextWrapping InputBoxTextWrapping
		{
			get { return _InputBoxTextWrapping; }
			set { _InputBoxTextWrapping = value; }
		}

		private Color _MatchedTextBackgroundColor;
		public Color MatchedTextBackgroundColor
		{
			get { return _MatchedTextBackgroundColor; }
			set { _MatchedTextBackgroundColor = value; }
		}
		private Color _MatchedTextForegroundColor;
		public Color MatchedTextForegroundColor
		{
			get { return _MatchedTextForegroundColor; }
			set { _MatchedTextForegroundColor = value; }
		}

	}
}
