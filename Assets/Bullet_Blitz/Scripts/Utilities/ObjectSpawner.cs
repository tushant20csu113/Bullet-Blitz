using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private const string O = "Orb";
    private const string H = "Health";
    public static ObjectSpawner Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
   
    public void Spawn(ObjectType objType,Vector3 spawnPosition)
    {
        switch (objType)
        {
            case ObjectType.HEALTH:
                {
                    //var health=MyPooler.ObjectPooler.Instance.GetFromPool(H, spawnPosition, Quaternion.identity);
                    GameObject health=ObjectPooler.Generate(H, spawnPosition, Quaternion.identity);
                    //if (health == null) Debug.Log("Health Null");
                    health.name = H;
                    break;
                }
            case ObjectType.EXP:
                {
                    //var expOrb= MyPooler.ObjectPooler.Instance.GetFromPool(O, spawnPosition, Quaternion.identity);
                    GameObject expOrb = ObjectPooler.Generate(O, spawnPosition, Quaternion.identity);
                    // if (expOrb == null) Debug.Log("Orb Null");
                    expOrb.name = O;
                    break;
                }
        }
    }
}