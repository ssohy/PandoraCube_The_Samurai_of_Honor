using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;

    public int enemyMaxHp;
    public int enemyCurHp;
    public string enemyName; // Samurai, Bowel, Skull
    public int attackDamage;
    public float speed;

    public bool isDay;
    public int currentDay;

    Rigidbody rigid;
    BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;
    NavMeshAgent nav;

    public Transform target;
    bool isChase;

    int enemyCnt;
    Animator anim;

    private DataManager dataManager;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        
        GameObject objectManagerObject = GameObject.Find("ObjectManager");
        objectManager = objectManagerObject.GetComponent<ObjectManager>();
        player = GameObject.Find("Player");

        target = player.GetComponent<Transform>();

        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        //enemyCnt = gameManager.enemyCnt;
        enemyCnt = dataManager.GetEnemyCount();
        anim = GetComponent<Animator>();
    }

    /*void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }*/
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 50f)
        {
            Walk();
        }
        if (distance < 30f)
        {
            Attack();
        }
    }
    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee")
        {
            Sword sword = other.GetComponent<Sword>();
            enemyCurHp -= sword.damage;
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        if (enemyCurHp <= 0)
        {
            anim.SetTrigger("doDie");
            gameObject.SetActive(false);
            dataManager.SetEnemyCount(++enemyCnt);
        }
        yield break;
    }
    void Walk()
    {
        nav.SetDestination(target.position);
        anim.SetTrigger("doWalk");
    }
    void Attack()
    {
        anim.SetTrigger("doAttack");
    }
    public void OnEnable()
    {
        int currentDay = dataManager.GetCurrentDay();
        bool isDay = dataManager.GetIsDay();
        switch (enemyName)
        {
            case "Samurai":
                if (isDay)
                {
                    enemyMaxHp = currentDay == 3 ? 70 : 40;
                    attackDamage = 10;
                }
                else
                {
                    enemyMaxHp = currentDay == 3 ? 60 : 50;
                    attackDamage = 10;
                }
                break;
            case "Bowel":
                if (isDay)
                {
                    enemyMaxHp = 60;
                    attackDamage = 40;
                }
                else
                {
                    enemyMaxHp = currentDay == 1 ? 30 : currentDay == 2 ? 40 : 60;
                    attackDamage = currentDay == 1 ? 20 : currentDay == 2 ? 30 : 40;
                }
                break;
            case "Skull":
                if (isDay)
                {
                    enemyMaxHp = 40;
                    attackDamage = 10;
                }
                else
                {
                    enemyMaxHp = currentDay == 1 ? 40 : currentDay == 2 ? 80 : 90;
                    attackDamage = currentDay == 1 ? 10 : currentDay == 2 ? 10 : 20;
                }
                break;
        }
        enemyCurHp = enemyMaxHp;
    }
}
