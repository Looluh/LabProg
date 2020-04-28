using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageControl : MonoBehaviour
{
    public int hp = 5;

    public IAWalk iawalk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        iawalk.currentState = IAWalk.IaState.Dying;
        //Destroy(gameObject, 5);
        //GetComponent<IAWalk>().enabled = false;
        //GetComponent<NavMeshAgent>().enabled = false;

    }

    /*public void TakeDamage()
    {
        hp--;

        if (hp <= 0)
            Die();


    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            hp--;
            iawalk.currentState = IAWalk.IaState.Damage;
        }

        if (hp <= 0)
        {
            //Destroy(gameObject, 5);
            //GetComponent<IAWalk>().enabled = false;
            //GetComponent<NavMeshAgent>().enabled = false;
            iawalk.currentState = IAWalk.IaState.Dying;

        }
    }
}
