using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
    private bool isDie = false;
    private Eneny enemy;
    private SpriteRenderer spriteRenderer;

    Spowner spowner;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;


    private void Awake()
    {
        currentHP = maxHP;
        enemy = GetComponent<Eneny>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isDie == true) { return; }
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if (currentHP <= 0)
        {
            isDie = true;
            enemy.OnDie(EnemyDestoryType.kill);

        }

    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;
        color.a = 0.4f;

        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        spriteRenderer.color = color;

    }


}
