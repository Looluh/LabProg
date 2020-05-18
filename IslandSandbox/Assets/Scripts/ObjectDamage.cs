using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public SkinnedMeshRenderer rend;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        //Destroy(gameObject);
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        int blink = 6;

        while (blink > 0)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(0.1f);
            blink--;
        }
    }
}
