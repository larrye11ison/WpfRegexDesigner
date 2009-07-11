using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace WpfRegexDesigner.Hacks
{
	/// <summary>    
	/// Represents a bindable rich editing control which operates on System.Windows.Documents.FlowDocument    
	/// objects.        
	/// </summary>    
	public class BindableRichTextBox : RichTextBox
	{
		/// <summary>        
		/// Identifies the <see cref="Document"/> dependency property.        
		/// </summary>        
		public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox));
		
		/// <summary>        
		/// Initializes a new instance of the <see cref="BindableRichTextBox"/> class.        
		/// </summary>        
		public BindableRichTextBox() : base() { }
		
		/// <summary>        
		/// Initializes a new instance of the <see cref="BindableRichTextBox"/> class.        
		/// </summary>        
		/// <param title="document">
		/// A <see cref="T:System.Windows.Documents.FlowDocument"></see> 
		/// to be added as the initial contents of the new <see cref="T:System.Windows.Controls.BindableRichTextBox"></see>.
		/// </param>        
		public BindableRichTextBox(FlowDocument document) : base(document) 
		{ }

		/// <summary>        
		/// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"></see> event. 
		/// This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"></see> 
		/// is set to true internally.        
		/// </summary>        
		/// <param title="e">The <see cref="T:System.Windows.RoutedEventArgs"></see> that contains the event data.</param>        
		protected override void OnInitialized(EventArgs e)
		{
			// Hook up to get notified when DocumentProperty changes.            
			DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(DocumentProperty, typeof(BindableRichTextBox));
			descriptor.AddValueChanged(this, delegate
			{
				// If the underlying value of the dependency property changes,                
				// update the underlying document, also.                
				base.Document = (FlowDocument)GetValue(DocumentProperty);
			});
			// By default, we support updates to the source when focus is lost (or, if the LostFocus            
			// trigger is specified explicity.  We don't support the PropertyChanged trigger right now.            
			this.LostFocus += new RoutedEventHandler(BindableRichTextBox_LostFocus);
			base.OnInitialized(e);
		}
		/// <summary>        
		/// Handles the LostFocus event of the BindableRichTextBox control.        
		/// </summary>        
		/// <param title="sender">The source of the event.</param>        
		/// <param title="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>        
		void BindableRichTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			// If we have a binding that is set for LostFocus or Default (which we are specifying as default)            
			// then update the source.            
			Binding binding = BindingOperations.GetBinding(this, DocumentProperty);
			if (binding.UpdateSourceTrigger == UpdateSourceTrigger.Default || binding.UpdateSourceTrigger == UpdateSourceTrigger.LostFocus)
			{
				BindingOperations.GetBindingExpression(this, DocumentProperty).UpdateSource();
			}
		}
		/// <summary>        
		/// Gets or sets the <see cref="T:System.Windows.Documents.FlowDocument"></see> 
		/// that represents the contents of the <see cref="T:System.Windows.Controls.BindableRichTextBox"></see>.        
		/// </summary>        
		/// <value></value>        
		/// <returns>A <see cref="T:System.Windows.Documents.FlowDocument"></see> object that represents 
		/// the contents of the <see cref="T:System.Windows.Controls.BindableRichTextBox"></see>. By default, 
		/// this property is set to an empty <see cref="T:System.Windows.Documents.FlowDocument"></see>.  
		/// Specifically, the empty <see cref="T:System.Windows.Documents.FlowDocument"></see> contains a 
		/// single <see cref="T:System.Windows.Documents.Paragraph"></see>, which contains a single 
		/// <see cref="T:System.Windows.Documents.Run"></see> which contains no text.</returns>        
		/// <exception cref="T:System.ArgumentException">Raised if an attempt is made to set this property 
		/// to a <see cref="T:System.Windows.Documents.FlowDocument"></see> that represents the contents of 
		/// another <see cref="T:System.Windows.Controls.RichTextBox"></see>.</exception>        
		/// <exception cref="T:System.ArgumentNullException">Raised if an attempt is made to set this 
		/// property to null.</exception>        
		/// <exception cref="T:System.InvalidOperationException">Raised if this property is set while a change 
		/// block has been activated.</exception>        
		public new FlowDocument Document 
		{ 
			get { return (FlowDocument)GetValue(DocumentProperty); } 
			set { SetValue(DocumentProperty, value); } 
		}
	}
}
