using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject player;
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public int doubleDamage;
    public float rate;
    public BoxCollider meleeArea; // 충돌영역
    //public TrailRenderer trailEffect; 

    void Awake()
    {
        damage = 20;
        doubleDamage = 30;
    }
    void Update()
    {
        
    }

}
