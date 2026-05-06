using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, 26 * Time.deltaTime, 0);
        //transform.Translate(0, dir * Time.deltaTime , 0);
        //if (transform.position.y >= pos.y + 1 || transform.position.y < pos.y)
        //{
        //    dir *= -1;
        //}

        float y = (Mathf.Sin(Time.time * 2f) + 1) * 0.2f;
        transform.position = pos + new Vector3(0, y, 0);

    }
}
