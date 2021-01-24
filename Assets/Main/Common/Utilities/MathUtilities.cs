using UnityEngine;

namespace Slenderman
{
	public static class MathUtilities
	{
		// Source: http://nic-gamedev.blogspot.com/2011/11/using-vector-mathematics-and-bit-of.html
		public static bool IsPointInCone(
			Vector3 point, Vector3 coneTip,
			Vector3 coneCenter, float fovRadians,
			float maxDistance)
		{
			Vector3 diff = point - coneTip;

			float length = diff.magnitude;
			if(length > maxDistance) { return false; }

			return Vector3.Dot(coneCenter, diff) >= Mathf.Cos(fovRadians);
		}
	}
}
