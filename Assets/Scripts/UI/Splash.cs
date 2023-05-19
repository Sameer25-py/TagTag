using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Splash : MonoBehaviour
    {
        private void Start()
        {
            Invoke(nameof(LoadMainMenu), 1.5f);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}