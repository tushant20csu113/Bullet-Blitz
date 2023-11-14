using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public float stoppingDistance = 2f;
    public float cooldownTimer = 3f;
    public float speed = 5f;
    public float bounceForce = 10f;
    public int damage = 20;
    public float hp = 4f;

    public EnemyStats(EnemyStats stats)
    {
        this.stoppingDistance = stats.stoppingDistance;
        this.cooldownTimer = stats.cooldownTimer;
        this.speed = stats.speed;
        this.bounceForce = stats.bounceForce;
        this.damage = stats.damage;
        this.hp = stats.hp;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }
}
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour,IDamageable
{
   
    EnemyStats enemyStats;
    Rigidbody2D _rb;
    Animator _enemyAnim;
    private bool isPaused=false;
    private Transform targetDestination;
    private SpriteRenderer _spriteRen;
    float timer;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRen = GetComponent<SpriteRenderer>();
        _enemyAnim = GetComponent<Animator>();
        targetDestination = PlayerMovement.Instance.transform;
        _enemyAnim.SetBool("isAlive", true);
        timer = 0;
    }
    private void OnEnable()
    {
        GameManager.OnStateChange += OnStateChange;
        LevelTracker.OnStateChange += LevelState;
    }
    private void OnDisable()
    {
        GameManager.OnStateChange += OnStateChange;
        LevelTracker.OnStateChange += LevelState;
    }

    private void LevelState(LEVEL_STATE currentlevelState)
    {
        switch (currentlevelState)
        {
            case LEVEL_STATE.COMPLETED:
                break;
            case LEVEL_STATE.GAME_OVER:
                Die();
                break;
        }
    }

    private void OnStateChange(GAME_STATE currentgameState)
    {
        switch (currentgameState)
        {
            case GAME_STATE.STARTED:
                break;
            case GAME_STATE.PAUSED:
                {
                    isPaused = true;
                    Time.timeScale = 0;
                    break;
                }
            case GAME_STATE.RUNNING:
                {
                    isPaused = false;
                    Time.timeScale = 1;
                    break;
                }
            
            default:
                break;
        }
    }
    
    private void FixedUpdate()
    {
        if(isAlive==false)
        {
              return;
        }
        Vector2 direction = (targetDestination.position - transform.position).normalized;
        _rb.velocity = direction * enemyStats.speed;
     
        //_rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);

        float angle= Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        SpriteFlip();
        if (Vector2.Distance(transform.position, targetDestination.position) <=enemyStats.stoppingDistance)
        {
            _rb.simulated = false;
            _enemyAnim.SetBool("isMoving", false);
            Cooldown();
        }
        else
        {
            _rb.simulated = true;
            _enemyAnim.SetBool("isMoving", true);
            _enemyAnim.SetBool("isAttacking", false);
        }
        if (isPaused)
        {
            _rb.velocity = Vector2.zero;
            _rb.simulated = false;
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPaused)
            return;

        if (collision.CompareTag("Enemy"))
        {
            if (Random.Range(0, 100) > 50)
            {
                _rb.AddForce(transform.right * Random.Range(0, -enemyStats.bounceForce));
            }
            else
            {
                _rb.AddForce(transform.forward * Random.Range(0, -enemyStats.bounceForce));
            }
        }
    }
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (isPaused)
            return;

        if (collision.transform.CompareTag("Enemy"))
        {
            if (Random.Range(0, 100) > 50)
            {
                _rb.AddForce(transform.right * Random.Range(0, -enemyStats.bounceForce));
            }
            else
            {
                _rb.AddForce(transform.forward * Random.Range(0, -enemyStats.bounceForce));
            }
        }
    }*/
    //Animation Event called in attack animation 
    void AttackAE()
    {    
        PlayerStats.Instance.TakeDamage(enemyStats.damage);
    }
   void SpriteFlip()
    {
        
        if(_rb.velocity.x<0)
        {
            _spriteRen.flipY = true;
        }
        else
        {
            _spriteRen.flipY = false;
        }
    }
    public void TakeDamage(int damage)
    {
        if(damage>enemyStats.hp)
        {
            enemyStats.hp = 0;
        }
        else
        {
        enemyStats.hp -= damage;

        }
       //Debug.Log("Enemy Hp left: "+ enemyStats.hp);
        _rb.simulated = false;

        _enemyAnim.SetTrigger("isDamaged");
   
        if (enemyStats.hp<=0)
        {
            _enemyAnim.SetBool("isAlive", false);
            isAlive = false;
            //levelUpSystem.AddExperience(1500);
            //gameObject.SetActive(false);
            //EnemySpawnManager.Instance.OnEnemyKilled();
            //Destroy(gameObject);
        }
    }
    void Cooldown()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0f)
        {
            _enemyAnim.SetBool("isAttacking", true);

        } 
    }
    //Triggered at end of attack animation using animation event
     void ResetTimerAE()
     {
         _enemyAnim.SetBool("isAttacking", false);
         _enemyAnim.SetBool("isMoving", false);
          timer = enemyStats.cooldownTimer;
         
    }
    //Called in Die Animation using Animation Event
    void DeathAE()
    {
        Invoke("Die", 3); 
    }
    void Die()
    {
        //gameObject.SetActive(false);
        DropOnDestroy dod = transform.GetComponent<DropOnDestroy>();
        dod.CheckOnDrop();
        //  gameObject.SetActive(false);
        EnemySpawnManager.Instance.TOTAL_ENEMIES_KILLED = 1;
        ObjectPooler.Destroy(this.gameObject);
       // MyPooler.ObjectPooler.Instance.ReturnToPool(this.gameObject.name, this.gameObject);
    }

    public void SetStats(EnemyStats stats)
    {
        this.enemyStats = new EnemyStats(stats);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        enemyStats.ApplyProgress(progress);
    }
}
