using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
	public class PlayerMovement : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private float _speed;
		[SerializeField] private float _rotateDuration;
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private GameObject _mainBody;
		[SerializeField] private AudioClip _drivingSound;
		[SerializeField] private AudioClip _idleSound;
		[SerializeField] private AudioSource _audioSource;

		#endregion

		#region Fields

		private Vector3 _velocity;
		private Vector3 _orientation;
		private float _elapsedTime;
		private float _rotatePercentage;

		#endregion

		#region Callbacks

		public void OnMove(InputAction.CallbackContext context)
		{
			var input = context.ReadValue<Vector2>();

			_velocity.x = _speed * input.x;
			_velocity.z = _speed * input.y;

			_orientation.x = input.x;
			_orientation.z = input.y;

			_elapsedTime = 0f;

			bool isStill = _velocity.sqrMagnitude == 0f;

			_audioSource.clip = isStill ? _idleSound : _drivingSound;
			_audioSource.Play();
		}

		#endregion

		#region Methods

		protected void Update()
		{
			if (_velocity.sqrMagnitude == 0f)
				return;

			_elapsedTime += Time.deltaTime;
			_rotatePercentage = _elapsedTime / _rotateDuration;

			_mainBody.transform.forward = Vector3.Lerp(_mainBody.transform.forward, _orientation,
				_rotatePercentage);
			_characterController.Move(_velocity * Time.deltaTime);
		}

		#endregion
	}
}