using UnityEngine;

namespace TankGame
{
	public class EnemySpawner : MonoBehaviour
	{
		#region Editor Fields
		
		[SerializeField] private int _poolSize;
		[SerializeField] private float _spawnDelay;
		[SerializeField] private float _spawnRadius;
		[SerializeField] private float _delayIncrement;

		#endregion
		
		#region Fields

		private GameObject[] _enemyPool;
		private Timer _spawnTimer;
		private const float EasySpawnChance = 0.5f;
		private const float MediumSpawnChance = 0.7f;
		private int _currentSpawnIdx = 0;

		#endregion
		
		#region Life Cycle
		
		protected void Start()
		{
			InitializePool();
			_spawnTimer.RestartTimer(_spawnDelay);
		}

		private void InitializePool()
		{
			_enemyPool = new GameObject[_poolSize];
            
			for (int i = 0; i < _poolSize; ++i)
			{
				GameObject enemy;
				Vector3 spawnPos = GetRandomSpawnPosition(transform.position, _spawnRadius);
				
				switch ((float)i / _poolSize)
				{
					case < EasySpawnChance: 
						enemy = EnemyFactory.CreateEnemy(EnemyType.Easy, spawnPos, transform.rotation);
						break;
					case < MediumSpawnChance: 
						enemy = EnemyFactory.CreateEnemy(EnemyType.Medium, spawnPos, transform.rotation);
						break;
					case > MediumSpawnChance: 
						enemy = EnemyFactory.CreateEnemy(EnemyType.Hard, spawnPos, transform.rotation);
						break;
					default: 
						enemy = EnemyFactory.CreateEnemy(EnemyType.Easy, spawnPos, transform.rotation);
						break;
				}
				
				enemy.SetActive(false);
				_enemyPool[i] = enemy;
			}
		}
		
		#endregion

		#region Methods

		protected void Update()
		{
			if (_spawnTimer.Expired() == false) return;

			_spawnDelay = Mathf.Clamp(_spawnDelay - _delayIncrement, 0.1f, 100f);
			_spawnTimer.RestartTimer(_spawnDelay);
			Spawn();
		}

		private void Spawn()
		{
			bool hasFoundElement = GetPooledElement(out GameObject enemy);
			if (hasFoundElement == false) return;
			enemy.transform.position = GetRandomSpawnPosition(transform.position, _spawnRadius);
		}
		
		private Vector3 GetRandomSpawnPosition(Vector3 center, float radius)
		{
			Vector3 randomDirection = Random.insideUnitSphere;
			randomDirection.y = 0f;
			Vector3 spawnPosition = center + randomDirection.normalized * radius;
			return spawnPosition;
		}
		
		private bool GetPooledElement(out GameObject element)
		{
			for(int i = _currentSpawnIdx; i < _enemyPool.Length; ++i)
			{
				GameObject enemy = _enemyPool[i];
				if (enemy.activeInHierarchy == false)
				{
					enemy.SetActive(true);
					element = enemy;
					++_currentSpawnIdx;
					_currentSpawnIdx = _currentSpawnIdx >= _enemyPool.Length ? 0 : _currentSpawnIdx;
					return true;
				}
			}

			_currentSpawnIdx = 0;
			element = null;
			return false;
		}

		#endregion
	}
}