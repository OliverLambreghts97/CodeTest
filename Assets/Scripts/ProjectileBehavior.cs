using UnityEngine;

namespace TankGame
{
	public abstract class ProjectileBehavior : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private LifetimeHandler _lifetimeHandler;
		[SerializeField] private GameObject _particleTemplate;
		[SerializeField] private float _particleLifeTime;
		[SerializeField] protected float _damagePerHit;
		[SerializeField] private AudioClip _shellExplosion;
		[SerializeField] private AudioSource _audioSource;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			_lifetimeHandler.OnLifeTimeEnded += OnExplode;
		}

		protected void OnDestroy()
		{
			_lifetimeHandler.OnLifeTimeEnded -= OnExplode;
		}

		#endregion

		#region Callbacks

		protected void OnExplode()
		{
			// Spawn grenade explosion particles
			GameObject particle = Instantiate(_particleTemplate, transform.position, Quaternion.identity);
			Destroy(particle, _particleLifeTime);
			HandleExplosion();
			gameObject.SetActive(false);
			_audioSource.clip = _shellExplosion;
			_audioSource.Play();
		}

		#endregion

		#region Methods

		protected abstract void HandleExplosion();

		#endregion
	}
}