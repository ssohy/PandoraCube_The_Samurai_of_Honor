using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public int hp;

    // (�̵� ����)
    float h, v;
    Vector3 moveV;
    public float moveSpeed; 

    // (���� ����)
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

    //#.�̵�(ui�߰��� �� ��ư���� ������ ����)
    void Move() // ����� ���Ǽ��� ���� WASD�� �̵���
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
    }

    //#.����
    void Attack() // ����� ���Ǽ��� ���� ��Ŭ��, ��Ŭ������ ����
    {
        AttackDelay += Time.deltaTime;
        isAttackReady = sword.rate < AttackDelay;

        if (Input.GetMouseButtonDown(0) && isAttackReady) // �⺻ ����
        {
            //Debug.Log("�⺻ ����");
            sword.StartSwing();
            //anim.SetTrigger("doSwing");
            AttackDelay = 0;
        }

        if (Input.GetMouseButtonDown(1) && isAttackReady) // ���� ����
        {
            //Debug.Log("���� ����");
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
