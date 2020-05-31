using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayCycle : MonoBehaviour
{
    public float daytime;
    public float lightDegrees = 0;
    public float timeScale;

    public TimeSpan myTime;
    public Light sun;
    public Color fogColor;
    public Gradient ambientGradient;

    public delegate void MorningCall();
    public delegate void NightCall();

    public MorningCall myMorningCall;
    public NightCall myNightCall;

    bool day = false;
    // Start is called before the first frame update
    void Start()
    {
        fogColor = RenderSettings.ambientLight;
    }

    // Update is called once per frame
    void Update()
    {
        daytime += Time.deltaTime * timeScale;
        myTime = TimeSpan.FromSeconds(daytime);
        lightDegrees = 360 * (daytime / 86400);
        transform.rotation = Quaternion.Euler(lightDegrees, -30, 0);

        float normalsun = Vector3.Dot(transform.forward, Vector3.down);
        sun.intensity = normalsun * 1.2f;

        RenderSettings.ambientLight = ambientGradient.Evaluate(normalsun + .4f);
        RenderSettings.fogColor = fogColor * normalsun;

        if (normalsun > 0)
        {
            if (!day)
            {
                myMorningCall();
                day = true;
            }
        }
        else
        {
            if (day)
            {
                myNightCall();
                day = false;
            }
        }
    }
}
