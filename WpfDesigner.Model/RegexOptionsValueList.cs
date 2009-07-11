using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Xml;
using System.Windows;

namespace WpfRegexDesigner.Model
{
	public class RegexOptionsValueList : ObservableCollection<RegexOptionsValue>
	{
		/// <summary>
		/// Initializes a new instance of the RegexOptionsValueList class.
		/// </summary>
		/// <remarks>
		/// Excludes values of None and Compiled because neither is meaningful 
		/// in the context of this application.
		/// </remarks>
		public RegexOptionsValueList()
			: this(ff => (ff != (int)RegexOptions.None && ff != (int)RegexOptions.Compiled))
		{
			XDocument xd = null;
			var fileName = "WpfRegexDesigner.Model.RegexOptionsDescriptions.xml";
			System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
			using (System.IO.StreamReader strr = new System.IO.StreamReader(ass.GetManifestResourceStream(fileName)))
			{
				try
				{
					xd = XDocument.Load(strr);
				}
				catch (Exception ex)
				{
					foreach (var item in this)
					{
						item.Description = ex.ToString();
					}
					return;
				}
			}
			foreach (RegexOptionsValue item in this)
			{
				var qry = from desc in xd.Descendants("descriptions").Descendants("description")
							 where (string.Compare(desc.Attribute("name").Value, item.Name, true) == 0)
							 select desc;

				var description = "Description not found.";
				if (qry != null)
				{
					var tehFirst = qry.FirstOrDefault();
					if (tehFirst != null)
					{
						description = tehFirst.Attribute("value").Value;
					}
				}
				item.Description = description;
			}
		}

		/// <summary>
		/// Ensures that duplicate RegexOption values are not put into the list.
		/// </summary>
		/// <remarks>
		/// Between this object's natural desire to load the items in the collection directly 
		/// from the actual enumeration type and the XML Serialization of .net, duplicate
		/// values will end up in this list. This override ensures that this does not happen. When
		/// a duplicate item is inserted, this method will effectively copy the item rather than
		/// inserting a duplicate.
		/// </remarks>
		/// <param name="index"></param>
		/// <param name="item"></param>
		protected override void InsertItem(int index, RegexOptionsValue item)
		{
			var q = (
				from i in this
				where (i.Value == item.Value)
				select i).FirstOrDefault();

			if (q == null)
			{
				base.InsertItem(index, item);
			}
			else
			{
				q.IsSelected = item.IsSelected;
				//q.Description = item.Description;
			}
		}

		public RegexOptions Value
		{
			get
			{
				RegexOptions retVal = 0;
				foreach (var item in this)
				{
					if (item.IsSelected)
					{
						retVal |= item.Value;
					}
				}
				return retVal;
			}
			set
			{
				foreach (var item in this)
				{
					item.IsSelected = ((value & item.Value) > 0);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the RegexOptionsValueList class.
		/// </summary>
		/// <param name="filterFunction">
		/// A function that returns true for each candidate value that should be included in the collection.
		/// </param>
		public RegexOptionsValueList(Func<int, bool> filterFunction)
		{
			foreach (int item in Enum.GetValues(typeof(System.Text.RegularExpressions.RegexOptions)))
			{
				if (filterFunction(item))
				{
					this.Add(new RegexOptionsValue(item));
				}
			}
		}
	}
}