using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{
    [SerializeField]
    public GameObject emenyPrefab;
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    public float interval;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private PlayerGold playerGold;


    private List<Eneny> enemyList;

    public List<Eneny> EnemyList => enemyList;


    private void Awake()
    {
        enemyList = new List<Eneny>();

        StartCoroutine("SpawnEnemy");
    }


    private IEnumerator SpawnEnemy()
    {
        while (true) // 무한반복
        {
            GameObject clone = Instantiate(emenyPrefab);
            Eneny eneny = clone.GetComponent<Eneny>();

            eneny.Setup(this, wayPoints);
            enemyList.Add(eneny);

            SpawnEnemyHPSlider(clone);

            yield return new WaitForSeconds(interval);  //interval만큼 기다린후

            // Spawn 실행
        }

    }



    public void enemyDestroy(EnemyDestoryType type, Eneny enemy, int gold)
    {
        if (type == EnemyDestoryType.Arrive)
        {
            playerHP.TakeDamage(1);
        }else if(type == EnemyDestoryType.kill)
        {
            playerGold.CurrentGold += gold;
        }

        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject eneny)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(eneny.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(eneny.GetComponent<EnemyHp>());
    }

}
