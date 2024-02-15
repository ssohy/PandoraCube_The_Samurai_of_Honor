using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public int hp;
    GameObject gameManagerObject;
    public Transform[] spawnMidwayEndPoint;
    // (�̵� ����)
    public VariableJoystick joy;
    float h, v;
    Vector3 moveV;
    public float moveSpeed;
    public float rotateSpeed = 10.0f;
    private float gravity; //�߷�
    private CharacterController controller; //ĳ���� ��Ʈ�ѷ�

    // (���� ����)
    float attackDelay;
    float doubleAttackDelay;
    public Sword sword;
    int tmp = 0;

    Rigidbody rigid;
    Animator anim;

    bool isDamage;
    bool isDay;
    int currentDay;
    string nextScene;

    bool isBorder;

    int enemyCnt;
    private DataManager dataManager;


    void Awake()
    {
        dataManager = DataManager.GetInstance();

        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        hp = dataManager.GetPlayerHp();
        isDay = dataManager.GetIsDay();
        currentDay = dataManager.GetCurrentDay();

        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        gravity = 10f; //�߷°� ����

    }

    void Update()
    {
        Move();
        //gameOver();
    }

    //#.�̵�
    void Move()
    {
        h = joy.Horizontal;
        v = joy.Vertical;
        moveV = new Vector3(h, 0, v).normalized;

        moveV.y -= gravity * Time.deltaTime;

        if (moveV.magnitude > 0)
        {
            anim.SetBool("isWalk", true);
            controller.Move(moveV * moveSpeed * Time.deltaTime);
            if (moveV != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

        /* Ű����
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveV = new Vector3(h, 0, v).normalized;

        moveV.y -= gravity * Time.deltaTime;

        if (moveV.magnitude > 0)
        {
            anim.SetBool("isWalk", true);
            controller.Move(moveV * moveSpeed * Time.deltaTime);
            if (moveV != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }*/

    }

    //#.����
    public void Attack()
    {
        attackDelay += Time.deltaTime;
        if (attackDelay >= sword.rate)
        {
            Debug.Log("�⺻ ����");
            sword.StartSwing();
            attackDelay = 0;
            if (tmp == 0)
            {
                anim.SetTrigger("doAttack");
                tmp = 1;
            }
            else if (tmp == 1)
            {
                anim.SetTrigger("doSlash");
                tmp = 0;
            }
        }
        else
        {
            Debug.Log("�⺻��Ÿ��");
        }
    }
    public void DoubleAttack()
    {
        doubleAttackDelay += Time.deltaTime;
        if (doubleAttackDelay >= sword.rate)
        {
            Debug.Log("���� ����");
            sword.StartDoubleSwing();
            doubleAttackDelay = 0;
            anim.SetTrigger("doDoubleAttack");
        }
        else
        {
            Debug.Log("������Ÿ��");
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
                dataManager.SetPlayerHp(hp);
                gameManager.UpdateHPBar(hp);
                StartCoroutine(OnDamage());
            }
            if (dataManager.GetPlayerHp() <= 0)
                gameManager.gameOver();
            
        }

            if (other.tag == "Midway")
        {
            isDay = false;
            dataManager.SetIsDay(isDay);
            Debug.Log("isDay : " + isDay);
        }

        if (other.tag == "End")
        {
            if (currentDay == 3)
                gameManager.gameOver();
            else
            {
                currentDay++;
                isDay = true;
                //������ ����
                dataManager.SetCurrentDay(currentDay);
                dataManager.SetIsDay(isDay);
                // �� ��ȯ
                nextScene = "Day" + currentDay.ToString();
                SceneManager.LoadScene(nextScene);
                player.transform.position = spawnMidwayEndPoint[0].position;
                if (currentDay <= 3)
                    gameManager.ReadSpawnFile();
            }
        }
    }

    
    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void StopToWall()
    {
        //Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }
    void FixedUpdate()
    {
        //FreezeRotation();
        StopToWall();
    }



}
