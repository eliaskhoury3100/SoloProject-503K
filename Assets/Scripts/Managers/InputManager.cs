using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UIManager _manager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // press I
        {
            _manager.CurrentScreenState = UIManager.ScreenState.Inventory; // set equal to Inventory ScreenState enum
        }
        else if (Input.GetKeyDown(KeyCode.P)) // press P
        {
            _manager.CurrentScreenState = UIManager.ScreenState.PauseMenu;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) // press Escape
        {
            _manager.CurrentScreenState = UIManager.ScreenState.Game;
        }
    }
}
