using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    [Range(0, 5f)][SerializeField] private float _AttackRange = 1.4f; //mouseun icinde olupta attack yapabilecegii maksimum menzil
    [Range(0, 2f)][SerializeField] private float mouseSnapRange = 0.2f; //mouseun etrafindaki alan
    [SerializeField] private LayerMask MouseRange;
    [SerializeField] private LayerMask _EnemyLayers;
    [Range(0, 10)][SerializeField] private int AttackDamage;

    public Vector3 worldPositionofMouse;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {


        // Get the position of the mouse in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Set the z coordinate to the distance from the camera to the game object
        mousePosition.z = -Camera.main.transform.position.z; // amacini anlamadim @cag

        // Convert the position from screen space to world space
        worldPositionofMouse = Camera.main.ScreenToWorldPoint(mousePosition);



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

    }
    public void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        //Bu attack range mouse bunun disindayken attack komutu calismiyor.
        Collider2D[] AttackRangeLimit = Physics2D.OverlapCircleAll(transform.position, _AttackRange);
        //buda mousein kendi icindeki alani attack yapmak icin. buyuk bir alan varki oyuncu biraz kenara bile tiklasa dusmana atak yapsin
        Collider2D[] enemiesInRangeOfMouse = Physics2D.OverlapCircleAll(worldPositionofMouse, mouseSnapRange, _EnemyLayers); // center, radius, checking layer @cag


        foreach (Collider2D collider in AttackRangeLimit)
        {
            //Burda hem rangein hemde mouse alaninin icinde mi diye kontrol ediyor.
            if (enemiesInRangeOfMouse.Contains(collider))
            {
                EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.GetHit(AttackDamage, this.gameObject);
            }
        }

        if (AttackRangeLimit.Length <= 0) //Circle carpmazsa 
        {
            Debug.Log("No Enemies in Range");
        }
    }

    private Vector2 GetCollusionPoint(Vector2 mousePosition)
    {
        Vector2 center = transform.position;
        Vector2 direction = mousePosition - center;
        float distance = Mathf.Min(direction.magnitude, _AttackRange);

        Vector2 collisionPoint = center + direction.normalized * distance;

        return collisionPoint;
    }


    private void OnDrawGizmos()
    {
        //burasi editorde attack range ve mouse rangi gormek icin kodlari bulunduruyor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _AttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPositionofMouse, mouseSnapRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GetCollusionPoint(worldPositionofMouse), mouseSnapRange);
    }
}
