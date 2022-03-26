using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool isGrounded;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void Move(Vector3 direction, float speed)
    {
        if (isGrounded)
        {
            rb.velocity += direction;
            rb.velocity = rb.velocity.normalized * speed;

            IDisposable inertia = Observable.EveryUpdate()
                .SkipWhile(_ => rb.velocity.sqrMagnitude < 3).TakeWhile(_ => rb.velocity != Vector3.zero).Subscribe(_ =>
                {
                    rb.velocity = Vector3.zero;
                });
        }
    }

    public void ApplyGravity(Vector3 direction, float speed)
    {

        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }
}
