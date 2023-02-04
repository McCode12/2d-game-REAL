using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    Basic,
    Fast,
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int myInteger;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float chaseSpeed;

    [SerializeField] private int health;

    [SerializeField] private Color basicEnemyColor;

    [SerializeField] private EnemyType enemyType = EnemyType.Basic;

    [SerializeField] private Color[] enemyColor = new Color[2];

    [SerializeField] private List<Sprite> listName = new() {};
    
    private delegate IEnumerator AttackPatterns();

    private Dictionary<EnemyType, AttackPatterns> enemyAttackPatterns = new();

    private Dictionary<EnemyType, Color> enemyColors = new();

    private Dictionary<EnemyType, float> enemySpeeds = new() {
        {EnemyType.Basic, 1},
        {EnemyType.Fast, 1},
    };

    private IEnumerator BasicAttackPattern () {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            FireProjectile(1, 2);
        
        }
    }

    private IEnumerator FastAttackPattern () {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            FireProjectile(10, 1);
        }
    
    }   

    private IEnumerator TrackingAttackPattern () {
        while(true)
        {

            yield return new WaitForSeconds(1f);
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float angle = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);
            FireProjectile(10, 1);
        }
    
    }   

    private Vector3 CalculatePlayerPos(int steps) {
        float playerH = player.horizontal;
        float playerV = player.vertical;
        float playerSpeed = player.movementMultiplier;

        Vector3 calculate = new Vector3(playerH*playerSpeed*steps, playerV*playerSpeed*steps, 0);

        return calculate;
    }

    private void FireProjectile (float speed, float size) {

        GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet currentBullet = go.GetComponent<Bullet>();
        currentBullet.bulletSpeed = speed;

        Vector2 lookDirection = player.transform.position - transform.position;
        float theta = Mathf.Atan2(lookDirection.y, lookDirection.x);
        go.transform.rotation = Quaternion.Euler(
            new Vector3(0f, 0f, theta * Mathf.Rad2Deg)
        );
        Vector3 vectorScale = new Vector3(size, size , size );

        go.transform.localScale = vectorScale;


    } 


    // Start is called before the first frame update
    void Start()
    {
        enemyColors = new() {
            {EnemyType.Basic, enemyColor[0]},
            {EnemyType.Fast, enemyColor[1]},
        };

        enemyAttackPatterns = new() {
            {EnemyType.Basic, BasicAttackPattern},
            {EnemyType.Fast, FastAttackPattern}
        };

        GetComponent<SpriteRenderer>().color = enemyColors[enemyType];
        chaseSpeed = enemySpeeds[enemyType];

        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<Player>().gameObject.GetComponent<Player>();
        
        StartCoroutine(enemyAttackPatterns[enemyType]());
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

    public void ChangeHPBy(int amount) {
        health = health - amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
