using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Slenderman.Environment;

namespace Slenderman.Characters
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private FirstPersonCharacter _fpsCharacter = null;

		[SerializeField]
		private InteractorCaster<RaycastHit> _interactorCaster = null;

		[SerializeField]
		private InventoryObject _inventoryObject = null;

		private void OnHitEnter(RaycastHit hit)
		{
			Debug.Log($"Hit Enter: {hit.transform.name}");
		}

		private void OnHitStay(IEnumerable<RaycastHit> hits)
		{
			if(Input.GetKeyDown(KeyCode.E)) {
				foreach(RaycastHit hit in hits) {
					if(hit.collider.TryGetComponent(out ItemHolder itemHolder)) {
						_inventoryObject.inventory.AddItem(itemHolder.item);
						Debug.Log($"Add item: {itemHolder.item.key}");
					}

					Debug.Log($"Queue [{hit.transform.name}] for destruction.");
					Destroy(hit.transform.gameObject);
				}
			}
		}

		private void OnHitExit(RaycastHit hit)
		{
			Debug.Log($"Hit Exit: {(hit.transform != null ? hit.transform.name : "Destroyed object")}");
		}

		private void Awake()
		{
			Assert.IsNotNull(_fpsCharacter);
			Assert.IsNotNull(_interactorCaster);
		}

		private void OnEnable()
		{
			_interactorCaster.onHitEnter += OnHitEnter;
			_interactorCaster.onHitExit += OnHitExit;
		}

		private void OnDisable()
		{
			_interactorCaster.onHitEnter -= OnHitEnter;
			_interactorCaster.onHitExit -= OnHitExit;
		}

		private void Update()
		{
			if(_interactorCaster.hits != null && _interactorCaster.hits.Count() > 0) {
				OnHitStay(_interactorCaster.hits);
			}
		}

#if UNITY_EDITOR
		private void Reset()
		{
			_fpsCharacter = GetComponentInChildren<FirstPersonCharacter>();
			_interactorCaster = GetComponentInChildren<InteractorCaster<RaycastHit>>();
		}
#endif
	}
}
