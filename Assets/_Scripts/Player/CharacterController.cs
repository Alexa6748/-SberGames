using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameStateController controller;

    private Player player;
    public bool isGrounded;
    private Rigidbody rb;

    Vector3 zero = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        Observable.Timer(TimeSpan.FromSeconds(2)).TakeWhile(_ => isGrounded == false && player.HasState("OnRoadEnd")).Subscribe(_ =>
        {
            controller.GameOver();
        });
    }

    public void Move(Vector3 direction, float speed)
    {
        if (isGrounded && direction != zero)
        {
            rb.velocity += direction * speed;
            rb.velocity = rb.velocity.normalized * speed;
        }
        else if (direction == zero)
        {
            rb.velocity = zero;
        }
    }

    public static Vector3 ZeroY(Vector3 vector)
    {
        Vector3 newVector = vector;
        newVector.y = 0;
        return newVector;
    }

    public void ApplyGravity(Vector3 direction, float speed)
    {
        if (player.CurrentState.UseGravity)
        {
            Debug.Log("sdfds");
            rb.AddForce(direction * speed, ForceMode.VelocityChange);
        }
    }
}
