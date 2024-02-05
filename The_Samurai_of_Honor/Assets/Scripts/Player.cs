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

    // (공격 변수)
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

    //#.이동(ui추가할 때 버튼으로 수정할 예정)
    void Move() // 현재는 편의성을 위해 WASD로 이동중
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
        //anim.SetBool("isWalk", moveV != transform.position);
        if (moveV.magnitude > 0)
        {
            anim.SetBool("isWalk", true); // 이동 애니메이션 활성화
        }
        else
        {
            anim.SetBool("isWalk", false); // 이동 애니메이션 비활성화
        }
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
            anim.SetTrigger("doAttack");

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
            if (!isDamage)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                hp -= enemy.attackDamage;
                StartCoroutine(OnDamage());
            }
            Debug.Log("플레이어 체력 : " + hp);
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
                // 씬 전환
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
