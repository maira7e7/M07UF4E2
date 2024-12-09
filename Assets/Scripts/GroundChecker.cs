using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private float range = 1.1f;
    private Vector3 offset = new Vector3(0, 1f,0);
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
           RaycastHit hit;
        Ray ray = new Ray(transform.position + offset, -transform.up);
        Debug.DrawLine(transform.position +offset, transform.position +offset - transform.up * range, Color.red);
        if(Physics.Raycast(ray, out hit, range))
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.layer == 7)
            {
                Debug.Log("Walkable");
                animator.SetBool("isGrounded", true);
            }
            else
            {
                animator.SetBool("isGrounded", false);
            }
        }
        if(Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);
        }
        else 
        {
            animator.SetBool("isRunning", false);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
        }
        else 
        {
            animator.SetBool("isJumping", false);
        }
    }
}
