using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage;
    public int basicDamage;
    public int doubleDamage;
    public float basicRate;
    public float doubleRate;

    public BoxCollider meleeArea; // 충돌영역

    private IEnumerator currentSwingCoroutine;

    public void StartSwing()
    {
        SetDamage(basicDamage);
        StartSwingCoroutine(basicRate);
    }

    public void StartDoubleSwing()
    {
        SetDamage(doubleDamage);
        StartSwingCoroutine(doubleRate);
    }

    private void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void StartSwingCoroutine(float duration)
    {
        if (currentSwingCoroutine != null)
            StopCoroutine(currentSwingCoroutine);

        currentSwingCoroutine = SwingCoroutine(duration);
        StartCoroutine(currentSwingCoroutine);
    }

    private IEnumerator SwingCoroutine(float duration)
    {
        yield return new WaitForSeconds(0.01f);
        EnableMeleeArea();

        yield return new WaitForSeconds(duration);
        DisableMeleeArea();
    }

    private void EnableMeleeArea()
    {
        meleeArea.enabled = true;
    }

    private void DisableMeleeArea()
    {
        meleeArea.enabled = false;
    }
}
