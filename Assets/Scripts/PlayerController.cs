using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control movement speed
    [SerializeField] private bool isMoving = false;
    [SerializeField] private LayerMask bounds;
    [SerializeField] private Vector2 directionVector;
    [SerializeField] private List<Vector2> disallowedVectors;

    [SerializeField] private bool isTouchingWall;

    void Update()
    {
        if (!isMoving) // Only allow movement if not already moving
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 && verticalInput == 0)
            {
                directionVector = new Vector2(horizontalInput, 0);
                MovePlayer(directionVector);           
            }
            else if (verticalInput != 0 && horizontalInput == 0)
            {
                directionVector = new Vector2(0, verticalInput);
                MovePlayer(directionVector);             
            }
        }
    }

    private void FixedUpdate()
    {
        DetectWall();
    }

    //Boran 3/3/2024
    //Added conditions.
    void MovePlayer(Vector2 direction)
    {
        isMoving = false;
        Vector2 targetPosition = Vector2.zero;

        if(!disallowedVectors.Contains(direction))
        {
            isMoving = true;
            targetPosition = (Vector2)transform.position + direction;
        }

        if(isMoving)
            StartCoroutine(MoveCoroutine(targetPosition));
    }

    //Boran. 3/3/2024
    //I pray for this to work.
    //Edit: It worked. Yes.
    private void DetectWall()
    {
        float dist = .67f;
        Vector2[] directions = {Vector2.down, Vector2.up, Vector2.left, Vector2.right};

        foreach (Vector2 direction in directions)
        {
            isTouchingWall = Physics2D.Raycast(transform.position, direction, dist, bounds);
           
            if(!disallowedVectors.Contains(direction) && isTouchingWall)
                disallowedVectors.Add(direction);

            else if(!isTouchingWall && disallowedVectors.Contains(direction))
                disallowedVectors.Remove(direction);
        }    
    }

    System.Collections.IEnumerator MoveCoroutine(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }
}
