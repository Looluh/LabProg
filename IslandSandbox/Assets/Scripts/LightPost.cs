using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPost : MonoBehaviour
{
    public Light lamp;
    public MeshRenderer lampMat;
    // Start is called before the first frame update
    void Start()
    {
        DayCycle dayscript = GameObject.FindObjectOfType<DayCycle>();
        dayscript.myNightCall += TurnOn;
        dayscript.myMorningCall += TurnOff;
    }

    public void TurnOn()
    {
        lamp.enabled = true;
        lampMat.materials[1].EnableKeyword("_EMISSION");
    }

    public void TurnOff()
    {
        lamp.enabled = false;
        lampMat.materials[1].EnableKeyword("_EMISSION");

    }
}
