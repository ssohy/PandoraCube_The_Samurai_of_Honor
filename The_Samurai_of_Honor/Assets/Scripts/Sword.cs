using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;

    public int damage;
    public int basicDamage;
    public int doubleDamage;

    public float basicRate;
    public float doubleRate; // ��������� ����

    public BoxCollider meleeArea; // �浹����

    public void StartSwing()
    {
        damage = basicDamage;
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }
    public void StartDoubleSwing()
    {
        damage = doubleDamage;
        StopCoroutine("DoubleSwing");
        StartCoroutine("DoubleSwing");
    }
    IEnumerable Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
    }

    IEnumerable DoubleSwing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
    }
}
