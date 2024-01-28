using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Move move;
    private Transform target;
    private int damage;

    public void Setup(Transform target, int damage)
    {
        move = GetComponent<Move>();
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if ( target != null )
        {
            Vector3 direction = (target.position - transform.position).normalized;
            move.MoveTo(direction);
        }
        else
        {
            Destroy( gameObject );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( !collision.CompareTag("Enemy")  ) return;
        if(collision.transform != target ) return;

        collision.GetComponent<EnemyHp>().TakeDamage(damage);
        Destroy( gameObject );
    }

}
