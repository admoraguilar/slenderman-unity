using UnityEngine;

namespace Slenderman.Characters
{
	[ExecuteAlways]
	public class FirstPersonCamera : MonoBehaviour
	{
		public Transform follow = null;

		public new Transform transform => this.GetComponent(ref _transform);
		private Transform _transform = null;

		private void UpdateTransform()
		{
			if(follow == null) { return; }

			transform.position = follow.position;
			transform.rotation = follow.rotation;
		}

		private void Update()
		{
			// Editor behaviour
			if(!Application.isPlaying) {
				UpdateTransform();
			}
		}

		private void LateUpdate()
		{
			UpdateTransform();
		}
	}
}
