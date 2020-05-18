using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightDShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        other.gameObject.SendMessage("EightD", SendMessageOptions.DontRequireReceiver);

    }
}
