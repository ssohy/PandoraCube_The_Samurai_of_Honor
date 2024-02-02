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
    Material mat;
    NavMeshAgent nav;

    public Transform target;
    bool isChase;

    int enemyCnt;
    //Animator anim;
    void Awake()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        
        GameObject objectManagerObject = GameObject.Find("ObjectManager");
        objectManager = objectManagerObject.GetComponent<ObjectManager>();

        player = GameObject.Find("Player");
        target = player.GetComponent<Transform>();


        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        enemyCnt = gameManager.enemyCnt;
    }

    /*void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }*/
    void Update()
    {

        nav.SetDestination(target.position);
        /*if (nav.enabled)
        {
            
            //nav.isStopped = !isChase;
        }*/
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
        //mat.color = Color.red;
        yield return null;

        if(enemyCurHp > 0)
        {
            //mat.color = Color.white;
        }
        else
        {
            //mat.color = Color.gray;
            Destroy(gameObject, 4);
            enemyCnt++;
        }
    }


    void OnEnable()
    {
        currentDay = gameManager.currentDay;
        isDay = gameManager.isDay;
        switch (enemyName)
        {
            case "Samurai":
                if (isDay)
                {
                    if (currentDay == 3)
                    {
                        enemyMaxHp = 60;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 10;
                    }
                    else
                    {
                        enemyMaxHp = 30;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 10;
                    }
                }
                else
                {
                    if(currentDay == 3)
                    {
                        enemyMaxHp = 60;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 10;
                    }
                    else
                    {
                        enemyMaxHp = 50;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 10;
                    }
                }
                break;
            case "Bowel":
                if (isDay)
                {
                    enemyMaxHp = 60;
                    enemyCurHp = enemyMaxHp;
                    attackDamage = 40;
                }
                else
                {
                    if(currentDay == 1)
                    {
                        enemyMaxHp = 30;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 20;
                    }
                    else if(currentDay == 2)
                    {
                        enemyMaxHp = 40;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 30;
                    }
                    else if (currentDay == 3)
                    {
                        enemyMaxHp = 60;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 40;
                    }

                }
                break;
            case "Skull":
                if (isDay)
                {
                    enemyMaxHp = 40;
                    enemyCurHp = enemyMaxHp;
                    attackDamage = 10;
                }
                else
                {
                    if (currentDay == 1)
                    {
                        enemyMaxHp = 40;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 10;
                    }
                    else if (currentDay == 2)
                    {
                        enemyMaxHp = 80;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 10;
                    }
                    else if (currentDay == 3)
                    {
                        enemyMaxHp = 90;
                        enemyCurHp = enemyMaxHp;
                        attackDamage = 20;
                    }
                }
                break;
        }
    }
}
