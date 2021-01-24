using System.Collections;
using UnityEngine;

namespace Slenderman.Characters
{
	public class SlendermanAI : MonoBehaviour
	{
		public Transform playerCharacter;
		public Transform slendermanTransform;

		[Space]
		public Camera pOVCamera = null;
		public Renderer slendermanRenderer = null;

		private IEnumerator GeneratePointsContinuously()
		{
			while(true) {
				yield return new WaitForSeconds(1f);
				Debug.Log("Setting slenderman position");
				GeneratePosition();
			}
		}

		private void GeneratePosition()
		{
			Vector2 randomCirclePoint = Vector3.zero;
			Vector3 generatedPosition = Vector3.zero;
			int triesCount = 0;

			bool isPositionInCone = false;
			bool isPositionInDistance = false;

			do {
				randomCirclePoint = Random.insideUnitCircle;
				generatedPosition =
					playerCharacter.position +
					(new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y) * 13f);

				isPositionInCone = MathUtilities.IsPointInCone(
					generatedPosition, playerCharacter.position,
					-playerCharacter.forward * 6.5f, 10f * Mathf.Deg2Rad, 13f);
				isPositionInDistance = Vector3.Distance(
					playerCharacter.position, generatedPosition) >= 5f;

				if(triesCount++ > 50) {
					Debug.LogWarning("Error finding position");
					break;
				}
			} while(!isPositionInCone || !isPositionInDistance);

			slendermanTransform.position = generatedPosition;
		}

		private void Start()
		{
			StartCoroutine(GeneratePointsContinuously());
		}

		private void Update()
		{
			if(slendermanRenderer.IsVisibleFrom(pOVCamera)) {
				Debug.Log("Slenderman is in view");
			} else {
				Debug.Log("Slenderman isn't in view");
			}
		}
	}
}
