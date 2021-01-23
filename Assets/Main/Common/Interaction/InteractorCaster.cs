using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slenderman
{
	public abstract class InteractorCaster<T> : MonoBehaviour
	{
		public event Action<T> onHitEnter = delegate { };
		public event Action<T> onHitExit = delegate { };

		public IEnumerable<T> hits { get; protected set; } = new T[0];

		protected void TriggerOnHitEnter(T hit) => onHitEnter?.Invoke(hit);
		protected void TriggerOnHitExit(T hit) => onHitExit?.Invoke(hit);
	}
}
