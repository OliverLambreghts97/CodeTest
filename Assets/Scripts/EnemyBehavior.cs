using UnityEngine;

namespace TankGame
{
	public class EnemyBehavior : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private HealthBehavior _healthBehavior;
		[SerializeField] private int _scoreValue;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private AudioClip _tankExplosion;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			_healthBehavior.OnDied += OnDied;
		}

		protected void OnDestroy()
		{
			_healthBehavior.OnDied -= OnDied;
		}

		#endregion

		#region Callbacks

		private void OnDied()
		{
			PlayerData.Instance.PlayerScore += _scoreValue;
			gameObject.SetActive(false);
			_audioSource.clip = _tankExplosion;
			_audioSource.Play();
		}

		#endregion
	}
}