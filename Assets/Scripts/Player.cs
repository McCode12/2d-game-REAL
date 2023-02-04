

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] public float horizontal;
    [SerializeField] public float vertical;
    [SerializeField] public float movementMultiplier = 0.1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float playerHP;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private GameObject bullet;

    
    public void ChangeHPBy(int amount) {
        playerHP = playerHP - amount;
    }

    private float GetAngleToCursor(Vector3 pos) {
        Vector2 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos;
        return Mathf.Atan2(lookDirection.y, lookDirection.x);
        
    }

    private void AttemptAttack(){
        if (canAttack == true) {
            // attack!!
            GameObject fired = Instantiate(bullet, transform.position, Quaternion.identity);
            float angle = GetAngleToCursor(transform.position); 
            fired.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg));
            canAttack = false;
            StartCoroutine(BecomeTrueAgain());
            fired.GetComponent<Bullet>().bulletDamage = 50;
            fired.GetComponent<Bullet>().bulletSpeed = 6f;
        }
    }


    private IEnumerator BecomeTrueAgain() {
    // write your code here
        yield return new WaitForSeconds(0.000000000000000000000001f);
        canAttack = true;
    }   

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag != "Wall") {
            playerHP = playerHP - 10;
            healthText.text = $"Player HP: {playerHP}";
        }
        
    }
    void FixedUpdate(){
        if (horizontal != 0 && vertical != 0) {
            horizontal = horizontal/1.414f;
            vertical = vertical/1.414f;
        }
        transform.position = new Vector2(transform.position.x + horizontal * movementMultiplier, transform.position.y + vertical * movementMultiplier);
 
    }

    void Start()
    {

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(0,0);
        bool firebullet = Input.GetMouseButton(0);
        if (firebullet == true) {
            AttemptAttack();
        }
    }
}