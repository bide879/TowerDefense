using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDir = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    public void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDir = direction;
    }

}
