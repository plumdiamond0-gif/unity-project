using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Generator : MonoBehaviour
{
    [SerializeField] float spawnTerm;
    [SerializeField] float spawnAmount;
    [SerializeField] EnemyType enemyType;
    public bool canSpawn = true;
    [SerializeField] float detectdist;
    GameObject enemy;
    public GameObject player;

    private void Awake()
    {
        GameManager.OnPlayerSpawned += GetPlayer;
    }

    void GetPlayer(GameObject go)
    {
        player = go;
        GenerateReady();

    }
    void Start()
    {
        enemy = GM.GetPrefabManager().
            EnemyPrefabTable.EnemyPrefabDatas.Find
            (x => x.enemyType == enemyType).Enemy;

        canSpawn = true;
    }

    private void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
        }
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
            GameObject spawned = Instantiate(enemy, transform.position, Quaternion.identity, transform);
            //spawned.transform.localScale = new Vector3(5, 5f, 5);
        }
    }
}
