using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdWalk : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walk,
        Jump,
        Suffer,
        Die,
        Attack,
        Special,
    }

    public States state;
    public Animator anim;
    public Rigidbody rdb;
    public bool SpecialOn = false;

    public GameObject spcl;
    public Vector3 move { get; private set; }
    public float moveForce;

    Vector3 direction;
    public float jumpForce = 1000;
    public float jumpTime = .5f;
    GameObject referenceObject;

    public bool ikActive = false;
    public GameObject objectToLook;
    public Vector3 lookPosition;

    public float hp = 5;
    public bool takingDamage = false;
    public GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Idle());

        referenceObject = Camera.main.GetComponent<ThirdCam>().GetReferenceObject();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state != States.Die)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            move = referenceObject.transform.TransformDirection(move);

            if (move.magnitude > 0)
            {
                direction = move;
            }
            transform.forward = Vector3.Lerp(transform.forward, direction, Time.fixedDeltaTime * 20);


            rdb.AddForce(move * (moveForce / (rdb.velocity.magnitude + 1)));

            Vector3 velocityWoY = new Vector3(rdb.velocity.x, 0, rdb.velocity.z);
            rdb.AddForce(-velocityWoY * 500);

            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 65279))
            {
                anim.SetFloat("GroundDistance", hit.distance);
            }
        }
    }

    private void Update()
    {
        if (state != States.Die)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Attack());
            }

            if (Input.GetButtonDown("Fire2") && !SpecialOn)
            {
                SpecialOn = !SpecialOn;
                StartCoroutine(Special());
            }


            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine(Jump());
            }
            if (Input.GetButtonUp("Jump"))
            {
                jumpTime = 0;
            }
        }
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

    IEnumerator Attack()
    {
        //start
        state = States.Attack;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Idle());


        //saida do estado
    }

    IEnumerator Special()
    {
        //start
        state = States.Special;
        anim.SetTrigger("Special");

        yield return new WaitForSeconds(0.8f);

        GameObject projectile1 = Instantiate(spcl, transform.position + transform.forward, transform.rotation);
        GameObject projectile2 = Instantiate(spcl, transform.position - transform.forward, transform.rotation);
        GameObject projectile3 = Instantiate(spcl, transform.position + transform.right, transform.rotation);
        GameObject projectile4 = Instantiate(spcl, transform.position - transform.right, transform.rotation);
        GameObject projectile5 = Instantiate(spcl, transform.position + transform.forward + transform.right, transform.rotation);
        GameObject projectile6 = Instantiate(spcl, transform.position + transform.forward - transform.right, transform.rotation);
        GameObject projectile7 = Instantiate(spcl, transform.position - transform.forward + transform.right, transform.rotation);
        GameObject projectile8 = Instantiate(spcl, transform.position - transform.forward - transform.right, transform.rotation);
        projectile1.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        projectile2.GetComponent<Rigidbody>().AddForce(-transform.forward * 500);
        projectile3.GetComponent<Rigidbody>().AddForce(transform.right * 500);
        projectile4.GetComponent<Rigidbody>().AddForce(-transform.right * 500);
        projectile5.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right) * 500);
        projectile6.GetComponent<Rigidbody>().AddForce((transform.forward - transform.right) * 500);
        projectile7.GetComponent<Rigidbody>().AddForce((-transform.forward + transform.right) * 500);
        projectile8.GetComponent<Rigidbody>().AddForce((-transform.forward - transform.right) * 500);
        Destroy(projectile1, 1.5f);
        Destroy(projectile2, 1.5f);
        Destroy(projectile3, 1.5f);
        Destroy(projectile4, 1.5f);
        Destroy(projectile5, 1.5f);
        Destroy(projectile6, 1.5f);
        Destroy(projectile7, 1.5f);
        Destroy(projectile8, 1.5f);


        SpecialOn = !SpecialOn;

        StartCoroutine(Idle());

        //saida do estado
    }

    IEnumerator Jump()
    {
        //start
        state = States.Jump;
        jumpTime = 0.5f;
        
        if (Physics.Raycast(transform.position + Vector3.up * .5f, Vector3.down, out RaycastHit hit, 65279))
        {
            if (hit.distance > 0.6f)
            {
                StartCoroutine(Idle());
            }
        }
        while (state == States.Jump)
        {
            //update

            rdb.AddForce(Vector3.up * jumpForce * jumpTime);

            jumpTime -= Time.fixedDeltaTime;
            if (jumpTime < 0)
            {
                StartCoroutine(Idle());
            }

            yield return new WaitForFixedUpdate();
        }
        //saida do estado
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (ikActive)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, 3, 65279))
            {
                if (hit.collider.CompareTag("Push"))
                {
                    anim.SetIKPosition(AvatarIKGoal.LeftHand, hit.point - transform.right * 0.2f);
                    anim.SetIKPosition(AvatarIKGoal.RightHand, hit.point + transform.right * 0.2f);
                }
            }
        }

        if (objectToLook)
        {
            anim.SetLookAtWeight(1);
            lookPosition = Vector3.Lerp(lookPosition, objectToLook.transform.position, Time.deltaTime * 10);
            anim.SetLookAtPosition(lookPosition + Vector3.up * .5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && (state != States.Die) && !takingDamage)
        {
            StartCoroutine(Suffer());
        }
    }

    IEnumerator Suffer()
    {
        //start
        takingDamage = !takingDamage;

        state = States.Suffer;
        hp--;
        anim.SetTrigger("Hit");

        if (hp <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(Idle());
        }

        yield return new WaitForSeconds(3f);
        takingDamage = !takingDamage;

        //saida do estado
    }

    IEnumerator Die()
    {
        state = States.Die;

        anim.SetBool("Death", true);
        yield return new WaitForEndOfFrame();

        deathScreen.SetActive(true);
    }

}