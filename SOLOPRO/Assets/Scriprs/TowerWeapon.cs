using System.Collections;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float attackRate = 0.5f;
    [SerializeField]
    private float attackRange = 2.0f;
    [SerializeField]
    private int attackDamage = 1;


    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private Spowner spowner;

    public void Setup(Spowner spowner)
    {
        this.spowner = spowner;

        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());

        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if(attackTarget != null)
        {
            RotateToTarget();
        }


    }

    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while(true)
        {
            float closestDistSpr = Mathf.Infinity;
            for (int i = 0; i < spowner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(spowner.EnemyList[i].transform.position, transform.position);
                if(distance <= attackRange && distance <= closestDistSpr)
                {
                    closestDistSpr = distance;
                    attackTarget = spowner.EnemyList[i].transform;
                }
            }
            if(attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;

        }
    }

    private IEnumerator AttackToTarget()
    {
        while(true)
        {
            if(attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            float distance = Vector3.Distance(attackTarget.position, transform.position);

            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            yield return new WaitForSeconds(attackRange);


            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
    }


}
