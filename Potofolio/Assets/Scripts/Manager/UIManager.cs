using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

                PanelBase panelBase = go.GetComponent<PanelBase>();

                if(panelBase != null)
                {
                    string name = panelName.Replace("_", "");
                    if(!_dicContentPanels.ContainsKey(name))
                    {
                        _dicContentPanels[name] = panelBase;
                    }

                    panelBase.Init();
                    panelBase.Hide();
                }

                callback?.Invoke(go);
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



  
}
