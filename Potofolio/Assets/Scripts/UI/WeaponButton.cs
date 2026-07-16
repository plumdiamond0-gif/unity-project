using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{

   [SerializeField] Image weaponImage;
   public Button Button;
    public WeaponState weaponState;
    public Image LockImage;
    public bool isActive;
    [SerializeField] private PanelWeaponUpgrade  panelWeaponUpgrade;

    private void Awake()
    {
        weaponImage = GetComponent<Image>();
        Button = GetComponent<Button>();

    }

    void Start()
    {
        //LockImage = GetComponentInChildren<Image>();
        weaponImage.sprite = GM.GetPrefabManager().WeaponPrefabTable
            .weaponPrafabTableDatas.Find(x => x.weaponState == weaponState).WeaponImage;
        if (weaponImage == null)
        {
            Debug.Log("D@$@EWfergerghre");
        }

        if (SaveManager.CurrentData.weaponActive[weaponState] == true)
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
            Button.onClick.AddListener(() => { OnClick(); GM.GetSoundManager().PlaySFX(AudioType.NormalBtn); });
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
        isActive = true;
        LockImage.enabled = false;
        weaponImage.enabled = true;
    }
    //무기 획득했을 때 호출
}
