using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    PanelPlayer HUD;


    private readonly Dictionary<string, PanelBase> _dicContentPanels = new();

    private Transform RootCanvas;
    public void GetRootCanvas(Transform rootCanvas) => RootCanvas = rootCanvas;
    public void ReleaseRootCanvas() => RootCanvas = null;

    public Health PlayerHealth;

    public void CreateUIPanel<T>(string panelName, Action<T> callback) where T : PanelBase  
    {
        string uiTitle = $"UI_{panelName}.prefab";
        GM.GetAssetManager().LoadAsset<GameObject>(uiTitle,
            (go) =>
            {
                if(RootCanvas == null)
                    return;
                RectTransform rt = Instantiate(go).GetComponent<RectTransform>();
                {
                    rt.SetParent(RootCanvas, false);
                    rt.localPosition = Vector3.zero;
                    rt.localRotation = Quaternion.identity;
                    rt.localScale = Vector3.one;
                }// rt √ ±‚»≠
                T panelBase = rt.gameObject.GetComponent<T>();
                if (panelBase != null)
                {
                    string name = panelName.Replace("_", "");
                    if (!_dicContentPanels.ContainsKey(name))
                    {
                        _dicContentPanels[name] = panelBase;
                        panelBase.Init();
                        panelBase.Show();
                    }
                }
                callback?.Invoke(panelBase);
            });
    }
    public void SaveHUD(PanelPlayer panel)
    {
        HUD = panel;
    }
    public PanelPlayer GetHUD()
    {
        return HUD; 
    }
    public void Bind(GameObject playerInfo)
    {
        if (HUD != null)
        {

            PlayerAttack playerAttack = playerInfo.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.attackGaugeBar = HUD.ChargeUI;
                playerAttack.attackGaugeBarFill = HUD.ChargeImageUI;
                playerAttack.weaponImage = HUD.WeapomImage;
                playerAttack.weaponText = HUD.WeapomText;   
                playerAttack.bulletNumText = HUD.BulletNum;
            }

            Health playerHealth = playerInfo.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.HealthBarFill = HUD.HpUI;
            }

            PlayerItem playerItem  = playerInfo.GetComponent<PlayerItem>();
            if (playerItem != null)
            {
                playerItem.expFillImageUI = HUD.ExpFillImageUI;
                playerItem.inventoryImages = HUD.Inventory;
            }    
        }
        GameManager.OnPlayerSpawned -= Bind;

    }
    public void RemoveKey(string key)
    {
        _dicContentPanels.Remove(key);
    }
}
