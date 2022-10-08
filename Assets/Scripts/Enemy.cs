using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int myInteger;

    [SerializeField] private float chaseSpeed = 492f;


    [SerializeField] private Color basicEnemyColor;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = basicEnemyColor;

        rb = GetComponent<Rigidbody2D>();
        



    }

    // Update is called once per frame
    void Update()
    {

        Vector2 lookDirection = player.transform.position - transform.position;

        float theta = Mathf.Atan2(lookDirection.y, lookDirection.x);
        transform.rotation = Quaternion.Euler(
            new Vector3(0f, 0f, theta * Mathf.Rad2Deg)
        );

        rb.velocity = transform.right * chaseSpeed;

    }
}
