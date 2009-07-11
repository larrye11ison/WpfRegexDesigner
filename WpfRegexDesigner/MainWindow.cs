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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using WpfRegexDesigner.Controller;

namespace WpfRegexDesigner
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Model.RegexDesignerScreenData regexData = null;

		private static WpfRegexDesigner.Model.RegexDesignerScreenData GetDefaultRegexData()
		{
			Color backColor = new Color();
			Color foreColor = new Color();
			backColor.R = 255;
			foreColor.R = foreColor.G = foreColor.B = 0;

			Model.RegexDesignerApplicationOptions appOptions = new WpfRegexDesigner.Model.RegexDesignerApplicationOptions
			{
				MatchedTextBackgroundColor = backColor,
				MatchedTextForegroundColor = foreColor,
				InputBoxTextWrapping = TextWrapping.NoWrap
			};
			Model.RegexOptionsValueList rovl = new WpfRegexDesigner.Model.RegexOptionsValueList();
			rovl.Value = RegexOptions.ExplicitCapture;
			Model.RegexDesignerScreenData rsd = new Model.RegexDesignerScreenData
			{
				InputText = "This is some sample text.",
				RegularExpression = "(?<w>\\w+)",
				Options = rovl,
				ReplacementExpression = "${w}\n",
				ApplicationOptions = appOptions
			};
			return rsd;
		}
		public MainWindow()
		{
			InitializeComponent();

			regexData = Properties.Settings.Default.RegexData ?? GetDefaultRegexData();
			DataContext = regexData;
		}

		void editInputTextCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			inputTextTabControl.SelectedIndex = 0;
			inputTextBox.SelectAll();
			inputTextBox.Focus();
		}

		void editRegexCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			regexTextBox.Focus();
			regexTextBox.SelectAll();
		}

		void toolsOptionsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			View.OptionsDialog optionsDialogue = new WpfRegexDesigner.View.OptionsDialog(regexData);
			optionsDialogue.Owner = this;
			optionsDialogue.ShowDialog();
		}

		void cbd_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ExecuteRegex();
		}

		/// <summary>
		/// Executes the regular expression and sets up the UI accordingly.
		/// </summary>
		void ExecuteRegex()
		{
			try
			{
				resultsTreeView.Focus();
				regexData.Execute();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					string.Format(
					"There was a problem executing the expression:{0}{0}{1}", Environment.NewLine, ex.ToString()),
					"Error",
					MessageBoxButton.OK, MessageBoxImage.Error);

				return;
			}
			inputTextTabControl.SelectedItem = inputTextResultsTab;
			ExpandFirstLevelTreeNodes(resultsTreeView);
		}

		/// <summary>
		/// When a new object is selected in the Results tree view, highlights the corresponding (matched)
		/// text in the inputRichTextBox control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected void resultsTreeView_SelectedItemChanged(object sender, RoutedEventArgs args)
		{
			if (resultsTreeView.SelectedItem == null)
			{
				return;
			}
			Model.IRegexInfo rinfo = resultsTreeView.SelectedItem as Model.IRegexInfo;
			System.Diagnostics.Debug.Assert(rinfo != null, "resultsTreeView_SelectedItemChanged could not coerce the tree view's selected item into an IRegexInfo");

			inputTextTabControl.SelectedItem = inputTextResultsTab;
			regexData.SetMatchedTextProperties(rinfo.Index, rinfo.Length);
		}

		private void ExpandFirstLevelTreeNodes(TreeView tree)
		{
			foreach (object item in tree.Items)
			{
				TreeViewItem treeItem = tree.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
				treeItem.IsExpanded = true;
			}
		}

		private void MainWindow_Closed(object sender, EventArgs args)
		{
			//try
			//{
			//   System.IO.StringWriter swrtr = new System.IO.StringWriter();
			//   System.Xml.Serialization.XmlSerializer xsl = new System.Xml.Serialization.XmlSerializer(typeof(Model.RegexDesignerScreenData));
			//   xsl.Serialize(swrtr, regexData);
				
			//}
			//catch (Exception ex)
			//{
			//   MessageBox.Show(ex.ToString());
			//}
			
			Properties.Settings.Default.RegexData = regexData;
			Properties.Settings.Default.Save();
		}
		
		private void itemsList_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				e.Handled = true;
				ListBox lbox = sender as ListBox;
				if (lbox == null)
				{
					MessageBox.Show(sender.GetType().FullName);
					return;
				}
				Model.RegexOptionsValue rov = lbox.SelectedItem as Model.RegexOptionsValue;
				if (rov == null)
				{
					MessageBox.Show(lbox.SelectedItem.GetType().FullName);
					return;
				}
				rov.IsSelected = !rov.IsSelected;
			}
		}
	}
}
