using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    Basic,
    Fast,
    Tracking,
    Hallucination,
    Rat,
    Landmine,
    Mg,
    Xbow,
    Boss,
    Shotgun,
    Spiral,
    Flamethrower,
    Pyromaniac
}

public enum EnemyMovement {
    Chase,
    Avoid,
    Maintain,
    Stationary,
    Jumpy,
    Random
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float chaseSpeed;

    [SerializeField] private int health;

    [SerializeField] private Color basicEnemyColor;

    [SerializeField] private EnemyType enemyType = EnemyType.Basic;

    [SerializeField] private EnemyMovement enemyMovement = EnemyMovement.Chase;

    [SerializeField] private Color[] enemyColor = new Color[4];
    [SerializeField] private Sprite[] enemySpriteList = new Sprite[4];

    [SerializeField] private List<Sprite> listName = new() {};
    [SerializeField] private int Offset = 10;

    private Coroutine type;



    private delegate IEnumerator AttackPatterns();

    private Dictionary<EnemyType, AttackPatterns> enemyAttackPatterns = new();

    private Dictionary<EnemyType, Color> enemyColors = new();

    private Dictionary<EnemyType, EnemyMovement> enemyMovementPatterns = new();

    private Dictionary<EnemyType, float> enemySpeeds = new() {
        {EnemyType.Basic, 3},
        {EnemyType.Fast, 3},
        {EnemyType.Tracking, 3},
        {EnemyType.Hallucination, 3},
        {EnemyType.Rat, 5},
        {EnemyType.Landmine, 1},
        {EnemyType.Mg, 2},
        {EnemyType.Xbow, 1},
        {EnemyType.Boss, 3},
        {EnemyType.Shotgun, 3},
        {EnemyType.Spiral, 3},
        {EnemyType.Flamethrower, 3},
        {EnemyType.Pyromaniac, 3}
    };
    
    private Dictionary<EnemyType, Sprite> enemySprite = new();

    private Dictionary<EnemyType, int> enemyHealth = new();


    private IEnumerator BasicAttackPattern () {
        while(true)
        {
            yield return new WaitForSeconds(1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 1f,
                damage: 20,
                angle: theta,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
        
        }
    }

    private IEnumerator FastAttackPattern () {
        while(true)
        {
            yield return new WaitForSeconds(1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 15f,
                damage: 20,
                angle: theta,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
        }
    
    }   

    private IEnumerator TrackingAttackPattern () {

        while(true)
        {
            yield return new WaitForSeconds(1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position + CalculatePlayerPos(Offset);
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);
            FireProjectile(
                speed: 10f,
                damage: 0,
                angle: theta,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
        }
    
    }   

    private IEnumerator HallucinationAttackPattern () {

        while(true)
        {
            yield return new WaitForSeconds(1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 5f,
                damage: 0,
                angle: theta,
                scale: new Vector3(3,3,3),
                sprite: enemySprite[enemyType]
            );
        }
    
    }   

    private IEnumerator RatAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 5f,
                damage: 1,
                angle: theta,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
        }
    }   

    private IEnumerator LandmineAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(0.00001f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 0f,
                damage: 9999999,
                angle: theta,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
        }
    }   

