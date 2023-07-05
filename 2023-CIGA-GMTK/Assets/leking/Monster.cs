using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void Kill();
}
public class Monster : MonoBehaviour,IHealth
{
    public void MonsterAction()
    {
        
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
