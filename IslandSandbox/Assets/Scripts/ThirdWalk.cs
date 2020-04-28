using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdWalk : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walk,
        Jump,
        Die,
    }

    public States state;
    public Animator anim;
    public Rigidbody rdb;

    public Vector3 move { get; private set; }
    public float moveForce;

    Vector3 direction;

    GameObject referenceObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Idle());

        referenceObject = Camera.main.GetComponent<ThirdCam>().GetReferenceObject();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move = referenceObject.transform.TransformDirection(move);

        if (move.magnitude > 0)
        {
            direction = move;
        }
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.fixedDeltaTime*20);


        rdb.AddForce(move * (moveForce / (rdb.velocity.magnitude + 1)));
    }

    IEnumerator Idle()
    {
        //start
        state = States.Idle;

        while (state == States.Idle)
        {
            //update

            anim.SetFloat("Velocity", 0);
            if (rdb.velocity.magnitude > 0.1)
            {
                StartCoroutine(Walk());
            }
            yield return new WaitForEndOfFrame();

        }
        //saida do estado
    }

    IEnumerator Walk()
    {
        //start
        state = States.Walk;

        while (state == States.Walk)
        {
            //update

            anim.SetFloat("Velocity", rdb.velocity.magnitude);
            if (rdb.velocity.magnitude < 0.1f)
            {
                StartCoroutine(Idle());
            }
            //
            yield return new WaitForEndOfFrame();
        }
        //saida do estado
    }
}
