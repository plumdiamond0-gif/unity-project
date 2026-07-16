using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelWeaponUnlock: MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image CenterImage;
    [SerializeField] Image LeftImage;
    [SerializeField] Image RightImage;
    [SerializeField] TMP_Text weaponNameText;
    [SerializeField] Button unlockButton;
    [SerializeField] Button LeftButton;
    [SerializeField] Button RightButton;
    [SerializeField] UnlockCostUI unlockCostUI;

    private int currentIndex = 0;

    WeaponPrefabTable weaponPrefabTable;
    WeaponPrefabData currentData => weaponPrefabDatas[currentIndex];

    List<WeaponPrefabData> weaponPrefabDatas;

    void Start()
    {
        weaponPrefabTable = GM.GetPrefabManager().WeaponPrefabTable;
        weaponPrefabDatas = weaponPrefabTable.weaponPrafabTableDatas;
        LeftButton.onClick.AddListener(() => { PrevWeapon(); GM.GetSoundManager().PlaySFX(AudioType.Button); });
        RightButton.onClick.AddListener(() => { NextWeapon(); GM.GetSoundManager().PlaySFX(AudioType.Button); });

        unlockButton.onClick.AddListener(()=>
        {
            if (SaveManager.CurrentData.weaponActive[currentData.weaponState])
                return;
            unlockCostUI.ShowCosts(currentData);
            GM.GetSoundManager().PlaySFX(AudioType.SpecialBtn);
        });

        RefreshUI();


    }

    public void NextWeapon()
    {
        Debug.Log("next");
        currentIndex++;
        if (currentIndex >= weaponPrefabDatas.Count)
            currentIndex = 0;

        RefreshUI();
    }

    public void PrevWeapon()
    {
        Debug.Log("prev");

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = weaponPrefabDatas.Count - 1;

        RefreshUI();
    }




    public void RefreshUI()
    {
        var data = currentData;

        CenterImage.sprite = data.WeaponImage;
        weaponNameText.text = data.weaponState.ToString();

        bool hasLeft = currentIndex > 0;
        bool hasRight = currentIndex < weaponPrefabDatas.Count - 1;

        LeftImage.enabled = hasLeft;
        RightImage.enabled = hasRight;
        LeftButton.gameObject.SetActive(hasLeft);
        RightButton.gameObject.SetActive(hasRight);

        if (hasLeft)
            LeftImage.sprite = weaponPrefabDatas[currentIndex - 1].WeaponImage;

        if (hasRight)
            RightImage.sprite = weaponPrefabDatas[currentIndex + 1].WeaponImage;

        if (SaveManager.CurrentData.weaponActive[data.weaponState])
        {
            unlockButton.GetComponent<Button>().interactable = false;
            unlockButton.GetComponentInChildren<TMP_Text>().text = "Unlocked";
        }
        else
        {
            unlockButton.GetComponent<Button>().interactable = true;
            unlockButton.GetComponentInChildren<TMP_Text>().text = "Unlock";
        }

    }
}