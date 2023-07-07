using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
	public class FlameWeapon : BaseWeapon
	{
		#region Editor Fields

		[SerializeField] private ParticleSystem _flameParticle;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private float _rayCastDistance;
		[SerializeField] private float _flameRadius;
		[SerializeField] private float _damagePerHit;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			_flameParticle.Stop();
		}

		#endregion
		
		#region Methods

		protected override void Shoot(InputAction.CallbackContext context)
		{
			if (_flameParticle.isPlaying == false)
			{
				_flameParticle.Play();
			}
		}

		protected new void Update()
		{
			base.Update();

			if (_isActive == false && _flameParticle.isPlaying)
			{
				_flameParticle.Stop();
			}
			else if(_isActive && _flameParticle.isPlaying)
			{
				Transform turretTransform = PlayerData.Instance.TurretTransform;
				RaycastHit[] hits = Physics.CapsuleCastAll(turretTransform.position, 
					turretTransform.position + (turretTransform.forward * _rayCastDistance), _flameRadius,
					turretTransform.forward, _rayCastDistance, _layerMask);

				foreach (RaycastHit hit in hits)
				{
					hit.collider.GetComponent<HealthBehavior>()?.ChangeHealth(-_damagePerHit);
				}
			}
		}

		#endregion

		#if UNITY_EDITOR

		#region Debug

		[ExecuteAlways]
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _flameRadius);
		}

		#endregion

		#endif
	}
}