using TankGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveStartButton : MonoBehaviour, IInteractiveButton
{
    #region Editor Fields

    [SerializeField] private float _loadDelay;

    #endregion

    #region Fields

    private const string MethodName = "LoadSceneDelayed";

    #endregion
    
    #region Methods

    public void Activate()
    {
        Invoke(MethodName, _loadDelay);
    }

    private void LoadSceneDelayed()
    {
        PlayerData.Instance.Player.SetActive(false);
        SceneManager.LoadScene((int)GamePhase.MainGame);
    }

    #endregion
}
