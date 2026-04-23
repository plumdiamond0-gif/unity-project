using UnityEngine;

public class Health : MonoBehaviour
{
    public float Hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

public void TakeHealth(float value)
    {
        Hp += value;
    }
}
