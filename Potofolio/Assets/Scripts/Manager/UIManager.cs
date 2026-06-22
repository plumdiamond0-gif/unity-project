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

    public void CreateUIPanel(string panelName, Action<GameObject> callback)
    {
        string uiTitle = $"UI_{panelName}.prefab";
        GM.GetAssetManager().LoadAsset<GameObject>(uiTitle,
            (go) =>
            {
                if(RootCanvas == null)
                {
                    Debug.LogError("RootCanvas == null");
                    return;
                }

                RectTransform rt = Instantiate(go).GetComponent<RectTransform>();
                rt.SetParent(RootCanvas, false);

                rt.localPosition = Vector3.zero;
                rt.localRotation = Quaternion.identity;
                rt.localScale = Vector3.one;

                //TODO: 오프닝 이미지에 패널베이스 상속받는 스프킬븥 넣기, 처음 불러와질 때, 숨길 때 페이드 인으로 

                PanelBase panelBase = rt.GetComponent<PanelBase>();

                if (panelBase != null)
                {
                    string name = panelName.Replace("_", "");
                    if (!_dicContentPanels.ContainsKey(name))
                    {
                        _dicContentPanels[name] = panelBase;
                    }

                    panelBase.Init();
                    panelBase.Show();
                   // panelBase.Hide();
                }

                callback?.Invoke(rt.gameObject);
            });
    }

    public T GetPanel<T>() where T : PanelBase
    {
        string name = typeof(T).ToString();
        Debug.Log("name: " + name);
        PanelBase panel = null;
        _dicContentPanels.TryGetValue(name, out panel);

        return (T) panel;

    }
    public Inventory Inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UIManager CreateUIManager(GameObject res, Transform parent)
    {
        if (res == null)
        {
            return null;
        }
        GameObject gameObject = Instantiate(res, parent);

        return gameObject.GetComponent<UIManager>();
    }

    //public void GetPlayer(GameObject gameObject)
    //{
    //    playerInfo = gameObject;
    //}
    public void GetHUD(PanelPlayer panel)
    {
        HUD = panel;
    }

    private void Awake()
    {
        GameManager.OnPlayerSpawned += Bind;
    }

    public void Bind(GameObject playerInfo)
    {
        if (HUD != null)
        {
            PlayerAttack playerAttack = playerInfo.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.AttackGuageBar = HUD.ChargeUI;
                playerAttack.AttackGuageBarFill = HUD.ChargeImageUI;

            }

            Health playerHealth = playerInfo.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.HealthBarFill = HUD.HpUI;
            }

            PlayerMovement playerMovement = playerInfo.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {

            }
        }
    }
  
}
