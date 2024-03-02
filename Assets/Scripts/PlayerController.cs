using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control movement speed
    private bool isMoving = false;

    void Update()
    {
        if (!isMoving) // Only allow movement if not already moving
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 && verticalInput == 0)
            {
                MovePlayer(new Vector2(horizontalInput, 0));
            }
            else if (verticalInput != 0 && horizontalInput == 0)
            {
                MovePlayer(new Vector2(0, verticalInput));
            }
        }
    }

    void MovePlayer(Vector2 direction)
    {
        isMoving = true;

        Vector2 targetPosition = (Vector2)transform.position + direction;
        StartCoroutine(MoveCoroutine(targetPosition));
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
