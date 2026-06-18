using System.Collections;
using UnityEngine;

public class door : TriggerObject
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Trigger(GameObject entered)
    {
        anim.SetBool("Opened", true);
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Opened", false);

    }
}
