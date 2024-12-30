using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UIManager : MonoBehaviour
{
    public enum ScreenState
    {
        MainMenu,
        PauseMenu,
        SaveAndLoadMenu,
        Game,
        Inventory,
        GameOver
    }

    private ScreenState _previousState;
    private ScreenState _currentScreenState;

    public ScreenState CurrentScreenState
    {
        get
        {
            return _currentScreenState;
        }
        set
        {
            _previousState = _currentScreenState;
            _currentScreenState = value;
            ChangeState(value);
        }
    }

    [SerializeField] private GameObject _mainMenu; // Canvas Parent of MainMenu

    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _contextMenu;
    [SerializeField] private GameObject _saveAndLoadMenu;

    public MouseClickMovement mouseClickMovement;
    [SerializeField] private GameObject _playerCanvas;

    private void Start()
    {
        CurrentScreenState = ScreenState.MainMenu;
    }

    private void ChangeState(ScreenState state)
    {
        _mainMenu.SetActive(false);
        _inventory.SetActive(false);
        _pauseMenu.SetActive(false);
        _contextMenu.SetActive(false);
        _saveAndLoadMenu.SetActive(false);
        mouseClickMovement.enabled = false;
        _playerCanvas.SetActive(true);

        switch (state)
        {
            case ScreenState.MainMenu:
                Time.timeScale = 0f; // pause game
                _mainMenu.SetActive(true);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _contextMenu.SetActive(false);
                _playerCanvas.SetActive(false);
                break;
            case ScreenState.PauseMenu:
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
                _saveAndLoadMenu.SetActive(false);
                break;
            case ScreenState.SaveAndLoadMenu:
                Time.timeScale = 0f;
                _pauseMenu.SetActive(false);
                _saveAndLoadMenu.SetActive(true);
                break;
            case ScreenState.Game:
                mouseClickMovement.enabled = true;
                Time.timeScale = 1f;
                break;
            case ScreenState.Inventory:
                _inventory.SetActive(true);
                Time.timeScale = 1f;
                break;
            case ScreenState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }

}
