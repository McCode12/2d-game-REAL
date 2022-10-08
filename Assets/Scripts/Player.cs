using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float movementMultiplier = 0.1f;

    [SerializeField] private float diagonalMovementMultiplier = 1.414f;
    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) {
            horizontal = horizontal / diagonalMovementMultiplier;
            vertical = vertical / diagonalMovementMultiplier;
        }
    }

    void FixedUpdate() {
        transform.position = new Vector2(
            transform.position.x + horizontal * movementMultiplier, 
            transform.position.y + vertical * movementMultiplier
        );
    }

}


