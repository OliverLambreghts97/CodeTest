using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
	public class GrenadeWeapon : BaseWeapon
	{
		#region Editor Fields

		[SerializeField] private GameObject _grenadeTemplate;
		[SerializeField] private int _poolSize;

		#endregion

		#region Fields

		private GameObject[] _grenadePool;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			InitializePool();
		}

		private void InitializePool()
		{
			_grenadePool = new GameObject[_poolSize];

			for (int i = 0; i < _poolSize; ++i)
			{
				GameObject grenade = Instantiate(_grenadeTemplate, transform.position, Quaternion.identity);
				grenade.SetActive(false);
				_grenadePool[i] = grenade;
				DontDestroyOnLoad(grenade);
			}
		}

		#endregion

		#region Methods

		protected override void Shoot(InputAction.CallbackContext context)
		{
			Transform grenadeSocket = PlayerData.Instance.GrenadeSocketTransform;
			bool hasFoundElement = GetPooledElement(out GameObject grenade);
			if (hasFoundElement == false) return;
			grenade.transform.SetPositionAndRotation(grenadeSocket.position, grenadeSocket.rotation);
		}

		private bool GetPooledElement(out GameObject element)
		{
			foreach (GameObject grenade in _grenadePool)
			{
				if (grenade.activeInHierarchy == false)
				{
					grenade.SetActive(true);
					element = grenade;
					return true;
				}
			}

			element = null;
			return false;
		}

		#endregion
	}
}