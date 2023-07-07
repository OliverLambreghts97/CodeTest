using System;
using UnityEngine;

namespace TankGame
{
	public class LifetimeHandler : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private float _lifeTime;

		#endregion

		#region Fields

		private Timer _lifeTimer;

		#endregion

		#region Events

		public event Action OnLifeTimeEnded;

		#endregion

		#region Life Cycle

		protected void OnEnable()
		{
			_lifeTimer.RestartTimer(_lifeTime);
		}

		protected void OnDisable()
		{
			_lifeTimer.Invalidate();
		}

		#endregion

		#region Methods

		protected void Update()
		{
			if (_lifeTimer.Expired() == false || gameObject.activeInHierarchy == false) return;
			
			OnLifeTimeEnded?.Invoke();
			gameObject.SetActive(false);
		}

		#endregion
	}
}