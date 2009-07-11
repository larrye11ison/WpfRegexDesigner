using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfRegexDesigner.Model
{
	public abstract class PropertyChangeNotificationBase : INotifyPropertyChanged
	{
		protected void SendChangeNotification(string propertyName)
		{
			if (PropertyChanged != null)
         {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
		}


		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
