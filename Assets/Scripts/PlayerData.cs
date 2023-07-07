using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame
{
	public class PlayerData : MonoBehaviour
	{
		#region SINGLETON

		private static PlayerData _instance;

		public static PlayerData Instance
		{
			get
			{
				if (_instance == null && !_applicationQuiting)
				{
					_instance = FindObjectOfType<PlayerData>();
					if (_instance == null)
					{
						GameObject newObject = new GameObject("Singleton_PlayerData");
						_instance = newObject.AddComponent<PlayerData>();
					}
				}

				return _instance;
			}
		}

		private static bool _applicationQuiting = false;

		public void OnApplicationQuit()
		{
			_applicationQuiting = true;
		}

		void Awake()
		{
			DontDestroyOnLoad(gameObject);
			if (_instance == null)
			{
				_instance = this;
			}
			else if (_instance != this)
			{
				Destroy(gameObject);
			}
		}

		#endregion

		#region Editor Fields

		[SerializeField] private WeaponHandler _weaponHandler;
		[SerializeField] private HealthBehavior _healthBehavior;
		[SerializeField] private Transform _turretTransform;
		[SerializeField] private Transform _grenadeSocketTransform;
		[SerializeField] private GameObject _player;

		#region Fields

		private int _playerScore;

		#endregion
		
		#endregion

		#region Events

		public event Action<int> OnScoreChanged;

		#endregion

		#region Properties

		public WeaponHandler WeaponHandler => _weaponHandler;
		public HealthBehavior HealthBehavior => _healthBehavior;
		public Transform TurretTransform => _turretTransform;
		public Transform GrenadeSocketTransform => _grenadeSocketTransform;
		public int PlayerScore
		{
			get => _playerScore;
			set
			{
				_playerScore = value; 
				OnScoreChanged?.Invoke(_playerScore);
			}
		}
		public Camera Camera { get; set; }
		public GameObject Player => _player;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			Camera = Camera.main;
		}

		protected void OnDestroy()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		#endregion

		#region Callbacks

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
		{
			Camera = Camera.main;
			_player.SetActive(true);
		}

		#endregion
	}
}