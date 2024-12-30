using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIManager _manager;

    public void PlayGame()
    {
        _manager.CurrentScreenState = UIManager.ScreenState.Game;
        Time.timeScale = 1f; // to play the game
    }


    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
