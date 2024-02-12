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
    // (이동 변수)
    float h, v;
    Vector3 moveV;
    public float moveSpeed;
    public float rotateSpeed = 10.0f;
    private float gravity; //중력
    private CharacterController controller; //캐릭터 컨트롤러

    // (공격 변수)
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
        gravity = 10f; //중력값 설정
    }

    void Update()
    {
        Move();
        Attack();
        gameOver();
    }

    //#.이동(ui추가할 때 버튼으로 수정할 예정)
    void Move() // 현재는 편의성을 위해 WASD로 이동중
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveV = new Vector3(h, 0, v).normalized;

        moveV.y -= gravity * Time.deltaTime;

        if (moveV.magnitude > 0)
        {
            // 이동 애니메이션 처리
            anim.SetBool("isWalk", true);

            // CharacterController를 이용하여 이동
            controller.Move(moveV * moveSpeed * Time.deltaTime);

            // 플레이어가 이동하면서 회전
            if (moveV != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
            }
        }
        else
        {
            // 이동 애니메이션을 중지
            anim.SetBool("isWalk", false);
        }
        /*
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveV = new Vector3(h, 0, v).normalized;
        if (!(h == 0 && v == 0) || !isBorder)
        {
            transform.position += moveV * moveSpeed * Time.deltaTime;
            anim.SetBool("isWalk", true); // 이동 애니메이션 활성화
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveV), Time.deltaTime * rotateSpeed);
            
        }
        else
        {
            anim.SetBool("isWalk", false); // 이동 애니메이션 비활성화
        }*/
        /*
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
        //anim.SetBool("isWalk", moveV != transform.position);*/
        //if (moveV.magnitude > 0)
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

        if (Input.GetMouseButtonDown(1) && isAttackReady) // 더블 공격
        {
            //Debug.Log("더블 공격");
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
            Debug.Log("공격받는 중");
            if (!isDamage)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                Debug.Log("현재 enemy" + enemy);
                hp -= enemy.attackDamage;
                dataManager.SetPlayerHp(hp);
                Debug.Log("플레이어 체력 : " + hp);
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
                //데이터 전달
                dataManager.SetCurrentDay(currentDay);
                dataManager.SetIsDay(isDay);
                // 씬 전환
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
