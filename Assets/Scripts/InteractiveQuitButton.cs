using UnityEngine;

namespace TankGame
{
    public class InteractiveQuitButton : MonoBehaviour, IInteractiveButton
    {
        #region Editor Fields

        [SerializeField] private float _quitDelay;

        #endregion
        
        #region Fields

        private const string MethodName = "QuitGameDelayed";

        #endregion

        #region Methods

        public void Activate()
        {
            Invoke(MethodName, _quitDelay);
        }
        
        private void QuitGameDelayed()
        {
            Application.Quit();
        }

        #endregion
    }
}