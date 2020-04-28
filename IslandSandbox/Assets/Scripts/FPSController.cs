using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    Vector3 playeraxis;
    public CharacterController cctrl;
    //public GameObject projectileprefab;
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        playeraxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cctrl.SimpleMove(transform.TransformDirection(playeraxis*5));
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        head.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));



        /*
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject currentball = Instantiate(projectileprefab, transform.position + head.transform.forward, Quaternion.identity);
            currentball.GetComponent<Rigidbody>().AddForce(head.transform.forward * 1000);
        }
        */
    }
}
