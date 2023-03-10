using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_RUNNING = "isRunning";
    private const string IS_HOLDING = "isHolding";  

    [SerializeField] private PlayerController playerController;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool(IS_HOLDING, playerController.IsHolding());
        animator.SetBool(IS_RUNNING, playerController.IsRunning());


    }
}
