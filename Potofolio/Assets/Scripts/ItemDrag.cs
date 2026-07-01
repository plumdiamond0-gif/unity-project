using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    Transform player;
    private void Start()
    {
        player = GameManager.instance.GetPlayer().transform;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, 4f * Time.deltaTime);
    }
}
