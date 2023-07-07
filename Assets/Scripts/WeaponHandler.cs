using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
	public enum WeaponType
	{
		Rocket = 0,
		Flamethrower = 1,
		Grenade = 2
	}
	
	public class WeaponHandler : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private BaseWeapon[] _weapons;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private AudioClip _shotFiring;

		#endregion
		
		#region Fields

		private BaseWeapon _currentWeapon;
		private int _currentIndex;

		#endregion

		#region Events

		public event Action<WeaponType> OnWeaponChanged;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			SetWeapon(0);
		}

		#endregion

		#region Callbacks

		public void OnShoot(InputAction.CallbackContext context)
		{
			_currentWeapon?.OnShoot(context);
			_audioSource.clip = _shotFiring;
			_audioSource.Play();
		}

		public void OnCycleGun(InputAction.CallbackContext context)
		{
			if (context.performed == false) return;

			int oldIndex = _currentIndex;

			float scrollValue = context.ReadValue<Vector2>().y;
			switch (scrollValue)
			{
				case > 0f:
					++_currentIndex;
					break;
				case < 0f:
					--_currentIndex;
					break;
			}

			if (_currentIndex == oldIndex) return;
			
			SetWeapon(_currentIndex);
		}

		public void OnSelectPrimary(InputAction.CallbackContext context)
		{
			SetWeapon(0);
		}
		
		public void OnSelectSecondary(InputAction.CallbackContext context)
		{
			SetWeapon(1);
		}
		
		public void OnSelectTertiary(InputAction.CallbackContext context)
		{
			SetWeapon(2);
		}

		#endregion

		#region Methods

		private void SetWeapon(int idx)
		{
			_currentIndex = Math.Clamp(idx, 0, _weapons.Length - 1);
			_currentWeapon = _weapons[_currentIndex];
			OnWeaponChanged?.Invoke((WeaponType)_currentIndex);
		}

		#endregion
	}
}