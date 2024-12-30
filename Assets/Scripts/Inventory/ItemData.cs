using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="Item", menuName = "Data/Item", order = 1)]

public class ItemData : ScriptableObject
{ 
    public string id;
    public string description;
    public Sprite sprite;
    public GameObject prefab;
    public int value; // weight
    public string type;

    private void OnValidate()
    {
        // if id is not give, I will give it one (auto-generate ID)
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }
}
