using UnityEngine;

namespace TankGame
{
	public class EnemyRocketWeapon : RocketWeapon
	{
		#region Editor Fields
		
		[SerializeField] private Transform _turretTransform;

		#endregion
		
		#region Fields

		private Timer _shootTimer;

		#endregion

		#region Life Cycle

		protected void OnEnable()
		{
			_shootTimer.RestartTimer(_cooldown);
		}

		protected void OnDisable()
		{
			_shootTimer.Invalidate();
		}

		#endregion

		#region Methods

		protected new void Update()
		{
			base.Update();
			if (_shootTimer.Expired() == false) return;
			Shoot();
		}

		private void Shoot()
		{
			_shootTimer.RestartTimer(_cooldown);
			bool hasFoundElement = GetPooledElement(out GameObject rocket);
			if (hasFoundElement == false) return;
			rocket.transform.SetPositionAndRotation(_turretTransform.position, _turretTransform.rotation);
		}

		#endregion
	}
}