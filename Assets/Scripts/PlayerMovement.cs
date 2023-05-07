using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController _playerController;
    public Animator animator;
    public Rigidbody2D rb;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    public float KBForce; // KB kuvveti @cag
    public float KBCounter; // KB efektinin ne kadar suresinin kaldigi @cag
    public float KBTotalTime; // KB efektinin ne kadar surecegi @cag
    public bool KnockFromRight; // knock'in alindigi yeri soyleyen bool @cag
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
        if(KBCounter <= 0) // sadece bu sayac bittiginde tekrar hareket edilebilecek
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.deltaTime;
        }
        else
        {
            if(KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(!KnockFromRight)
            {
                rb.velocity = new Vector2(KBForce, -KBForce);
            }
            KBCounter -= Time.deltaTime; // counting down @cag
        }

         // delta time'i buraya aldim cunku
                                                                                     // horizontal move'u kullanmam lazim @ cag
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); // hiz - olunca mal olmamasi icin mathf'li @cag
        _playerController.Move(horizontalMove);
    }
}
