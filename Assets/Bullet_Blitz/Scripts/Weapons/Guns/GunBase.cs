using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    MACHINE_GUN,
    PISTOL,
    SHOTGUN
}
public class GunBase : MonoBehaviour
{
    [SerializeField] GunType gunType;
    [Header("Shooting")]
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int bulletDamage;
    [SerializeField]
    private int shotsPerSecond;
    [SerializeField]
    private float reloadSpeed;
    [SerializeField]
    private float range=10f;
    [SerializeField]
    private float recoil;
    [SerializeField]
    private float recoilSmooth;
    [SerializeField]
    private bool tapable;
    [SerializeField]
    private float spread;
    [SerializeField] float shakeIntensity = 7f;
    [SerializeField] float shakeTimer = 0.2f;
    [SerializeField]
    private GameObject bulletPrefab;
    private bool _reloading=false;
    private bool _shooting=false;
    private int ammo;
    private string shootAudio;
    private SpriteRenderer sRen;
 
    PlayerMovement playerMove;
    [SerializeField]
    private GameObject gunEndPoint;
    private bool isPaused;
  

    private void OnEnable()
    {
        GameManager.OnStateChange += OnStateChange;
    }
    private void OnDisable()
    {
        GameManager.OnStateChange -= OnStateChange;
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
                }
                break;
            case GAME_STATE.RUNNING:
                {
                    isPaused = false;
                }
                break;
        }
    }
    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();
        playerMove = PlayerMovement.Instance;
        ammo = maxAmmo;
       // gunEndPoint = transform.GetComponentInChildren<Transform>().position;
        switch (gunType)
        {
            case GunType.MACHINE_GUN:
                {
                    shootAudio = "MachineGunShoot";
                }
                break;
            case GunType.PISTOL:
                {
                    shootAudio = "PistolShoot";
                }
                break;
            case GunType.SHOTGUN:
                {
                    shootAudio = "ShotgunShoot";
                }
                break;
        }
    }
    private void Update()
    {
        if (isPaused) return;
        SpriteFlip();
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, recoilSmooth * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R) && !_reloading && ammo < maxAmmo)
        {
            StartCoroutine(ReloadingCooldown());
        }


        if((tapable?Input.GetMouseButtonDown(0):Input.GetMouseButton(0))&& !_shooting&&!_reloading)
        {
            if(ammo <= 0)
            {
                StartCoroutine(ReloadingCooldown());
            }
            else
            {
                StartCoroutine(ShootingCooldown());
            }
               
        }
    }

    private void SpriteFlip()
    {
        if(playerMove.shootPosition.x<0)
        {
            sRen.flipY = true;
        }
        else
        {
            sRen.flipY = false;
        }
    }

    private IEnumerator ShootingCooldown()
    {
        Shoot();
        ammo -= shotsPerSecond;
        _shooting = true;
        //Debug.Log("Shooting Cooldown");
        yield return new WaitForSeconds(1f / (shotsPerSecond*2));
      //  Debug.Log("Shooting Cooldown over");
        _shooting = false;
    }
    private IEnumerator ReloadingCooldown()
    {
        _reloading = true;
       // Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadSpeed);
       // Debug.Log("Reloaded");
        ammo = maxAmmo;
        _reloading= false;
    }
    public  void Shoot()
    {
       
        for (int i = 0; i < shotsPerSecond; i++)
        {
            //gunEndPoint = transform.GetComponentInChildren<Transform>().position;
            //GameObject bullet = MyPooler.ObjectPooler.Instance.GetFromPool("Bullet", gunEndPoint, Quaternion.identity);
            GameObject bullet=ObjectPooler.Generate("Bullet", gunEndPoint.transform.position, Quaternion.identity);
            //GameObject bullet = Instantiate(bulletPrefab, gunEndPoint.transform.position, Quaternion.identity);
           // if (bullet == null) return;
            bullet.name = "Bullet";
            bullet.transform.position = gunEndPoint.transform.position;
            Vector2 direction = BulletDirection();
            Projectile bulletProjectile = bullet.GetComponent<Projectile>();
            bulletProjectile.range = range;
            bulletProjectile.speed = 8f;
            bulletProjectile.damage = bulletDamage;
            bulletProjectile.SetDirectionAndRotation(direction.x, direction.y);
            StartCoroutine(bulletProjectile.TimeToLive(range/8f));

        }
        transform.localPosition -= new Vector3(recoil, 0, 0);
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTimer);
        AudioManager.Instance.PlaySFX(shootAudio);

    }
    public Vector2 BulletDirection()
    {
        Vector2 shootPosition = playerMove.shootPosition;//Mouse Position
        Vector2 gunEndPos = new Vector2(gunEndPoint.transform.position.x, gunEndPoint.transform.position.y);
        Vector2 direction = (shootPosition - gunEndPos).normalized;//shoot direction
        direction += Vector2.Perpendicular(direction) * Random.Range(-spread, spread);
        return direction;
    }

    
}