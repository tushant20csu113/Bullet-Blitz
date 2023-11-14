using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    Animator _gunAnim;
    public Animator _playerAnim; 
    public static PlayerAnimator Instance { get;private set; }
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.OnStateChange += OnStateChange;
        PlayerMovement.OnShoot += OnShootPressed;
    }

    private void OnDisable()
    {
        GameManager.OnStateChange -= OnStateChange;
        PlayerMovement.OnShoot -= OnShootPressed;
    }

   public void CheckDirection(float speed, float inputX, float inputY)
    {
        if(speed >= 0.01)
        {
            _playerAnim.SetFloat("Velocity", speed);
            _playerAnim.SetFloat("Horizontal", inputX);
            _playerAnim.SetFloat("Vertical", inputY);
        }
        else
        {
            _playerAnim.SetFloat("Velocity", speed);
            _playerAnim.SetFloat("Horizontal", PlayerMovement.Instance.lastHorizontalDeCoupledVector);
            _playerAnim.SetFloat("Vertical", PlayerMovement.Instance.lastVerticalDeCoupledVector);
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
                  
                    break;
                }
            case GAME_STATE.RUNNING:
                {
                 
                    break;
                }
          
        }

    }
    private void OnShootPressed(Vector3 shootPosition)
    {
        _gunAnim.SetTrigger("isAttacking");
    }


    
}
