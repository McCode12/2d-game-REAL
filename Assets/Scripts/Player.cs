using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float movementMultiplier = 0.1f;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float diagonalMovementMultiplier = 1.414f;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] public int playerHP = 100;

    [SerializeField] private bool canAttack;




    private IEnumerator gunCooldown() {
        yield return new WaitForSeconds(1f);
    }

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"Health: {playerHP}";
        rb.velocity = new Vector2(0, 0);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) {
            horizontal = horizontal / diagonalMovementMultiplier;
            vertical = vertical / diagonalMovementMultiplier;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision) {
        playerHP = playerHP - 10;
    }

    void FixedUpdate() {
        transform.position = new Vector2(
            transform.position.x + horizontal * movementMultiplier, 
            transform.position.y + vertical * movementMultiplier
        );
    }
    
}


