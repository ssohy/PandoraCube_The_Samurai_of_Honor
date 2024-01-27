using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public int hp;

    // (이동 변수)
    float h, v;
    Vector3 moveV;
    public float moveSpeed; 

    // (공격 변수)
    bool AttackDown;
    float AttackDelay;
    bool isAttackReady;
    public Sword sword;
    // Animator anim;

    Rigidbody rigid;

    void Awake()
    {
        player = GameObject.Find("Player");
        hp = 100;
    }

    void Update()
    {
        Move();
        Attack();
    }

    //#.이동(ui추가할 때 버튼으로 수정할 예정)
    void Move() // 현재는 편의성을 위해 WASD로 이동중
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
    }

    //#.공격
    void Attack() // 현재는 편의성을 위해 좌클릭, 우클릭으로 공격
    {
        AttackDelay += Time.deltaTime;
        isAttackReady = sword.rate < AttackDelay;

        if (Input.GetMouseButtonDown(0) && isAttackReady) // 기본 공격
        {
            //Debug.Log("기본 공격");
            sword.StartSwing();
            //anim.SetTrigger("doSwing");
            AttackDelay = 0;
        }

        if (Input.GetMouseButtonDown(1) && isAttackReady) // 더블 공격
        {
            //Debug.Log("더블 공격");
            sword.StartDoubleSwing();
            //anim.SetTrigger("doSwing");
            AttackDelay = 0;
        }
    }
    /*
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeRotation();
    }*/
}
