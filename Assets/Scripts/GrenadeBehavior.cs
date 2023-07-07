using UnityEngine;

namespace TankGame
{
	public class GrenadeBehavior : ProjectileBehavior
	{
		#region Editor Fields

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

			foreach (RaycastHit hit in hits)
			{
				hit.collider.GetComponent<HealthBehavior>()?.ChangeHealth(-_damagePerHit);
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