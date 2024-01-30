using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum EnemyDestoryType { kill = 0, Arrive}
public class Eneny : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;

    private int currentIndex = 0;
    private Move move;
    private Spowner spowner;

    [SerializeField]
    private int gold = 10;


    internal void Setup(Spowner spowner, Transform[] wayPoints)
    {
        move = GetComponent<Move>();
        this.spowner = spowner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");

    }


    /*
    public void MovePath(Transform[] wayPoint)
    {
        move = GetComponent<Move>();
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoint;

        transform.position = wayPoint[currentIndex].position;

        StartCoroutine("OnMove");
    }*/

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            transform.Rotate(Vector3.forward * 10);

            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * move.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }


    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPoints.Length - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            move.MoveTo(direction);
        }
        else
        {
            gold = 0;
            OnDie(EnemyDestoryType.Arrive);
        }
    }

    public void OnDie(EnemyDestoryType type)
    {
        spowner.enemyDestroy(type, this, gold);
    }




}