using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SaveAndLoadMenu : MonoBehaviour
{
    [SerializeField] GameObject _saveLabel;
    [SerializeField] GameObject _loadLabel;

    [SerializeField] List<TMP_Text> _slotsLabels; // contain dates

    SavingSystem _savingSystem;

    [SerializeField] private UIManager _manager; 

    SavingSystem SavingSystem
    {
        get
        {
            if (_savingSystem == null)
                _savingSystem = FindObjectOfType<SavingSystem>();
            return _savingSystem;
        }
    }

    bool _isSaveMode = false;
    private const string EMPTY_SLOT_LABEL = "-EMPTY-";

    public void Open(bool isSaveMode)
    {
        _manager.CurrentScreenState = UIManager.ScreenState.SaveAndLoadMenu; 
        _isSaveMode = isSaveMode;

        if (_isSaveMode)
        {
            _saveLabel.SetActive(true);
            _loadLabel.SetActive(false);
        }
        else
        {
            _saveLabel.SetActive(false);
            _loadLabel.SetActive(true);
        }


        foreach (var label in _slotsLabels)
        {
            label.text = EMPTY_SLOT_LABEL;
        }

        var files = SavingSystem.GetFilesInPersistentDirectory();
        for (int i=0; i < files.Length; i++)
        {
            _slotsLabels[i].text = File.GetLastWriteTime(files[i]).ToString();
        }
    }


    public void OnSlotClicked(int index)
    {
        if (_isSaveMode == true)
        {
            SavingSystem.SerializeAndSave(index);
        }
        else
        {
            SavingSystem.LoadAndDeserialize(index);
        }
        Close();
    }

    public void Close()
    {
        _manager.CurrentScreenState = UIManager.ScreenState.Game;
    }

    public void Back()
    {
        _manager.CurrentScreenState = UIManager.ScreenState.PauseMenu;
    }
}
