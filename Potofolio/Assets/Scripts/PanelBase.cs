using System;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    public Action OnBeforShow { get; set; } = null;
    public Action OnFinishedHide { get; set; } = null;

    public virtual void Init() { }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }



    public virtual void Show() 
    { 
        OnBeforShow?.Invoke();    
    }

    public virtual void Hide()
    {
        OnFinishedHide?.Invoke();
    }
}
