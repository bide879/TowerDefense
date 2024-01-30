using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpowner : MonoBehaviour
{

    [SerializeField]
    private GameObject towerPrefab1;
    [SerializeField]
    private GameObject towerPrefab2;
    [SerializeField]
    private GameObject towerPrefab3;
    [SerializeField]
    private GameObject towerPrefab4;


    [SerializeField]
    private int towerBuildGold = 50;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private Spowner Spowner;

    GameObject towerPrefab;

    int TowerBuildGold;
    public void SpownTower(Transform tileTransform)
    {
        towerPrefab = towerPrefab1;
        TowerBuildGold = towerBuildGold;

        if (towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }


        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            if (towerBuildGold + 19 < playerGold.CurrentGold && tile.Lever == 0)
            {
                TowerBuildGold = towerBuildGold + 20;
                towerPrefab = towerPrefab2;
                tile.Lever++;
            }
            else if (towerBuildGold + 49 < playerGold.CurrentGold && tile.Lever == 1)
            {
                TowerBuildGold = towerBuildGold + 50;
                towerPrefab = towerPrefab3;
                tile.Lever++;
            }
            else if (towerBuildGold + 99 < playerGold.CurrentGold && tile.Lever == 2)
            {
                TowerBuildGold = towerBuildGold + 100;
                towerPrefab = towerPrefab4;
                tile.Lever++;
            }
            else
            {
                return;
            }
           
        }

        tile.IsBuildTower = true;

        playerGold.CurrentGold -= TowerBuildGold;

        //Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);

        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(Spowner);

    }
}
