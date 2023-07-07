using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
    public class RocketWeapon : BaseWeapon
    {
        #region Editor Fields

        [SerializeField] private GameObject _rocketTemplate;
        [SerializeField] private int _poolSize;
        [SerializeField] private int _rocketSpeed;

        #endregion

        #region Fields

        private GameObject[] _rocketPool;

        #endregion

        #region Life Cycle

        protected void Start()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            _rocketPool = new GameObject[_poolSize];
            
            for (int i = 0; i < _poolSize; ++i)
            {
                GameObject rocket = Instantiate(_rocketTemplate, transform.position, Quaternion.identity);
                rocket.SetActive(false);
                _rocketPool[i] = rocket;
                DontDestroyOnLoad(rocket);
            }
        }

        #endregion
        
        #region Methods

        protected override void Shoot(InputAction.CallbackContext context)
        {
            Transform turretTransform = PlayerData.Instance.TurretTransform;
            bool hasFoundElement = GetPooledElement(out GameObject rocket);
            if (hasFoundElement == false) return;
            rocket.transform.SetPositionAndRotation(turretTransform.position, turretTransform.rotation);
        }

        protected bool GetPooledElement(out GameObject element)
        {
            foreach (GameObject rocket in _rocketPool)
            {
                if (rocket.activeInHierarchy == false)
                {
                    rocket.SetActive(true);
                    element = rocket;
                    return true;
                }
            }

            element = null;
            return false;
        }

        protected new void Update()
        {
            base.Update();

            foreach (GameObject rocket in _rocketPool)
            {
                if (rocket.activeInHierarchy)
                {
                    rocket.transform.position += rocket.transform.forward * (_rocketSpeed * Time.deltaTime);
                }
            }
        }

        #endregion

        #region Callbacks

        

        #endregion
    }
}