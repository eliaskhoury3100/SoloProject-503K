using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [System.Serializable]
    // I don't want to save everythg from Collectable, just Data
    // I want to save the collectable as a spawner with ID and whether it was collected
    public class Data
    {
        public string ID = string.Empty;
        public bool isCollected = false;
    }


    [SerializeField] private ItemData _data;
    private GameObject _collectableInstance;
    private const string PLAYER_TAG = "Player";
    Inventory _inventory;

    public string ID => gameObject.GetInstanceID().ToString(); // given by Unity at instantiation
    bool isCollected = false;

    

    private void Start()
    {
        InstantiatePrefab();
        _inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected)
            return;
        if (other.CompareTag(PLAYER_TAG))
        {
            Debug.Log("colliding");
            _inventory.AddItem(_data);
            Destroy(this._collectableInstance); // badel gameObject  <-- ? ?? ? ?
            isCollected = true;
        }
    }

    private void InstantiatePrefab()
    {
        _collectableInstance = GameObject.Instantiate(_data.prefab, parent: this.transform); // instantiate the prefab with "this" as its parent
        _collectableInstance.transform.localPosition = Vector3.zero; // aligning child with its parent object’s position.
    }

    public Data Save()
    {
        return new Data
        {
            ID = this.ID,
            isCollected = this.isCollected
        };
    }

    public void Load(Data data)
    {
        isCollected = data.isCollected;
        if (isCollected)
            Destroy(this._collectableInstance);
        else
        {
            if (_collectableInstance == null)
                InstantiatePrefab();
        }
    }


}
