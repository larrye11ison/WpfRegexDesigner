using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace WpfRegexDesigner.Model
{
	public class GridLengthInfo
	{
		public override int GetHashCode()
		{
			return _Value.Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if ((obj is GridLength || obj is GridLengthInfo) == false)
			{
				return false;
			}
			GridLength gl;
			if (obj is GridLength)
			{
				gl = (GridLength)obj;
			}
			else
			{
				gl = ((GridLengthInfo)obj).Value;
			}
			return (gl.ToString() == this.Value.ToString());
		}
		private GridLengthConverter glc = new GridLengthConverter();

		[XmlAttribute("key")]
		public string Key
		{ get; set; }

		private string _stringValue;
		[XmlAttribute("stringvalue")]
		public string StringValue
		{
			get
			{
				return _stringValue;
			}
			set
			{
				GridLength gl = (GridLength)glc.ConvertFrom(value ?? "Auto");
				_Value = gl;
				_stringValue = gl.ToString();
			}
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		private GridLength? _Value = null;
		[XmlIgnore]
		public GridLength Value
		{
			get
			{
				if (_Value == null)
				{
					_Value = (GridLength)glc.ConvertFrom(StringValue ?? "Auto");
				}
				return _Value.Value;
			}
			set
			{
				_Value = value;
				_stringValue = value.ToString();
			}
		}

		/// <summary>
		/// Initializes a new instance of the GridLengthInfo class.
		/// </summary>
		public GridLengthInfo()
		{
		}
		public GridLengthInfo(string key, GridLength gl)
		{
			Key = key;
			Value = gl;
		}
	}
}
