using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{
    [SerializeField]
    public GameObject emenyPrefab1;
    [SerializeField]
    public GameObject emenyPrefab2;
    [SerializeField]
    public GameObject emenyPrefab3;
    [SerializeField]
    public GameObject emenyPrefab4;
    [SerializeField]
    public GameObject emenyPrefab5;
    [SerializeField]
    public GameObject emenyBossPrefab;
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

    GameObject emenyPrefab;

    private List<Eneny> enemyList;

    public List<Eneny> EnemyList => enemyList;

    private bool BossTrigger = false;
    private float timeEnd = 80.0f;

    private void Awake()
    {
        enemyList = new List<Eneny>();
        StartCoroutine("SpawnEnemy");
    }

    private void Update()
    {
        if(BossTrigger == true)
        {
            timeEnd -= Time.deltaTime;
            if(timeEnd < 0)
            {
                GameManager.Instance.GameClear();
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        emenyPrefab = emenyPrefab1;
        int countEnemy = 0;
        while (true) // 무한반복
        {
            if(countEnemy == 300)
            {
                SpawBossEnemy();
                yield break;
            }


            if(countEnemy > 200)
            {
                emenyPrefab = emenyPrefab5;
            }else if(countEnemy > 150)
            {
                emenyPrefab = emenyPrefab4;
            }
            else if(countEnemy > 100)
            {
                emenyPrefab = emenyPrefab3;
            }
            else if (countEnemy > 50)
            {
                emenyPrefab = emenyPrefab2;
            }
            
            GameObject clone = Instantiate(emenyPrefab);
            Eneny eneny = clone.GetComponent<Eneny>();

            eneny.Setup(this, wayPoints);
            enemyList.Add(eneny);

            SpawnEnemyHPSlider(clone);
            countEnemy++;
            yield return new WaitForSeconds(interval);  //interval만큼 기다린후

            // Spawn 실행
        }

    }

    private void SpawBossEnemy()
    {
        emenyPrefab = emenyBossPrefab;

        GameObject clone = Instantiate(emenyPrefab);
        Eneny eneny = clone.GetComponent<Eneny>();

        eneny.Setup(this, wayPoints);
        enemyList.Add(eneny);

        SpawnEnemyHPSlider(clone);

        BossTrigger = true;
        
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
