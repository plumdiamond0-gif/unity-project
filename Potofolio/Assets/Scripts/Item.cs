using UnityEngine;

public class Item : MonoBehaviour
{
    float dir = 1;
    Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos= transform.position;

 

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 26 * Time.deltaTime, 0);
        //transform.Translate(0, dir * Time.deltaTime , 0);
        //if (transform.position.y >= pos.y + 1 || transform.position.y < pos.y)
        //{
        //    dir *= -1;
        //}

        float y = (Mathf.Sin(Time.time * 2f)+1) * 0.5f; 
        transform.position = pos + new Vector3(0, y, 0);

    }
}
