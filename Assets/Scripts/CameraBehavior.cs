using UnityEngine;

namespace TankGame
{
	public class CameraBehavior : MonoBehaviour
	{
		#region Editor Fields
		
		[SerializeField] private Transform _targetTransform;
		[SerializeField] private float _smoothTime;

		#endregion

		#region Fields

		private Vector3 _currentVelocity = new Vector3();
		private PlayerData _playerData;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			_playerData = PlayerData.Instance;
		}

		#endregion

		#region Methods

		protected void Update()
		{
			Camera camera = _playerData.Camera;
			Vector3 smoothedPosition = Vector3.SmoothDamp(camera.transform.position, _targetTransform.position, 
				ref _currentVelocity, _smoothTime);
			camera.transform.position = smoothedPosition;
		}

		#endregion
	}
}