using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
	public abstract class BaseWeapon : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] protected float _cooldown;

		#endregion

		#region Fields

		private Timer _timer;
		protected bool _isActive = false;
		private InputAction.CallbackContext _currentContext;

		#endregion

		#region Methods

		protected abstract void Shoot(InputAction.CallbackContext context);

		protected void Update()
		{
			if (_isActive)
			{
				if (_timer.IsRunning) return;
				
				_timer.RestartTimer(_cooldown);
				Shoot(_currentContext);
			};
		}

		#endregion

		#region Callbacks

		public void OnShoot(InputAction.CallbackContext context)
		{
			_isActive = context.performed;
			_currentContext = context;
		}

		#endregion
	}
}