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

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(target.position);
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
        }
    }


    void OnEnable()
    {
        currentDay = gameManager.currentDay;
        isDay = gameManager.isDay;
        switch (enemyName)
        {
            case "TestEnemy":
                enemyMaxHp = 200;
                enemyCurHp = 200;
                attackDamage = 10;
                break;
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
                    enemyMaxHp = 80;
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
        
    












    /*
    protected virtual void Awake()
    {
        
        
        currentDay = 1;
        spawnInterval = 5f;
        dTime = 0;
        isDay = true;
        
        StartCoroutine(SpawnEnemies()); // enemy스폰 시작
    }


    protected virtual IEnumerator SpawnEnemies()
    {
        while (true)
        {
            
            IsDaytime(); // 타이머 시작
            samuraiDayCount = 0;
            bowelDayCount = 0;
            skullDayCount = 0;
            samuraiNightCount = 0;
            bowelNightCount = 0;
            skullNightCount = 0;

            switch (currentDay) // 스폰될 enemy의 수
            {
                case 1:
                    samuraiDayCount = 40;
                    bowelNightCount = 20;
                    skullNightCount = 40;
                    break;
                case 2:
                    samuraiDayCount = 60;
                    bowelNightCount = 30;
                    skullNightCount = 50;
                    break;
                case 3:
                    samuraiDayCount = 30;
                    bowelDayCount = 20;
                    skullDayCount = 40;
                    samuraiNightCount = 30;
                    bowelNightCount = 30;
                    skullNightCount = 60;
                    break;
            }

            if (isDay)
            {
                GenDay(samuraiEnemy, samuraiDayCount);
                yield return new WaitForSeconds(spawnInterval);

                GenDay(bowelEnemy, bowelDayCount);
                yield return new WaitForSeconds(spawnInterval);

                GenDay(skullEnemy, skullDayCount);
            }
            else
            {
                GenNight(samuraiEnemy, samuraiNightCount);
                yield return new WaitForSeconds(spawnInterval);

                GenNight(bowelEnemy, bowelNightCount);
                yield return new WaitForSeconds(spawnInterval);

                GenNight(skullEnemy, skullNightCount);
            }

            currentDay++;
            if (currentDay > 3)
            {
                // 엔딩으로 넘어가기
                yield return null;
            }

            
        }
    }

    //#.Generate During Day
    protected void GenDay(GameObject enemy, int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            Debug.Log("낮 적 스폰");
            Generate(enemy, "Day");
        }
    }

    //#.Generate During Night
    protected void GenNight(GameObject enemy, int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            Debug.Log("밤 적 스폰");
            Generate(enemy, "Night");
        }
    }

    protected void Generate(GameObject enemy, string timeOfDay)
    {
        Enemy enemyComponent = enemy.GetComponent<Enemy>();

        if (timeOfDay == "Day")
        {
            SetDayStats();
        }
        else if (timeOfDay == "Night")
        {
            SetNightStats();
        }

        Vector3 spawnPosition = spawnPoint.position; // 위치 조정할 것

        if (timeOfDay == "Night")
        {
            spawnPosition += new Vector3(0, 10f, 0); // 위치 조정할 것
        }

        Instantiate(enemy, spawnPosition, spawnPoint.rotation);
    }

    protected virtual void SetDayStats()
    {
        // Default
        if (currentDay == 1)
        {
            hp = 40;
            attackDamage = 10;
        }
        else if (currentDay == 2)
        {
            hp = 50;
            attackDamage = 15;
        }
        else if (currentDay == 3)
        {
            hp = 60;
            attackDamage = 20;
        }
    }

    protected virtual void SetNightStats()
    {
        // Default
        if (currentDay == 1)
        {
            hp = 40;
            attackDamage = 10;
        }
        else if (currentDay == 2)
        {
            hp = 50;
            attackDamage = 15;
        }
        else if (currentDay == 3)
        {
            hp = 60;
            attackDamage = 20;
        }
    }

    protected void IsDaytime()
    {
        if (currentDay == 1)
        {
            // 낮 10분, 밤 15분
            if (isDay == true)
            {
                dTime += Time.deltaTime;
                if (dTime >= 600f)
                    Debug.Log("10분 경과");
                dTime = 0;
                isDay = false;
            }
            else
            {
                //화면 어두워지고 안개 코드 구현할 것
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                    Debug.Log("15분 경과");
                dTime = 0;
                isDay = true;
            }

        }
        else if (currentDay == 2)
        {
            // 낮 10분, 밤 15분
            if (isDay == true)
            {
                dTime += Time.deltaTime;
                if (dTime >= 600f)
                    Debug.Log("10분 경과");
                dTime = 0;
                isDay = false;
            }
            else
            {
                //화면 어두워지고 안개 코드 구현할 것
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                    Debug.Log("15분 경과");
                dTime = 0;
                isDay = true;
            }

        }
        else if (currentDay == 3)
        {
            // 낮 15분, 밤 15분
            if (isDay == true)
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                    Debug.Log("15분 경과");
                dTime = 0;
                isDay = false;
            }
            else
            {
                //화면 어두워지고 안개 코드 구현할 것
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                    Debug.Log("15분 경과");
                dTime = 0;
            }

        }
    }void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 3000;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 40;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 3;
                break;
        }
    }*/
}
