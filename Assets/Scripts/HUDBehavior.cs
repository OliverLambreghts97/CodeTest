using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TankGame
{
	public class HUDBehavior : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private CanvasGroup _mainHUDGroup;
		[SerializeField] private CanvasGroup _gameOverGroup;
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private float _canvasFadeDuration;
		[SerializeField] private float _healthSlideDuration;
		[SerializeField] private Image[] _weaponImgs = new Image[3];
		[SerializeField] private Slider _healthSlider;
		[SerializeField] private Button _mainMenuButton;

		#endregion

		#region Life Cycle

		protected void Start()
		{
			PlayerData playerData = PlayerData.Instance;
			playerData.OnScoreChanged += OnScoreChanged;
			playerData.WeaponHandler.OnWeaponChanged += OnWeaponChanged;
			playerData.HealthBehavior.OnHealthChanged += OnHealthChanged;
			playerData.HealthBehavior.OnDied += OnPlayerDied;
			OnWeaponChanged(WeaponType.Rocket);
			_gameOverGroup.alpha = 0f;
			_mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
			_mainMenuButton.interactable = false;
		}

		protected void OnDestroy()
		{
			PlayerData playerData = PlayerData.Instance;
			playerData.OnScoreChanged -= OnScoreChanged;
			playerData.WeaponHandler.OnWeaponChanged -= OnWeaponChanged;
			playerData.HealthBehavior.OnHealthChanged -= OnHealthChanged;
			playerData.HealthBehavior.OnDied -= OnPlayerDied;
			_mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
		}

		#endregion

		#region Methods

		private void DoFadeCanvas(CanvasGroup canvas, bool fadeIn)
		{
			if (fadeIn)
			{
				canvas.gameObject.SetActive(true);
				canvas.alpha = 0f;
				canvas.blocksRaycasts = true;
				canvas.DOKill();
				canvas.DOFade(1f, _canvasFadeDuration).SetEase(Ease.OutCubic);
			}
			else
			{
				canvas.blocksRaycasts = false;
				canvas.DOKill();
				canvas.DOFade(0f, _canvasFadeDuration).SetEase(Ease.OutCubic).OnComplete(() => 
					{ canvas.gameObject.SetActive(false); });
			}
		}

		#endregion

		#region Callbacks

		private void OnMainMenuButtonClicked()
		{
			GameState.Instance.GamePhase = GamePhase.MainMenu;
			SceneManager.LoadScene((int)GamePhase.MainMenu);
		}
		
		private void OnPlayerDied()
		{
			DoFadeCanvas(_mainHUDGroup, false);
			DoFadeCanvas(_gameOverGroup, true);
			_mainMenuButton.interactable = true;
		}
		
		private void OnHealthChanged(float healthPercentage)
		{
			if (_healthSlider.value <= 0f) return;

			_healthSlider.DOValue(healthPercentage, _healthSlideDuration);
		}
		
		private void OnWeaponChanged(WeaponType weaponType)
		{
			for (int i = 0; i < _weaponImgs.Length; ++i)
			{
				_weaponImgs[i].gameObject.SetActive((int)weaponType == i);
			}
		}

		private void OnScoreChanged(int newScore)
		{
			_scoreText.SetText(newScore.ToString());
		}

		#endregion
	}
}