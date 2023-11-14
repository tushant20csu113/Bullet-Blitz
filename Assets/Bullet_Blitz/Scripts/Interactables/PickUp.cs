using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour,IPickUpObjects
{
    [SerializeField] ObjectType objType;
    [SerializeField] int amount;
    public delegate void Pickup(ObjectType objType,int amount);
    public static event Pickup OnPickupExp;
    public void OnPickUp(PlayerStats player, ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.HEALTH:
                {
                    OnPickupExp?.Invoke(ObjectType.HEALTH, amount);
                    break;
                }
            case ObjectType.EXP:
                {
                    OnPickupExp?.Invoke(ObjectType.EXP,amount);
                    break;
                }
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        { 
   
            PlayerStats player = collider.transform.GetComponent<PlayerStats>();
            if (player != null)
            {
                OnPickUp(player, objType);
                ObjectPooler.Destroy(this.gameObject);
               // MyPooler.ObjectPooler.Instance.ReturnToPool(this.gameObject.name, this.gameObject);
            }
    }
    }
   

}
