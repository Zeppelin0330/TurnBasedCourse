using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private float stoppingDistance = 0.1f;
    private Vector3 targetPosition;

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetMouseWorldPosition());
        }

        if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance) return;

        Vector3 moveDir = (targetPosition - transform.position).normalized;
        transform.position += moveDir * movementSpeed * Time.deltaTime;    
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

}
