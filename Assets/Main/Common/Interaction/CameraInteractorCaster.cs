using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Slenderman
{
	public class CameraInteractorCaster : InteractorCaster<RaycastHit>
	{
		private class RaycastHitEqualityComparer : EqualityComparer<RaycastHit>
		{
			public override bool Equals(RaycastHit x, RaycastHit y)
			{
				if(x.transform == null && y.transform == null) { return true; }
				else if(x.transform == null || y.transform == null) { return false; }
				return x.transform.GetInstanceID() == y.transform.GetInstanceID();
			}

			public override int GetHashCode(RaycastHit obj)
			{
				if(obj.transform == null) { return -1; } 
				else { return obj.transform.GetInstanceID(); }
			}
		}

		public new Camera camera
		{
			get {
				if(_camera == null) {
					_camera = Camera.main;
				}
				return _camera;
			}
		}
		[SerializeField]
		private Camera _camera = null;

		public float rayLength = 5f;
		
		private void FixedUpdate()
		{
			Ray cameraCenterRay = camera.ViewportPointToRay(Vector3.one * .5f);

			RaycastHitEqualityComparer comparer = new RaycastHitEqualityComparer();
			RaycastHit[] newHits = Physics.RaycastAll(cameraCenterRay, rayLength);

			IEnumerable<RaycastHit> exittingHits = hits.Except(newHits, comparer);
			foreach(RaycastHit hit in exittingHits) { TriggerOnHitExit(hit); }
			
			IEnumerable<RaycastHit> enterringHits = newHits.Except(hits, comparer);
			foreach(RaycastHit hit in enterringHits) { TriggerOnHitEnter(hit); }

			hits = newHits;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Color oldColor = Gizmos.color;
			Gizmos.color = Color.red;

			Ray cameraCenterRay = camera.ViewportPointToRay(Vector3.one * .5f);
			Gizmos.DrawLine(
				cameraCenterRay.origin, 
				cameraCenterRay.origin + (cameraCenterRay.direction * rayLength));

			Gizmos.color = oldColor;
		}
#endif
	}
}
