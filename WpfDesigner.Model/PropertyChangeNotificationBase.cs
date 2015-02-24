using System.ComponentModel;

namespace WpfRegexDesigner.Model
{
    public abstract class PropertyChangeNotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SendChangeNotification(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}