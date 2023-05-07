using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController _playerController;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    [Range(0, 1000f)][SerializeField] private float JumpPower = 400f;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _playerController.Jump(JumpPower);
        }

    }

    private void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _playerController.Move(horizontalMove * Time.deltaTime);
    }
}
