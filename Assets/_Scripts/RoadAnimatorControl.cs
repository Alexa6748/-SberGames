using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadAnimatorControl : MonoBehaviour
{
    Animator animator;
    [SerializeField] string boolName;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(boolName, false);
    }

    public void StartAnimation()
    {
        animator.SetBool(boolName, true);
    }
}
}
