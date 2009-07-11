using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfRegexDesigner.Model;

namespace WpfRegexDesigner.View
{
	/// <summary>
	/// Interaction logic for OptionsDialog.xaml
	/// </summary>
	public partial class OptionsDialog : Window
	{
		public OptionsDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the OptionsDialog class.
		/// </summary>
		/// <param name="options"></param>
		public OptionsDialog(RegexDesignerScreenData data)
			: this()
		{
			_Options = data.ApplicationOptions;
			mainGrid.DataContext = _Options;
			ObjectDataProvider layoutProvider = (ObjectDataProvider)this.FindResource("layoutOptionsDataProvider");
			layoutProvider.ObjectInstance = data.LayoutOptions;
		}

		private RegexDesignerApplicationOptions _Options;
		public RegexDesignerApplicationOptions Options
		{
			get { return _Options; }
			set { _Options = value; }
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
