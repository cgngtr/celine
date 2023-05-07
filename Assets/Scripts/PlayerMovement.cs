using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController _playerController;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    [Range(0, 1000f)][SerializeField] private float JumpPower = 400f;
    int JumpCount = 1;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpCount > 0) {
                _playerController.Jump(JumpPower);
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
