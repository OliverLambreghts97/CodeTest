using UnityEngine;

namespace TankGame
{
    public enum GamePhase
    {
        MainMenu = 0,
        MainGame = 1,
        GameOver = 2
    }
    
    public class GameState : MonoBehaviour
    {
        #region SINGLETON

        private static GameState _instance;

        public static GameState Instance
        {
            get
            {
                if (_instance == null && !_applicationQuiting)
                {
                    _instance = FindObjectOfType<GameState>();
                    if (_instance == null)
                    {
                        GameObject newObject = new GameObject("Singleton_GameState");
                        _instance = newObject.AddComponent<GameState>();
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

        #region Properties

        public GamePhase GamePhase
        {
            get;
            set;
        }

        #endregion

        #region Life Cycle

        protected void Start()
        {
            GamePhase = GamePhase.MainMenu;
            PlayerData.Instance.HealthBehavior.OnDied += () =>
            {
                GamePhase = GamePhase.GameOver;
            };
        }

        #endregion
    }
}