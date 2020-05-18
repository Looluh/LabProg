using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    public float counter = 0;
    public float winCondition;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {
        counter++;
        if (counter == winCondition)
        {
            winScreen.SetActive(true);
        }
    }
}
