using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator animator;

    #region Horizontal Movement Values
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    #endregion

    #region JumpValues
    [Range(0, 1000f)][SerializeField] private float JumpPower = 400f;
    [SerializeField] private int JumpCount;
    [Range(0, 10)][SerializeField] private int _MaxJumpCount;
    #endregion



    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        JumpCount = _MaxJumpCount;
    }

    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            if (_playerController._Grounded == true)
            {
                JumpCount = _MaxJumpCount;
                animator.SetBool("IsJumping", false);
            }

            if (JumpCount <= _MaxJumpCount - 1 && JumpCount > 0)
            {
                _playerController.Jump(JumpPower, true);
                JumpCount--;
            }
            else if (JumpCount > _MaxJumpCount - 1)
            {
                _playerController.Jump(JumpPower, false);
                JumpCount--;
            }
        }
    }

    private void FixedUpdate()
    {
        // bu kodun animasyon manager adli scriptin icinde bulunmasi gerekiyor
        // | | |
        // V V V
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); // hiz - olunca mal olmamasi icin mathf'li @cag 
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.deltaTime;
        _playerController.Move(horizontalMove);
    }
    
}
