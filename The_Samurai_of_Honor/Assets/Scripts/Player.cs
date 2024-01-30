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

    bool isDamage;

    void Awake()
    {
        player = GameObject.Find("Player");
        hp = 1;
    }

    void Update()
    {
        Move();
        Attack();
        gameOver();
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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!isDamage)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                hp -= enemy.attackDamage;
                StartCoroutine(OnDamage());
            }
            Debug.Log("�÷��̾� ü�� : " + hp);
        }
    }
    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
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

    void gameOver()
    {
        if(hp <= 0)
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                            Application.Quit();
            #endif
        }
    }
}
