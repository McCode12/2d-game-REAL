using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
   void ChangeHPBy(int amount);
}

public class Bullet : MonoBehaviour,IDamageable
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public int bulletDamage = 50;
    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject g = collision.gameObject;
        if(g.GetComponent<IDamageable>() != null){
            g.GetComponent<IDamageable>().ChangeHPBy(bulletDamage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeHPBy(int amount) {
    }
}