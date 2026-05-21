using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    
    Button Button;
    string name;
    public WeaponPrefabTable.WeaponPrefabTableData.WeaponState weaponState;
    public Image LockImage;
    Image image;
    public bool isActive;
    bool virgin;
    WeaponInventoryUI weaponInventoryUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponInventoryUI = transform.parent.GetComponent<WeaponInventoryUI>(); 
        //LockImage = GetComponentInChildren<Image>();
        image = GetComponent<Image>();
        Button = GetComponent<Button>();

        virgin = true;

        image.enabled = true;
        LockImage.enabled = false;
        
        Button.onClick.AddListener(Show);
    }

    public void Show()
    {
        weaponInventoryUI.Show(weaponState, isActive);
    }
    //클릭 시 호출

    public void Active()
    {
        //인벤토리 칸 자식들 중에서 컴포넌트 꺼내워서 같은 무기 타입 갖고 있는 애의 함수 호출
        if( virgin)
        {
            isActive = true;
            LockImage.enabled = false;
            image.enabled = true;
            virgin = false;

        }
    }
    //무기 획득했을 때 호출
}
