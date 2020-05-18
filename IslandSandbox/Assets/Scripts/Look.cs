using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public ThirdWalk walkscript;
    private void OnTriggerEnter(Collider other)
    {
        walkscript.objectToLook = other.gameObject;
    }
}
