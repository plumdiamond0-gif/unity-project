using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Generator : MonoBehaviour
{
    [SerializeField] float spawnTerm;
    [SerializeField] float spawnAmount;
    [SerializeField] EnemyType enemyType;
    [SerializeField] InItemType inItemType;

    public bool canSpawn = true;
    [SerializeField] float detectdist;
    GameObject spawnObject;
    public GameObject player;

    private void Awake()
    {
        GameManager.OnPlayerSpawned += GetPlayer;
        canSpawn = true;
    }

    void GetPlayer(GameObject go)
    {
        player = go;
        GenerateReady();
    }
    void Start()
    {
        if(inItemType == InItemType.None)
        {
            spawnObject = GM.GetPrefabManager().
            EnemyPrefabTable.EnemyPrefabDatas.Find
            (x => x.enemyType == enemyType).enemyPrefab;
        }
        if(enemyType == EnemyType.None)
        {
            spawnObject = GM.GetPrefabManager().
                ItemPrefabTable.InItemDatas.Find
                (x => x.inItemType == inItemType).ItemPrefab;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist < detectdist && canSpawn)
            {
                GenerateReady();
            }
        }
    }
    public void GenerateReady()
    {
        canSpawn = false;
        StartCoroutine(GenerateRoutine());
    }

    IEnumerator GenerateRoutine()
    {
            Generate();
            yield return new WaitForSeconds(spawnTerm);
            canSpawn = true;
    }
    void Generate()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject spawned = Instantiate(spawnObject, transform.position, Quaternion.identity, transform);
            //spawned.transform.localScale = new Vector3(5, 5f, 5);
        }
    }
}
