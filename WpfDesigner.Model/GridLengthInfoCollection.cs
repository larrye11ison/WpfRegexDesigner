using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace WpfRegexDesigner.Model
{
	public class GridLengthInfoCollection : KeyedCollection<string, GridLengthInfo>
	{
		protected override string GetKeyForItem(GridLengthInfo item)
		{
			return item.Key;
		}

		public void SetGridLength(string key, GridLength value)
		{
			GridLengthInfo gli = GetItemByKey(key);
			if (gli == null)
			{
				gli = new GridLengthInfo(key, value);
				this.Add(gli);
			}
			else
			{
				this[key].Value = value;
			}
		}

		private GridLengthInfo GetItemByKey(string key)
		{
			return (from k in this
					  where (string.Compare(k.Key, key, true) == 0)
					  select k).FirstOrDefault();
		}

		public GridLength GetDefaultedGridLength(string key, double defaultSize, GridUnitType unitType)
		{
			GridLengthInfo gli = GetItemByKey(key);
			if (gli == null)
         {
				gli = new GridLengthInfo(key, new GridLength(defaultSize, unitType));
				this.Add(gli);
			}
			return gli.Value;
		}
		
		public GridLengthInfoCollection()
		{
		}
	}

	
	
}
