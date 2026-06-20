using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Generator : MonoBehaviour
{
    [SerializeField] float spawnTerm;
    [SerializeField] float spawnAmount;
    [SerializeField] EnemyType enemyType;
    public bool canSpawn;
    GameObject enemy;

    void Start()
    {
        enemy = GM.GetPrefabManager().
            EnemyPrefabTable.EnemyPrefabTableDatas.Find
            (x => x.enemyType == enemyType).Enemy;
    }
    public void GenerateReady()
    {
        if(!canSpawn) 
            return;
        StartCoroutine(GenerateRoutine());
    }

    IEnumerator GenerateRoutine()
    {
        while(canSpawn)
        {
            Generate();
            yield return new WaitForSeconds(spawnTerm);
        }
    }
    void Generate()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
        
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
