using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
	public class TurretOrientation : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private GameObject _turretBody;

		#endregion

		#region Fields

		private const float AngleOffset = 90f; 
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
			Vector3 mousePos = Mouse.current.position.ReadValue();
			var dir = mousePos - camera.WorldToScreenPoint(_turretBody.transform.position);
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - AngleOffset;
			_turretBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
		}

		#endregion
	}
}