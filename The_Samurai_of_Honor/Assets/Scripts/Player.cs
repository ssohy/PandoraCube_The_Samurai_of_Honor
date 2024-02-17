using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public int hp;
    GameObject gameManagerObject;
    public Transform[] spawnMidwayEndPoint;
    // (이동 변수)
    public VariableJoystick joy;
    float h, v;
    Vector3 moveV;
    public float moveSpeed;
    public float rotateSpeed = 10.0f;
    private float gravity; //중력
    private CharacterController controller; //캐릭터 컨트롤러

    // (공격 변수)
    float attackDelay;
    public Sword sword;
    int tmp = 0;
    public GameObject delayImage;
    public TMP_Text countdownText; // 카운트다운을 표시할 Text UI
    private bool isCountingDown = false; // 더블 공격 카운트다운 중 여부
    private float countdownTime = 10f; // 카운트다운 시간

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
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;
            UpdateCountdownText();

            if (countdownTime <= 0f)
            {
                delayImage.SetActive(false);
                countdownText.gameObject.SetActive(false);
                isCountingDown = false;
            }
        }
    }
    Vector3 lastMoveDirection = Vector3.forward;
    //#.이동
    void Move()
    {
        h = joy.Horizontal;
        v = joy.Vertical;
        moveV = new Vector3(h, 0, v).normalized;

        if (controller.isGrounded)
        {
            moveV.y = -gravity;
        }
        else
        {
            moveV.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveV * moveSpeed * Time.deltaTime);

        if (controller.velocity.magnitude > 0)
        {
            anim.SetBool("isWalk", true);
            if (moveV != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
            }
        }
        else
        {
            transform.LookAt(transform.position + transform.forward);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isWalk", false);
        }

        /* 키보드
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

    //#.공격
    public void Attack()
    {
        attackDelay += Time.deltaTime;
        if (attackDelay >= sword.basicRate)
        {
            Debug.Log("기본 공격");
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
            Debug.Log("기본쿨타임 : " + attackDelay);
        }
    }
    public void DoubleAttack()
    {
        Debug.Log("더블 공격");
        sword.StartDoubleSwing();
        anim.SetTrigger("doDoubleAttack");

        // 카운트다운 시작
        isCountingDown = true;
        countdownTime = 10f;
        UpdateCountdownText();
        delayImage.SetActive(true);
        countdownText.gameObject.SetActive(true);
    }
    private void UpdateCountdownText()
    {
        int seconds = Mathf.CeilToInt(countdownTime);
        countdownText.text = seconds.ToString();
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
            {
                anim.SetTrigger("doDie");

                Invoke("LoadStartScene", 5f);
            }
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
            {
                //gameManager.MaskClear();
                //currentDay = 1;
                //isDay = true;
                //dataManager.SetCurrentDay(currentDay);
                //dataManager.SetIsDay(isDay);
                //dataManager.SetButterfly(0);
                //dataManager.SetEnemyCount(0);
                //gameManager.EndGame();
                //SceneManager.LoadScene("Strat");
                //Debug.Log("데이터 초기화");
            }
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
