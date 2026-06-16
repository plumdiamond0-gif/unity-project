using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{

    Image weaponImage;
    Button Button;
    public WeaponState weaponState;
    public Image LockImage;
    public bool isActive;
    [SerializeField] private PanelWeaponUpgrade  panelWeaponUpgrade;

    void Start()
    {
        //LockImage = GetComponentInChildren<Image>();
        weaponImage = GetComponent<Image>();
        weaponImage.sprite = GM.GetPrefabManager().WeaponPrefabTable
            .weaponPrafabTableDatas.Find(x => x.weaponState == weaponState).WeaponImage;
        Button = GetComponent<Button>();

        if (SaveManager.CurrentData.WeaponActive[weaponState] == true)
        {
            isActive = true;
            LockImage.enabled = false;
            weaponImage.enabled = true;
        }
        else
        {
            isActive = false;
            LockImage.enabled = true;
            weaponImage.enabled = false;
        }
            Button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if(!isActive) 
            return;
        panelWeaponUpgrade.Show(weaponState);
    }
    //클릭 시 호출

    public void BeActive()
    {
        if (SaveManager.CurrentData.WeaponActive[weaponState] != true)
            return;
        //인벤토리 칸 자식들 중에서 컴포넌트 꺼내워서 같은 무기 타입 갖고 있는 애의 함수 호출
        isActive = true;
        LockImage.enabled = false;
        weaponImage.enabled = true;
    }
    //무기 획득했을 때 호출
}
