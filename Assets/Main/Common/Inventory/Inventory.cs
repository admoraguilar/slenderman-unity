using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Slenderman
{
	public class Inventory
	{
		private Dictionary<string, List<Item>> _itemLookup = new Dictionary<string, List<Item>>();

		public List<Item> GetItemList(string itemKey)
		{
			Assert.IsTrue(itemKey != string.Empty);

			if(!_itemLookup.TryGetValue(itemKey, out List<Item> itemList)) {
				_itemLookup[itemKey] = itemList = new List<Item>();
			}
			return itemList;
		}

		public void AddItem(Item item)
		{
			Assert.IsNotNull(item);

			List<Item> itemList = GetItemList(item.key);
			itemList.Add(item);
		}

		public void RemoveItem(Item item)
		{
			Assert.IsNotNull(item);

			List<Item> itemList = GetItemList(item.key);
			if(itemList.Count > 0) { itemList.Remove(item); }
		}
	}
}
