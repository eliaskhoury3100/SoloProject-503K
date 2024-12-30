using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{

    private string _fileName = "MYSave";
    public GameMemory gameMemory;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private GolemAnimationController _golemAnimationController;

    private void RestoreGameState()
    {
        RestoreCollectables();
        RestoreInventory();
        RestorePlayerPosition();
        RestoreGolemPosition();
    }

    private void RestoreCollectables()
    {
        var collectables = FindObjectsOfType<Collectable>(includeInactive: true);

        foreach (var collectableData in gameMemory.collectables)
        {
            var collectableLookup = Array.Find(collectables, _ => _.ID == collectableData.ID);
            collectableLookup.Load(collectableData); // Load Method in Collectables Script
            // add for positions of NPC and Player
        }
    }

    private void RestoreInventory()
    {
        _inventory.Load(gameMemory.inventory);
    }

    private void RestorePlayerPosition()
    {
        _playerAnimationController.Load(new PlayerAnimationController.Data { playerPosition = gameMemory.playerPosition });
    }

    private void RestoreGolemPosition()
    {
        _golemAnimationController.Load(new GolemAnimationController.Data { golemPosition = gameMemory.golemPosition });
    }

    public List<Collectable.Data> GetCollectableData()
    {
        var collectables = FindObjectsOfType<Collectable>(includeInactive: true); // including the inactive ones

        List<Collectable.Data> data = new List<Collectable.Data>();

        for (int i=0; i < collectables.Length; i++)
        {
            data.Add(collectables[i].Save()); // calls the Data type of collectibles Save Method
        }
        return data;
    }

    public void SerializeAndSave(int index)
    {
        gameMemory = new GameMemory
        {
            collectables = GetCollectableData(),
            inventory = _inventory.Save(),
            playerPosition = _playerAnimationController.Save().playerPosition,
            golemPosition = _golemAnimationController.Save().golemPosition
        };

        string serializeMemory = JsonUtility.ToJson(gameMemory);
        string path = Path.Combine(Application.persistentDataPath, _fileName + index.ToString());
        Debug.Log(path);
        File.WriteAllText(path, serializeMemory);
    }

    public void LoadAndDeserialize(int index)
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, _fileName + index.ToString());
            string rawData = File.ReadAllText(path);
            Debug.Log("This is the rawdata: " + rawData);
            gameMemory = JsonUtility.FromJson<GameMemory>(rawData);
            RestoreGameState();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }


    public string[] GetFilesInPersistentDirectory()
    {
        string path = Application.persistentDataPath;
        return Directory.GetFiles(path);
    }


}
