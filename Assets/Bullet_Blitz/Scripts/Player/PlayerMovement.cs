using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DIRECTION
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    Transform aimTransform;
    [SerializeField]
    GameObject GunParent;

    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalCoupledVector;
    [HideInInspector]
    public float lastVerticalCoupledVector;
    [HideInInspector]
    public float lastHorizontalDeCoupledVector;
    [HideInInspector]
    public float lastVerticalDeCoupledVector;


    public Vector3 shootPosition;


    private PlayerAnimator pAnim;
    

    //private variables
    Rigidbody2D _rb;
    bool isPaused = false;
    //Singleton
    public static PlayerMovement Instance { get; private set; }
    //On Shooting event
    public delegate void Shooting(Vector3 shootPosition);
    public static event Shooting OnShoot;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<PlayerAnimator>();

    }
   
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
        //Debug.Log(currentgameState.ToString());
        switch (currentgameState)
        {
            case GAME_STATE.STARTED:
                break;
            case GAME_STATE.PAUSED:
                {
                    isPaused = true;
                    break;
                }
            case GAME_STATE.RUNNING:
                {
                    isPaused = false;
                    break;
                }

        }

    }

    
  
    public void Move(float inputX,float inputY)
    {
        float speed = new Vector2(inputX, inputY).sqrMagnitude;
        moveDir = new Vector2(inputX, inputY).normalized;
       // _rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        if (isPaused)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        _rb.MovePosition(_rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        if (moveDir.x != 0|| moveDir.y != 0)
        {
            lastHorizontalDeCoupledVector = moveDir.x;
            lastVerticalDeCoupledVector = moveDir.y;
        }
       else if (shootPosition.x != 0 || shootPosition.y != 0)
        {
            shootPosition = shootPosition.normalized;
            lastHorizontalDeCoupledVector = shootPosition.x;
            lastVerticalDeCoupledVector = shootPosition.y;
        }
        if (moveDir.x!=0)
        {
            lastHorizontalCoupledVector = moveDir.x;
        }
        if(moveDir.y!=0)
        {
            lastVerticalCoupledVector = moveDir.y;
        }
        pAnim.CheckDirection(speed,inputX,inputY);
    }
    public void Aim(Vector2 mousePosition)
    {
        if (isPaused)
            return;

        Vector2 aimDirection = (mousePosition - new Vector2(aimTransform.position.x,aimTransform.position.y)).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        shootPosition = new(mousePosition.x,mousePosition.y,0);
    }
    public void Shoot()
    {
        if (isPaused )
            return;

        //BulletRaycast.Shoot(gunEndPoint.position, shootPosition,50);
     
        OnShoot?.Invoke(shootPosition);
    }
}
