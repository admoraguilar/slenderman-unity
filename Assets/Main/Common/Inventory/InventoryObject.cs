using UnityEngine;

namespace Slenderman
{
	[CreateAssetMenu(menuName = "Slenderman/Inventory Object")]
	public class InventoryObject : ScriptableObject
	{
		public Inventory inventory { get; private set; } = new Inventory();
	}
}
