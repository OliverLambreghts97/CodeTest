using UnityEngine;

namespace TankGame
{
	public class RocketBehavior : ProjectileBehavior
	{
		#region Editor Fields

		[SerializeField] private CapsuleCollider _collider;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private float _explosionRadius;
		[SerializeField] private float _castDistance;

		#endregion

		#region Methods

		protected override void HandleExplosion()
		{
			// Sphere cast here with a certain radius to hit enemies
			RaycastHit[] hits = Physics.SphereCastAll(transform.position, _explosionRadius,
				transform.forward, _castDistance, _layerMask);

			GameState gameState = GameState.Instance;
			foreach (RaycastHit hit in hits)
			{
				hit.collider.GetComponent<HealthBehavior>()?.ChangeHealth(-_damagePerHit);

				if (gameState.GamePhase != GamePhase.MainMenu) continue;
				
				hit.collider.GetComponent<IInteractiveButton>()?.Activate();
			}
		}

		protected void Update()
		{
			if (Physics.SphereCast(transform.position, _collider.radius, transform.forward,
					out RaycastHit hitInfo, 1f, _layerMask))
			{
				OnExplode();
			}
		}

		#endregion

		#if UNITY_EDITOR

		#region Debug

		[ExecuteAlways]
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _explosionRadius);
		}

		#endregion

		#endif
	}
}