using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
  
    [SerializeField]ObjectType dropItem;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    bool isQuitting = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    public void CheckOnDrop()
    {
        if (isQuitting) return;
        if (Random.value < chance)
        {
           
            switch (dropItem)
            {
                case ObjectType.HEALTH:
                    {
                        ObjectSpawner.Instance.Spawn(ObjectType.HEALTH,transform.position);
                       
                        break; 
                    }
                case ObjectType.EXP:
                    {
                        ObjectSpawner.Instance.Spawn(ObjectType.EXP, transform.position);
                        break;
                    }
            }
           
        }
    }

}
