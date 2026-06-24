using System;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public WeaponPrefabTable WeaponPrefabTable;//{ get; private set; } = null;
    public EnemyPrefabTable EnemyPrefabTable;
    public ItemPrefabTable ItemPrefabTable;
    public static PrefabManager CreatePrefabManager(GameObject res, Transform parent)
    {
        if(res == null)
        {
            return null;
        }
        GameObject gameObject = Instantiate(res, parent);
        
        return gameObject.GetComponent<PrefabManager>();
        
        
    }

    public void Init(Action OnComplete)
    {
        GM.GetAssetManager().LoadAsset<WeaponPrefabTable>
          ("WeaponPrefabTable", (go) =>
          {
              WeaponPrefabTable = go;
              OnComplete?.Invoke();
          });

        GM.GetAssetManager().LoadAsset<EnemyPrefabTable>
          ("EnemyPrefabTable", (go) =>
          {
              EnemyPrefabTable = go;
              OnComplete?.Invoke();
          });

        GM.GetAssetManager().LoadAsset<ItemPrefabTable>
       ("ItemPrefabTable", (go) =>
       {
           ItemPrefabTable = go;
           OnComplete?.Invoke();
       });
    }
}
