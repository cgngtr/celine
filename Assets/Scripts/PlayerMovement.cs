using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController _playerController;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    [Range(0, 1000f)][SerializeField] private float JumpPower = 400f;
    private int JumpCount;
    [Range(0, 1000f)][SerializeField] private int _MaxJumpCount = 2;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        JumpCount = _MaxJumpCount;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_playerController._Grounded == true)
            {
                JumpCount = _MaxJumpCount;
            }

            if (JumpCount <= _MaxJumpCount - 1) {
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
        _playerController.Move(horizontalMove * Time.deltaTime);
    }
}
