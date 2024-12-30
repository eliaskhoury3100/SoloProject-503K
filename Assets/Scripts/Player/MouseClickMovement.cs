using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseClickMovement : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private LayerMask _layerMask; // limit raycast to ground layer

    public Transform PlayerTransform
    {
        get
        {
            if (_playerTransform == null)
            {
                _playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
            }
            return _playerTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // left button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _layerMask))
            {
                PlayerTransform.GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }

        }
    }
}
