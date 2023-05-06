using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D _Rigid2D = new Rigidbody2D(); //YOk ARTIIK RIGIDBODY!!! @Han

    [Range(0, .3f)][SerializeField] private float _MovementSmoothing = .05f; //Daha Yumusak Gitmesini Sagliyo bu deger. @Han
    [SerializeField] private LayerMask _GroundLayers; //Ground Layerlari
    [SerializeField] private Transform m_GroundCheck; 


    private Vector3 _Velocity = Vector3.zero; //bu neden burda bilmiyom bosver bunu. @Han
    public bool _Grounded; //Karakterin Ground Layer  olan objelere Dokunup Dokunmadigini Gosteren Deger. @Han
    char _pastDirection = 'R';

    void Start()
    {
        _Rigid2D = GetComponent<Rigidbody2D>();
    }

	private void FixedUpdate()
	{
        bool wasGrounded = _Grounded;
        _Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.2f, _GroundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _Grounded = true;
            }
        }
    }

	public void Move(float moveSpeed)
	{
		if (_Grounded)
		{
            // Hedef Vecolityi bul. @Han
            Vector3 targetVelocity = new Vector2(moveSpeed * 10f, _Rigid2D.velocity.y);
            // Buldugun Velocitiyi SmoothDamp ile uygula. @Han
            _Rigid2D.velocity = Vector3.SmoothDamp(_Rigid2D.velocity, targetVelocity, ref _Velocity, _MovementSmoothing);

            //Gittigi yone gore karakteri cevir @Han
            if (moveSpeed > 0)
			{
				Flip('R');
			}else if (moveSpeed < 0)
			{
				Flip('L');
			}

		}else if (!_Grounded)
        {
            // Hedef Vecolityi bul. @Han
            Vector3 targetVelocity = new Vector2(moveSpeed * 10f, _Rigid2D.velocity.y);
            // Buldugun Velocitiyi SmoothDamp ile uygula. @Han
            _Rigid2D.velocity = Vector3.SmoothDamp(_Rigid2D.velocity, targetVelocity, ref _Velocity, _MovementSmoothing);

            //Gittigi yone gore karakteri cevir @Han
            if (moveSpeed > 0)
            {
                Flip('R');
            }
            else if (moveSpeed < 0)
            {
                Flip('L');
            }
        }
    }

    public void Jump(float JumpForce)
    {
        if (_Grounded)
        {
            _Rigid2D.AddForce(new Vector2(_Rigid2D.velocity.x, JumpForce));
        }
    }

    public void Flip(char direction)
    {
        // Karakterin X scale degerini tersine cevir. @Han

        if (direction == 'L')
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
        else if(direction == 'R')
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
        
    }
}
