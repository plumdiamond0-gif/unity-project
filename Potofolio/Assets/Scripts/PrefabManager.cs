using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public WeaponPrafabTable WeaponPrafabTable;
    public static PrefabManager CreatePrefabManager(GameObject res, Transform parent)
    {
        if(res == null)
        {
            return null;
        }
        GameObject gameObject = Instantiate(res, parent);
        
        return gameObject.GetComponent<PrefabManager>();
        
        
    }
}
