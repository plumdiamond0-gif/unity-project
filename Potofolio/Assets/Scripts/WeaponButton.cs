using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    
    Button Button;
    string name;
    public WeaponPrefabTable.WeaponPrefabTableData.WeaponState weaponState;
    public Image LockImage;
    Image image;
    bool isActive;
    public Image Upgraded;
    bool virgin;
    WeaponInventoryUI weaponInventoryUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponInventoryUI = transform.parent.GetComponent<WeaponInventoryUI>(); 
        virgin = true;
        LockImage = GetComponentInChildren<Image>();
        image = GetComponent<Image>();
        isActive = false;

            image.enabled = false;
            LockImage.enabled = true;
        
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Show);
    }

    public void Show()
    {
        weaponInventoryUI.Show(weaponState);
    }
    //클릭 시 호출

    public void Active(WeaponPrefabTable.WeaponPrefabTableData.WeaponState State)
    {
        if(weaponState == State && virgin)
        {
            isActive = true;
            LockImage.enabled = false;
            image.enabled = true;
            virgin = false;

        }
    }
    //무기 획득했을 때 호출
}
