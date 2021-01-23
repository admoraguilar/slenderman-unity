using UnityEngine;

namespace Slenderman
{
	[CreateAssetMenu(menuName = "Slenderman/Item")]
	public class Item : ScriptableObject
	{
		public string key = string.Empty;
		public string displayName = string.Empty;
		public Sprite icon = null;
	}
}
