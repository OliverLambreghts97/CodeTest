using System;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
	#region Editor Fields

	[SerializeField] private float _maxHealth;

	#endregion

	#region Fields

	private float _currentHealth;
	private float _oldHealth;
	private bool _isDead = false;

	#endregion

	#region Events

	public event Action<float> OnHealthChanged;
	public event Action OnDied;

	#endregion
	
	#region Life Cycle

	protected void Start()
	{
		ResetHealth();
	}

	protected void OnEnable()
	{
		ResetHealth();
	}

	#endregion

	#region Methods

	private void ResetHealth()
	{
		_currentHealth = _maxHealth;
		_isDead = false;
	}

	public void ChangeHealth(float amount)
	{
		if (_isDead) return;
		
		_oldHealth = _currentHealth;
		_currentHealth += amount;
		_currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
		
		print(_currentHealth);
		
		OnHealthChanged?.Invoke(_currentHealth / _maxHealth);

		if (_currentHealth == 0f)
		{
			_isDead = true;
			OnDied?.Invoke();
		}
	}

	#endregion
}