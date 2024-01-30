using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBuildTower { set; get; }
    public int Lever;

    private void Awake()
    {
        Lever = 0;
        IsBuildTower = false;
    }
}

