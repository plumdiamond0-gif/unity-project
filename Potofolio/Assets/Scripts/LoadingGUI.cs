using UnityEngine;

public class LoadingGUI : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 57);  
    }
}
