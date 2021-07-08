using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    void WinTheGame()
    {
        SceneManager.LoadScene("Win", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
