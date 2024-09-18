using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.EndGame
{
    public class EndGameService
    {
        public void EndGame()
        {
            Debug.Log("Game over!");
            DOVirtual.DelayedCall(2f, RestartScene);
        }

        private void RestartScene() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}