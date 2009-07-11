using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace WpfRegexDesigner.Model
{
	/// <summary>
	/// Abstract notion of an individual RegexOption enumerated value.
	/// </summary>
	/// <remarks>
	/// Includes the ability to be "selected" so that a UI can bind to a checkbox.
	/// </remarks>
	public class RegexOptionsValue : PropertyChangeNotificationBase
	{
		/// <summary>
		/// Initializes a new instance of the RegexOptionsValue class.
		/// </summary>
		private RegexOptionsValue()
		{
		}

		/// <summary>
		/// Initializes a new instance of the RegexOptionsValue class.
		/// </summary>
		/// <param name="value"></param>
		public RegexOptionsValue(int value)
		{
			_Value = (RegexOptions)value;
		}

		private bool _IsSelected = false;
		public bool IsSelected
		{
			get { return _IsSelected; }
			set
			{
				if (_IsSelected.Equals(value)) return;
				_IsSelected = value;
				SendChangeNotification("IsSelected");
			}
		}

		private string _Description = string.Empty;
		public string Description
		{
			get { return _Description; }
			set
			{
				var victor = value ?? string.Empty;
				if (_Description.Equals(victor)) return;
				_Description = victor;
				SendChangeNotification("Description");
			}
		}

		private RegexOptions _Value = RegexOptions.None;
		public RegexOptions Value
		{
			get { return _Value; }
			set
			{
				if (_Value.Equals(value)) return;
				_Value = value;
				SendChangeNotification("Value");
			}
		}

		public string Name
		{
			get { return ToString(); }
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
