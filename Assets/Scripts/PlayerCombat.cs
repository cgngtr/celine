using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    [Range(0, 5f)][SerializeField] private float _AttackRange = 1.4f; //mouseun icinde olupta attack yapabilecegii maksimum menzil
    [Range(0, 2f)][SerializeField] private float mouseSnapRange = 0.2f; //mouseun etrafindaki alan
    [Range(0, 2f)][SerializeField] private float deflectRange = 0.5f; //Deflect alani
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask bulletLayers;
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Deflect();
        }

    }
    public void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");
        //Bu attack range mouse bunun disindayken attack komutu calismiyor.
        Collider2D[] AttackRangeLimit = Physics2D.OverlapCircleAll(transform.position, _AttackRange);
        //buda mousein kendi icindeki alani attack yapmak icin. buyuk bir alan varki oyuncu biraz kenara bile tiklasa dusmana atak yapsin
        Collider2D[] enemiesInRangeOfMouse = Physics2D.OverlapCircleAll(worldPositionofMouse, mouseSnapRange, enemyLayers); // center, radius, checking layer @cag


        foreach (Collider2D collider in AttackRangeLimit)
        {
            //Burda hem rangein hemde mouse alaninin icinde mi diye kontrol ediyor.
            if (enemiesInRangeOfMouse.Contains(collider))
            {
                EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.GetHit(AttackDamage, this.gameObject.transform.position);
            }
        }

        if (AttackRangeLimit.Length <= 0) //Circle carpmazsa 
        {
            Debug.Log("No Enemies in Range");
        }
    }

    public void Deflect()
    {
        // Play a deflect animation
        //animator.SetTrigger("Deflect");
        //Bu attack range mouse bunun disindayken attack komutu calismiyor.
        Collider2D[] AttackRangeLimit = Physics2D.OverlapCircleAll(transform.position, _AttackRange);

        //buda mousein kendi icindeki alani attack yapmak icin. buyuk bir alan varki oyuncu biraz kenara bile tiklasa dusmana atak yapsin
        Collider2D[] bulletsInRangeOfMouse = Physics2D.OverlapCircleAll(GetCollusionPoint(worldPositionofMouse), deflectRange, bulletLayers); // center, radius, checking layer @cag

        foreach (Collider2D collider in AttackRangeLimit)
        {
            //Burda hem rangein hemde mouse alaninin icinde mi diye kontrol ediyor.
            if (bulletsInRangeOfMouse.Contains(collider))
            {
                EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.GetDeflect(AttackDamage, this.gameObject.transform.position);
            }
        }


        if (AttackRangeLimit.Length <= 0) //Circle carpmazsa 
        {
            Debug.Log("No Bullets in Range");
        }
    }

    private Vector2 GetCollusionPoint(Vector2 mousePositionWorld)
    {
        Vector2 center = transform.position;
        float distance = Mathf.Min(direction.magnitude, _AttackRange);

        Vector2 collisionPoint = center + direction.normalized * distance;

        return collisionPoint;
    }


        Vector2 direction = mousePositionWorld - center;
    private void OnDrawGizmos()
    {
        //burasi editorde attack range ve mouse range'i gormek icin kodlari bulunduruyor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _AttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPositionofMouse, mouseSnapRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GetCollusionPoint(worldPositionofMouse), mouseSnapRange);
    }
}
