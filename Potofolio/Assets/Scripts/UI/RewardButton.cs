using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    [SerializeField]RewardData data;
    Button button;
    Image image;
    TMP_Text text;

    float value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(ApplyBuff);
    }

    public void GetData(RewardData randData)
    {
        data = randData;
        image.sprite = data.RewardSprite;
        value = Random.Range(data.MinValue, data.MaxValue);
        text.text = $"{data.RewardName} : {value}";

    }
    void ApplyBuff()
    {
        GameObject player =  GameManager.instance.GetPlayer();
        Health health = player.GetComponent<Health>();
        PlayerStat stat = player.GetComponent<PlayerStat>();    

        
        switch (data.RewardType)
        {
            case RewardType.Heal:
                health.Heal(value);
                Debug.Log("Heal");

                break;
            case RewardType.DamageBuff:
                stat.BaseDamage *= value;
                Debug.Log("DamageUp");

                break;
            case RewardType.MaxHpPlus:
                health.MaxHp += value;
                Debug.Log("MaxHpUp");

                break;
            case RewardType.AttackSpeedPlus:
                stat.AttackSpeed *= value;
                Debug.Log("AttackSpeedUp");

                break;
            case RewardType.JumpPowerBuff:
                stat.JumpPower *= value;
                Debug.Log("JumpPowerBuff");

                break;

            case RewardType.MoveSpeedPlus:
                stat.MoveSpeed *= value;
                Debug.Log("MoveSpeedPlus");

                break;
        }
    }


}
