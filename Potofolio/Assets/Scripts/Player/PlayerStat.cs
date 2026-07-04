using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpPower ;
    public float BaseDamage ;
    public float AttackDamage;
    public float currentExp = 0;
    public float maxExp = 0;  

    public Image ExpFillImageUI;


    public void GetExp(float exp)
    {
        currentExp += exp;
        UpdateExpUI();
    }

    void UpdateExpUI()
    {
        while (currentExp >= maxExp)
        {
            currentExp = currentExp - maxExp;
            maxExp *= 1.5f;
            //GM.GetUIManager().
        }
        currentExp = Mathf.Clamp(currentExp, 0, maxExp);
        float ratio = currentExp / maxExp;
        ExpFillImageUI.fillAmount = ratio;
    }
    void Start()
    {
        UpdateExpUI();
    }

}
