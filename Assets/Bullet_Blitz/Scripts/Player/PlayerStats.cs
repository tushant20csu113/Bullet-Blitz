using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 100;
    private int currentHP;
    private float hpRegenerationRate =0f;
    private float hpRegenerationTimer=5f;
    public int MAX_HP
    {
        get
        {
           return maxHP;
        }
        set
        {
            maxHP = value;
            OnHPchange?.Invoke(currentHP, maxHP);
        }
    }
    public int CURRENT_HP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
            OnHPchange?.Invoke(currentHP, maxHP);
        }
    }
   public float HP_REGENERATION_RATE
    {
        get
        {
            return hpRegenerationRate;
        }
        set
        {
            hpRegenerationRate = value;
        }
    }
    public static PlayerStats Instance { get; private set; }

    public delegate void HPchange(int currentHP, int maxHP);
    public static event HPchange OnHPchange;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    private void Start()
    {
        currentHP = maxHP;
        StartCoroutine(HP_RECOVER());
        OnHPchange?.Invoke(currentHP, maxHP);
    }
   
    private void OnEnable()
    {
        PickUp.OnPickupExp += OnHealthPickup;
    }
    private void OnDisable()
    {
        PickUp.OnPickupExp -= OnHealthPickup;
    }

    private void OnHealthPickup(ObjectType objType, int amount)
    {
        if (objType == ObjectType.HEALTH)
        {
            Heal(amount);
        }
    }

   
    public void Heal(int healAmount)
    {
        int tempHP = currentHP;
        tempHP += healAmount;
        if (tempHP >= maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += healAmount;
        }
        CURRENT_HP = currentHP;

    }
    IEnumerator HP_RECOVER()
    {
        yield return new WaitForSeconds(hpRegenerationTimer);
        Heal((int)(HP_REGENERATION_RATE*MAX_HP*0.05f));
     
        StartCoroutine(HP_RECOVER());
    }
    
    private void CheckAlive()
    {
        if(currentHP<=0)
        {
            currentHP = 0;
            //Player Died
            LevelTracker.OnStateChange?.Invoke(LEVEL_STATE.GAME_OVER);
            
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {        
        currentHP -= damage;
        CURRENT_HP = currentHP;
        MessageSystem.Instance.PostMessage(damage.ToString(), transform.position);
        CheckAlive();
    }
}
