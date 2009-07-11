using System;
using System.Windows;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WpfRegexDesigner.Model
{
	public class ScreenLayoutOptions : PropertyChangeNotificationBase
	{

		private GridLengthInfoCollection _GridLengths = new GridLengthInfoCollection();
		public GridLengthInfoCollection GridLengths
		{
			get { return _GridLengths; }
			set { _GridLengths = value; }
		}

		[XmlIgnore]
		public GridLength GridSplitterSize
		{
			get { return _GridLengths.GetDefaultedGridLength("GridSplitterSize", 8, GridUnitType.Pixel); }
			set
			{
				SetColumnWidthValueByPropertyName(value, "GridSplitterSize");
			}
		}

		#region Window Height and Width

		private double _WindowWidth = 800;
		public double WindowWidth
		{
			get { return _WindowWidth; }
			set
			{
				if (_WindowWidth.Equals(value))
				{
					return;
				}
				_WindowWidth = value;
				SendChangeNotification("WindowWidth");
			}
		}

		private double _WindowHeight = 600;
		public double WindowHeight
		{
			get { return _WindowHeight; }
			set
			{
				if (_WindowHeight.Equals(value))
				{
					return;
				}
				_WindowHeight = value;
				SendChangeNotification("WindowHeight");
			}
		}

		#endregion

		private void SetColumnWidthValueByPropertyName(GridLength newValue, string propertyName)
		{
			if (_GridLengths[propertyName].Equals(newValue))
			{
				return;
			}
			_GridLengths.SetGridLength(propertyName, newValue);
			SendChangeNotification(propertyName);
		}
		private void SetColumnWidthValueByIndex(int index, GridLength newValue)
		{
			string propertyName = string.Format("Column{0}Width", index);

			SetColumnWidthValueByPropertyName(newValue, propertyName);
		}
		[XmlIgnore]
		public GridLength Column0Width
		{
			get { return _GridLengths.GetDefaultedGridLength("Column0Width", 5, GridUnitType.Star); }
			set
			{
				SetColumnWidthValueByIndex(0, value);
			}
		}

		//[XmlIgnore]
		//public GridLength Column1Width
		//{
		//   get { return _gridLengths[1]; }
		//   set
		//   {
		//      SetColumnWidthValue(1, value);
		//   }
		//}


		[XmlIgnore]
		public GridLength Column2Width
		{
			get { return _GridLengths.GetDefaultedGridLength("Column2Width", 5, GridUnitType.Star); }
			set { SetColumnWidthValueByIndex(2, value); }
		}

		[XmlIgnore]
		public GridLength Column4Width
		{
			get { return _GridLengths.GetDefaultedGridLength("Column4Width", 3, GridUnitType.Star); }
			set
			{
				SetColumnWidthValueByIndex(4, value);
			}
		}

		[XmlIgnore]
		public GridLength LeftColumnRow0Height
		{
			get { return _GridLengths.GetDefaultedGridLength("LeftColumnRow0Height", 5, GridUnitType.Star); }
			set { SetColumnWidthValueByPropertyName(value, "LeftColumnRow0Height"); }
		}
		[XmlIgnore]
		public GridLength LeftColumnRow2Height
		{
			get { return _GridLengths.GetDefaultedGridLength("LeftColumnRow2Height", 5, GridUnitType.Star); }
			set { SetColumnWidthValueByPropertyName(value, "LeftColumnRow2Height"); }
		}
		[XmlIgnore]
		public GridLength LeftColumnRow4Height
		{
			get { return _GridLengths.GetDefaultedGridLength("LeftColumnRow4Height", 5, GridUnitType.Star); }
			set { SetColumnWidthValueByPropertyName(value, "LeftColumnRow4Height"); }
		}
		[XmlIgnore]
		public GridLength LeftColumnRow6Height
		{
			get { return _GridLengths.GetDefaultedGridLength("LeftColumnRow6Height", 5, GridUnitType.Star); }
			set { SetColumnWidthValueByPropertyName(value, "LeftColumnRow6Height"); }
		}

	}
}
