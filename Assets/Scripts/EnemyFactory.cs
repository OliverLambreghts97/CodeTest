using UnityEngine;

namespace TankGame
{
	public enum EnemyType
	{
		Easy = 0,
		Medium = 1,
		Hard = 2
	}
	
	public class EnemyFactory : MonoBehaviour
	{
		#region Fields

		private static GameObject[] _enemyTemplates = new GameObject[3];
		private static bool _hasInitializedData = false;
		private const string EnemyAName = "EnemyA";
		private const string EnemyBName = "EnemyB";
		private const string EnemyCName = "EnemyC";

		#endregion

		#region Methods

		public static GameObject CreateEnemy(EnemyType enemyType, Vector3 position, Quaternion rotation)
		{
			if(_hasInitializedData == false) InitializeEnemies();
			
			GameObject newEnemy = null;
			switch (enemyType)
			{
				case EnemyType.Easy:
					newEnemy = Instantiate(_enemyTemplates[(int)EnemyType.Easy], position, rotation);
					break;
				case EnemyType.Medium:
					newEnemy = Instantiate(_enemyTemplates[(int)EnemyType.Medium], position, rotation);
					break;
				case EnemyType.Hard:
					newEnemy = Instantiate(_enemyTemplates[(int)EnemyType.Hard], position, rotation);
					break;
			}

			return newEnemy;
		}

		private static void InitializeEnemies()
		{
			_hasInitializedData = true;
			_enemyTemplates[(int)EnemyType.Easy] = Resources.Load<GameObject>(EnemyAName);
			_enemyTemplates[(int)EnemyType.Medium] = Resources.Load<GameObject>(EnemyBName);
			_enemyTemplates[(int)EnemyType.Hard] = Resources.Load<GameObject>(EnemyCName);
		}

		#endregion
	}
}