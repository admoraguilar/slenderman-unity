using UnityEngine;

namespace Slenderman
{
	public class CollectItemQuantityGameRule : MonoBehaviour
	{
		[SerializeField]
		private Item _item = null;

		[SerializeField]
		private int _itemCountToSatisfy = 8;

		[SerializeField]
		private InventoryObject _inventoryObject = null;

		private bool _isSatisfied = false;

		private void Update()
		{
			if(!_isSatisfied &&
			   _inventoryObject.inventory.GetItemList(_item.key).Count >= _itemCountToSatisfy) {
				_isSatisfied = true;
				Debug.Log("WIN");
			}
		}
	}
}
