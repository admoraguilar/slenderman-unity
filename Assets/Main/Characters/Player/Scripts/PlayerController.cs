using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Slenderman.Characters
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private FirstPersonCharacter _fpsCharacter = null;

		[SerializeField]
		private InteractorCaster<RaycastHit> _interactorCaster = null;

		private void OnHitEnter(RaycastHit hit)
		{
			
		}

		private void OnHitStay(IEnumerable<RaycastHit> hits)
		{
			if(Input.GetKeyDown(KeyCode.E)) {
				foreach(RaycastHit hit in hits) {
					Debug.Log($"Queue [{hit.transform.name}] for destruction.");
					Destroy(hit.transform.gameObject);
				}
			}
		}

		private void OnHitExit(RaycastHit hit)
		{

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
