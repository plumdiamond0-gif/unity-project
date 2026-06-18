using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    RewardDataTable.RewardData data;
    Button button;
    Image image;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(ApplyBuff);
    }

    public void GetData(RewardDataTable.RewardData randData)
    {
        data = randData;
    }
    void ApplyBuff()
    {
        switch (data.RewardType)
        {
            case RewardType.Heal:

                break;
            case RewardType.DamageUp:
                break;
            case RewardType.MaxHpUp:
                break;
            case RewardType.AttackSpeedUp:
                break;
        }
    }


}
