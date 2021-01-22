using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slenderman
{
	public static class ComponentExtensions
	{
		public static T GetComponent<T>(this Component component, ref T cache) where T : Component
		{
			if(cache == null) { cache = component.GetComponent<T>(); }
			return cache;
		}
	}
}
