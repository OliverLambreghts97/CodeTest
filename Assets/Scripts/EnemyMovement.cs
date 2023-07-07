using UnityEngine;

namespace TankGame
{
	public class EnemyMovement : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private float _speed;

		#endregion

		#region Fields

		private Transform _playerTransform;
		private Vector3 _lookAt = new Vector3();

		#endregion

		#region Life Cycle

		protected void Start()
		{
			_playerTransform = PlayerData.Instance.transform;
		}

		#endregion

		#region Methods

		protected void Update()
		{
			// Orientation
			_lookAt = (_playerTransform.position - transform.position).normalized;
			_lookAt.y = 0f;
			transform.forward = _lookAt;
			
			// Displacement
			transform.position += _lookAt * (Time.deltaTime * _speed);
		}

		#endregion
	}
}