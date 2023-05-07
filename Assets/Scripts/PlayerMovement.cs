using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController _playerController;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    [Range(0, 1000f)][SerializeField] private float JumpPower = 400f;
    [SerializeField] private int JumpCount;
    [Range(0, 10)][SerializeField] private int _MaxJumpCount;
    

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        JumpCount = _MaxJumpCount;
    }

    void Update()
    {
        

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            if (_playerController._Grounded == true)
            {
                JumpCount = _MaxJumpCount;
                animator.SetBool("IsJumping", false);
            }

            if (JumpCount <= _MaxJumpCount - 1 && JumpCount > 0) {
                _playerController.Jump(JumpPower, true);
                JumpCount--;
            }
            else if(JumpCount > _MaxJumpCount - 1)
            {
                _playerController.Jump(JumpPower, false);
                JumpCount--;
            }
        }

    }

    private void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        _playerController.Move(horizontalMove * Time.deltaTime);
    }
}
