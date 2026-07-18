using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    [SerializeField]RewardData data;
    Button button;
    [SerializeField]Image image;
    TMP_Text text;

    float value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAttack playerAttack =  GameManager.instance.GetPlayer().GetComponent<PlayerAttack>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(() => {
            playerAttack.CanAttack = true;
            ApplyBuff(); 
            GM.GetSoundManager().PlaySFX(AudioType.Button); });
    }

    public void GetData(RewardData randData)
    {
        data = randData;
        image.sprite = data.RewardSprite;

        value = Random.Range(data.MinValue, data.MaxValue);

        if (value > 10)
        {
            value = Mathf.Round(value);
            text.text = $"{data.RewardName} : + {value}";
        }
        else
        {
            value = Mathf.Round(value * 100) / 100;
            text.text = $"{data.RewardName} : x {value}";
        }

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
                stat.DamageMultiplier *= value;
                Debug.Log("DamageUp");

                break;
            case RewardType.MaxHpPlus:
                health.MaxHpPlus(value);
                Debug.Log("MaxHpUp");

                break;
            case RewardType.AttackSpeedPlus:
                stat.AttackSpeedMultiplier *= value;
                Debug.Log("AttackSpeedUp");

                break;
            case RewardType.JumpPowerBuff:
                stat.JumpPowerMultiplier *= value;
                Debug.Log("JumpPowerBuff");

                break;

            case RewardType.MoveSpeedPlus:
                stat.MoveSpeedMultiplier *= value;
                Debug.Log("MoveSpeedPlus");

                break;
        }
        Time.timeScale = 1f;
        Destroy(gameObject.transform.parent.gameObject);
    }


}
