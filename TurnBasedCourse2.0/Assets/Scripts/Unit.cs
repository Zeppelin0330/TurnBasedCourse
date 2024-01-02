using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const string WALKING_ANIMATION_PARAM = "IsWalking";

    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;

    private float stoppingDistance = 0.1f;
    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake() 
    {
        targetPosition = transform.position;
    }

    private void Start() 
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update() 
    {
        bool isMoving = Vector3.Distance(transform.position, targetPosition) > stoppingDistance;

        unitAnimator.SetBool(WALKING_ANIMATION_PARAM, isMoving);

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.position += moveDir * movementSpeed * Time.deltaTime;  

            transform.forward = Vector3.Lerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);  
        }

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition != gridPosition)
        {
            // Unit Changed Grid Position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

}
