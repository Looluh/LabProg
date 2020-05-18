using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageControl : MonoBehaviour
{
    public int hp = 5;
    public GameObject counter;
    public bool ifra = false;

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

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.collider.CompareTag("Projectile") || collision.collider.CompareTag("DroppedWeapon") && iawalk.currentState == IAWalk.IaState.Dying)
        {
            hp--;
            iawalk.currentState = IAWalk.IaState.Damage;
        }

        if (hp <= 0 && iawalk.currentState != IAWalk.IaState.Dying)
        {
            //Destroy(gameObject, 5);
            //GetComponent<IAWalk>().enabled = false;
            //GetComponent<NavMeshAgent>().enabled = false;
            iawalk.currentState = IAWalk.IaState.Dying;
            counter.gameObject.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
        }
        */
    }

    public void EightD()
    {
        StartCoroutine(TakeDamage());
    }

    IEnumerator TakeDamage()
    {
        if (!ifra)
        {
            ifra = true;

            hp--;
            iawalk.currentState = IAWalk.IaState.Damage;

            if (hp <= 0 && iawalk.currentState != IAWalk.IaState.Dying)
            {
                iawalk.currentState = IAWalk.IaState.Dying;
                counter.gameObject.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);

            }
            yield return new WaitForSeconds(3f);

            ifra = false;
        }
    }
}
