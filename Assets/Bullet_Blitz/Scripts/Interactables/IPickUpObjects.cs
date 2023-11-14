using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    HEALTH,
    EXP

}
public interface IPickUpObjects 
{
    public void OnPickUp(PlayerStats player,ObjectType objectType);
}
