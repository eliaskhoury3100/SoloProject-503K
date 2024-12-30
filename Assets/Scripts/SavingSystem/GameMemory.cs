using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMemory : MonoBehaviour
{
    public List<Collectable.Data> collectables;
    public Inventory.Data inventory;

    public Vector3 playerPosition;
    public Vector3 golemPosition;

}
