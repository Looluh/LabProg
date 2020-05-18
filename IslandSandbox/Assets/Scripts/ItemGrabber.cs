using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    public GameObject weaponOnHand;

    public Transform handPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedWeapon"))
        {
            if (weaponOnHand)
            {
                weaponOnHand.transform.parent = null;
                //RigidbodyConstraints constraint = new RigidbodyConstraints;

                weaponOnHand.GetComponent<Rigidbody>().isKinematic = false;
                //weaponOnHand.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);

                weaponOnHand.transform.Translate(-transform.up*100);

                weaponOnHand.layer = 0;
            }

            weaponOnHand = other.gameObject;
            other.transform.parent = handPos;
            other.transform.localPosition = Vector3.zero;
            weaponOnHand.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localRotation = Quaternion.identity;
            other.transform.gameObject.layer = transform.gameObject.layer;
        }
    }
}
