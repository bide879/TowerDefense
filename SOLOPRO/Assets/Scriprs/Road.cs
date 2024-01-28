using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    [SerializeField] Transform[] waypoint;
    [SerializeField] float speed = 5f;
    int waypointNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoint[waypointNum].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    public void MovePath()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, waypoint[waypointNum].transform.position, speed * Time.deltaTime);

        if (transform.position == waypoint[waypointNum].transform.position)
            waypointNum++;

        if (waypointNum == waypoint.Length)
            waypointNum = 0;
    }
}

