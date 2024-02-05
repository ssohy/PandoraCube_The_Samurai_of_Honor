using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public int hp;
    GameObject gameManagerObject;
    public Transform[] spawnMidwayEndPoint;
    // (�̵� ����)
    float h, v;
    Vector3 moveV;
    public float moveSpeed; 

    // (���� ����)
    bool AttackDown;
    float AttackDelay;
    bool isAttackReady;
    public Sword sword;
    

    Rigidbody rigid;

    bool isDamage;

    bool isDay;
    int currentDay;
    string nextScene;

    Animator anim;

    void Awake()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();


        player = GameObject.Find("Player");
        hp = 100;

        isDay = gameManager.isDay;
        currentDay = gameManager.currentDay;

        anim = GetComponent<Animator>();
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
        //anim.SetBool("isWalk", moveV != transform.position);
        if (moveV.magnitude > 0)
        {
            anim.SetBool("isWalk", true); // �̵� �ִϸ��̼� Ȱ��ȭ
        }
        else
        {
            anim.SetBool("isWalk", false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
        }
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
            anim.SetTrigger("doAttack");

        }

        if (Input.GetMouseButtonDown(1) && isAttackReady) // ���� ����
        {
            //Debug.Log("���� ����");
            sword.StartDoubleSwing();
            //anim.SetTrigger("doSwing");
            AttackDelay = 0;
            anim.SetTrigger("doDoubleAttack");
           
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

        if(other.tag == "Midway")
        {
            isDay = false;
            Debug.Log("isDay : " + isDay);
        }

        if(other.tag  == "End")
        {
            if (currentDay == 3)
                gameOver();
            else
            {
                currentDay++;
                isDay = true;
                // �� ��ȯ
                nextScene = "Day" + currentDay.ToString();
                SceneManager.LoadScene(nextScene);
                player.transform.position = spawnMidwayEndPoint[0].position;

            }
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
        if (hp <= 0)
        {
            #if UNITY_EDITOR
                 UnityEditor.EditorApplication.isPlaying = false;
            #else
                  Application.Quit();
            #endif
        }
    }
}
