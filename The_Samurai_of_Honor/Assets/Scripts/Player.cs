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
    public float rotateSpeed = 10.0f;
    private float gravity; //�߷�
    private CharacterController controller; //ĳ���� ��Ʈ�ѷ�

    // (���� ����)
    bool AttackDown;
    float AttackDelay;
    bool isAttackReady;
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
        Attack();
        gameOver();
    }

    //#.�̵�(ui�߰��� �� ��ư���� ������ ����)
    void Move() // ����� ���Ǽ��� ���� WASD�� �̵���
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveV = new Vector3(h, 0, v).normalized;

        moveV.y -= gravity * Time.deltaTime;

        if (moveV.magnitude > 0)
        {
            // �̵� �ִϸ��̼� ó��
            anim.SetBool("isWalk", true);

            // CharacterController�� �̿��Ͽ� �̵�
            controller.Move(moveV * moveSpeed * Time.deltaTime);

            // �÷��̾ �̵��ϸ鼭 ȸ��
            if (moveV != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
            }
        }
        else
        {
            // �̵� �ִϸ��̼��� ����
            anim.SetBool("isWalk", false);
        }
        /*
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveV = new Vector3(h, 0, v).normalized;
        if (!(h == 0 && v == 0) || !isBorder)
        {
            transform.position += moveV * moveSpeed * Time.deltaTime;
            anim.SetBool("isWalk", true); // �̵� �ִϸ��̼� Ȱ��ȭ
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveV), Time.deltaTime * rotateSpeed);
            
        }
        else
        {
            anim.SetBool("isWalk", false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
        }*/
        /*
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
        //anim.SetBool("isWalk", moveV != transform.position);*/
        //if (moveV.magnitude > 0)
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
            Debug.Log("���ݹ޴� ��");
            if (!isDamage)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                Debug.Log("���� enemy" + enemy);
                hp -= enemy.attackDamage;
                dataManager.SetPlayerHp(hp);
                Debug.Log("�÷��̾� ü�� : " + hp);
                StartCoroutine(OnDamage());
            }
            
        }

        if (other.tag == "Midway")
        {
            isDay = false;
            Debug.Log("isDay : " + isDay);
        }

        if (other.tag == "End")
        {
            if (currentDay == 3)
                gameOver();
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
