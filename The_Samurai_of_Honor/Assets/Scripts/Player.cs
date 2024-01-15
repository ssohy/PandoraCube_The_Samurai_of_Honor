using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveSpeed = 2; // �̵� �ӵ�
    public GameObject player;
    public int hp;
    void Start()
    {
        player = GameObject.Find("Player");
        hp = 100;
    }

    void Update()
    {
        Move();
    }

    void Move() // ����� ���Ǽ��� ���� W, A, D�� �̵���
    {
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0) );
        }

        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
    }

}
