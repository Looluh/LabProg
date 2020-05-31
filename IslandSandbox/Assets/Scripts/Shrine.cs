using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shrine : MonoBehaviour
{
    public static Vector3 lastPlyrPos;
    public bool backToWorld = false;
    public string shrineToLoad;
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
        if(other.gameObject.CompareTag("Player"))
        SceneManager.LoadScene(shrineToLoad);
    }
}
