using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfRegexDesigner.Controller
{
	public class RegexDesignerCommands
	{
		public static RoutedUICommand ExecuteRegexCommand;
		public static RoutedUICommand ToolsOptionsCommand;
		public static RoutedUICommand EditRegexCommand;
		public static RoutedUICommand EditInputTextCommand;

		static RegexDesignerCommands()
		{
			InputGestureCollection executeRegexInputs = new InputGestureCollection();
			executeRegexInputs.Add(new KeyGesture(Key.F5));

			InputGestureCollection editRegexInputs = new InputGestureCollection();
			editRegexInputs.Add(new KeyGesture(Key.R, ModifierKeys.Control));

			InputGestureCollection editInputTextInputs = new InputGestureCollection();
			editInputTextInputs.Add(new KeyGesture(Key.I, ModifierKeys.Control));

			ExecuteRegexCommand = new RoutedUICommand("Execute Regex", "ExecuteRegex", typeof(RegexDesignerCommands), executeRegexInputs);
			ToolsOptionsCommand = new RoutedUICommand("Options", "Options", typeof(RegexDesignerCommands));
			EditRegexCommand = new RoutedUICommand("Edit Regex", "EditRegex", typeof(RegexDesignerCommands), editRegexInputs);
			EditInputTextCommand = new RoutedUICommand("Edit Input Text", "EditInputText", typeof(RegexDesignerCommands), editInputTextInputs);
		}
	}
}
