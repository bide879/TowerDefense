using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    TextMeshProUGUI life;

    int lifePoint = 0;



    private void Awake()
    {
        life = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        lifePoint = 10;
    }

    private void Update()
    {
        life.text = $"Life : {lifePoint}";
    }



}
