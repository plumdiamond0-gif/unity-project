using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinText;
    float coinNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     coinNum = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            coinNum += 10;
            UpdateCoin(coinNum);
            Destroy(other.gameObject);
        }
    }

    public void UpdateCoin(float coinNum)
    {
        CoinText.text = coinNum.ToString();
    }
}
