using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Code.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
