using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpowner : MonoBehaviour
{

    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private int towerBuildGold = 50;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private Spowner Spowner;

    public void SpownTower(Transform tileTransform)
    {
        if (towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }


        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            return;
        }

        tile.IsBuildTower = true;

        playerGold.CurrentGold -= towerBuildGold;

        //Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);

        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(Spowner);

    }
}