    private IEnumerator MgAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(0.00001f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 10f,
                damage: 9999999,
                angle: theta,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
        }
    }   

    private IEnumerator XbowAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(2f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            FireProjectile(
                speed: 3f,
                damage: 9999999,
                angle: theta,
                scale: new Vector3(3,3,3),
                sprite: enemySprite[enemyType]
            );
        }
    }  

    private IEnumerator BossAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(1.5f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            
            for (int i = 0; i < 500; i++) {
                theta = theta - 30;
            FireProjectile(
                speed: 3f,
                damage: 9,
                angle: theta - i * 300,
                scale: new Vector3(1,1,1),
                sprite: enemySprite[enemyType]
            );
            };
        }
    }    

    private IEnumerator ShotgunAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(0.45f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            
            for (int i = -2; i < 5; i++) {
                FireProjectile(
                    speed: 6f,
                    damage: 9,
                    angle: theta + i * 0.15f,
                    scale: new Vector3(1,1,1),
                    sprite: enemySprite[enemyType]
                );
            };
        }
    }   

    private IEnumerator SpiralAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(0.2f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            yield return new WaitForSeconds(0.0001f);
            for (int i = -2; i < 500; i++) {
               yield return new WaitForSeconds(0.001f + 0.001f); 
                FireProjectile(
                    speed: 6f,
                    damage: 9,
                    angle: theta + i * 0.15f,
                    scale: new Vector3(1,1,1),
                    sprite: enemySprite[enemyType]
                );
            };
        }
    } 

    private IEnumerator FlamethrowerAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(0.1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            
            for (int i = -2; i < 3; i++) {
                FireProjectile(
                    speed: 8f,
                    damage: 9,
                    angle: theta + i * 0.075f,
                    scale: new Vector3(1,1,1),
                    sprite: enemySprite[enemyType]
                );
            };
        }
    }   

    private IEnumerator PyromaniacAttackPattern () {

        while(true) {
            yield return new WaitForSeconds(0.1f);  
            Vector2 bulletPosVector = player.transform.position - transform.position;
            float theta = Mathf.Atan2(bulletPosVector.y, bulletPosVector.x);  
            yield return new WaitForSeconds(0.00001f);

            for (int i = -2; i < 500; i++) {
                yield return new WaitForSeconds(0.0000001f + 0.00000001f); 
                    FireProjectile(
                        speed: 12f,
                        damage: 90,
                        angle: theta + i * 0.075f,
                        scale: new Vector3(1.25f,1.25f,1.25f),
                        sprite: enemySprite[enemyType]
                );
            };

            for (int i = -2; i < 3; i++) {
                yield return new WaitForSeconds(0.0000001f); 
                    FireProjectile(
                        speed: 8f,
                        damage: 9,
                        angle: theta + i * 0.075f,
                        scale: new Vector3(1,1,1),
                        sprite: enemySprite[enemyType]
                );
            };
        }
    }   


    private void ChasePlayer() {
        rb.velocity = transform.right * chaseSpeed;
    }

    private void AvoidPlayer() {
        rb.velocity = transform.right * -chaseSpeed;
    }

    private void MaintainPlayer() {
        float distance = DistFormula(this.transform.position, player.transform.position);         
        if (distance < 3) {
            AvoidPlayer();
        } else if (distance > 3) {
            ChasePlayer();
        } else {
            rb.velocity = transform.right * 0;
        }

    }

    private void StationaryPlayer() {
        rb.velocity = transform.right * 0;

    }

    private void JumpyPlayer () {
        if (Time.time % 3 < 1) {
            rb.velocity = transform.right * 6;
        } else {
            rb.velocity = transform.right * 2;
        }
    }

    private void RandomPlayer () {
        if (Time.time % 3 < 1) {
            int randomSpeed = Random.Range(0, 12);
            rb.velocity = transform.right * randomSpeed;
            float randomRotation = Random.Range(30, 361);
            transform.rotation = Quaternion.Euler(
                new Vector3(0f, 0f, randomRotation)
            );
        }
    }

    private float DistFormula(Vector3 pos1, Vector3 pos2) {
      return Mathf.Sqrt((pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y));
    }


    
    private void FlashbangBullet () { 
        // this throws a flashbang lol 

        // Instantiate 

    }

    private Vector3 CalculatePlayerPos(int steps) {
        float playerH = player.horizontal;
        float playerV = player.vertical;
        float playerSpeed = player.movementMultiplier;

        Vector3 calculate = new Vector3(playerH*playerSpeed*steps, playerV*playerSpeed*steps, 0);

        return calculate;
    }

    private void FireProjectile (float speed, int damage, float angle, Vector3 scale, Sprite sprite) {

        GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet currentBullet = go.GetComponent<Bullet>();
        currentBullet.bulletSpeed = speed;
        currentBullet.bulletDamage = damage;
        go.transform.localScale = scale;
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg));

    }

    // private IEnumerator BossPhase () {
    //     if (health > 15000) {
    //         StartCoroutine((BossAttackPattern()));
            
    //     }
    //     else {
    //         StartCoroutine((SpiralAttackPattern()));
    //     }
    //     yield break;
    // }

    

            // Start is called before the first frame update
    void Start()
    {
        enemyColors = new() {
            {EnemyType.Basic, enemyColor[0]},
            {EnemyType.Fast, enemyColor[1]},
            {EnemyType.Tracking, enemyColor[2]},
            {EnemyType.Hallucination, enemyColor[3]},
            {EnemyType.Rat, enemyColor[4]},
            {EnemyType.Landmine, enemyColor[5]},
            {EnemyType.Mg, enemyColor[6]},
            {EnemyType.Xbow, enemyColor[7]},
            {EnemyType.Boss, enemyColor[8]},
            {EnemyType.Shotgun, enemyColor[9]},
            {EnemyType.Spiral, enemyColor[10]},
            {EnemyType.Flamethrower, enemyColor[11]},
            {EnemyType.Pyromaniac, enemyColor[12]}
        };

        enemyAttackPatterns = new() {
            {EnemyType.Basic, BasicAttackPattern},
            {EnemyType.Fast, FastAttackPattern},
            {EnemyType.Tracking, TrackingAttackPattern},
            {EnemyType.Hallucination, HallucinationAttackPattern},
            {EnemyType.Rat, RatAttackPattern},
            {EnemyType.Landmine, LandmineAttackPattern},
            {EnemyType.Mg, MgAttackPattern},
            {EnemyType.Xbow, XbowAttackPattern},
            {EnemyType.Boss, BossAttackPattern},
            {EnemyType.Shotgun, ShotgunAttackPattern},
            {EnemyType.Spiral, SpiralAttackPattern},
            {EnemyType.Flamethrower, FlamethrowerAttackPattern},
            {EnemyType.Pyromaniac, PyromaniacAttackPattern}

        };

        enemyMovementPatterns = new() {
            {EnemyType.Basic, EnemyMovement.Chase},
            {EnemyType.Landmine, EnemyMovement.Stationary}
        };

        enemySprite = new()
        {
            {EnemyType.Basic, enemySpriteList[0]},
            {EnemyType.Fast, enemySpriteList[1]},
            {EnemyType.Tracking, enemySpriteList[2]},
            {EnemyType.Hallucination, enemySpriteList[3]},
            {EnemyType.Rat, enemySpriteList[4]},
            {EnemyType.Landmine, enemySpriteList[5]},
            {EnemyType.Mg, enemySpriteList[6]},
            {EnemyType.Xbow, enemySpriteList[7]},
            {EnemyType.Boss, enemySpriteList[8]},
            {EnemyType.Shotgun, enemySpriteList[9]},
            {EnemyType.Spiral, enemySpriteList[10]},
            {EnemyType.Flamethrower, enemySpriteList[11]},
            {EnemyType.Pyromaniac, enemySpriteList[12]}
        };

        enemyHealth = new()
        {
            {EnemyType.Basic, 3000},
            {EnemyType.Fast, 3000},
            {EnemyType.Tracking, 3000},
            {EnemyType.Hallucination, 3000},
            {EnemyType.Rat, 3000},
            {EnemyType.Landmine, 3000},
            {EnemyType.Mg, 3000},
            {EnemyType.Xbow, 3000},
            {EnemyType.Boss, 16000},
            {EnemyType.Shotgun, 3000},
            {EnemyType.Spiral, 3000},
            {EnemyType.Flamethrower, 3000},
            {EnemyType.Pyromaniac, 20000}
        };
        



        GetComponent<SpriteRenderer>().color = enemyColors[enemyType];
        chaseSpeed = enemySpeeds[enemyType];
        health = enemyHealth[enemyType];


        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<Player>().gameObject.GetComponent<Player>();
        
        type = StartCoroutine(enemyAttackPatterns[enemyType]());

    
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 lookDirection = player.transform.position - transform.position;

        float theta = Mathf.Atan2(lookDirection.y, lookDirection.x);
        transform.rotation = Quaternion.Euler(
            new Vector3(0f, 0f, theta * Mathf.Rad2Deg)
        );

        // rb.velocity = transform.right * chaseSpeed;
        RandomPlayer();


    }

    public void ChangeHPBy(int amount) {
        health = health - amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if(enemyType == EnemyType.Boss) {
            if(health < 15000 && health + amount >= 15000) {
                StopCoroutine(type);
                StartCoroutine(SpiralAttackPattern());
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject g = collision.gameObject;
        if(g.GetComponent<IDamageable>() != null && !g.name.Contains("enemy")){
            g.GetComponent<IDamageable>().ChangeHPBy(10);
        }
    }   
        
}