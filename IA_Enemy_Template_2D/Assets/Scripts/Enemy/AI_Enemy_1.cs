using UnityEngine;

[RequireComponent(typeof(Waypoints))]
public class AI_Enemy_1 : MonoBehaviour
{
    [Header("---- GENERAL ATTRIBUTES ----")] 
    [SerializeField]
    private float speed;
    
    [Space]
    [Header("--- PLAYER LAYER MASK ----")] 
    [SerializeField, Tooltip("Layer mask que tendrá que tener el player para poder detectarlo si se acerca" +
                             " lo suficiente al enemigo")]
    private LayerMask playerMask;
    
    [Space]
    [Header("---- RANGES ----")]
    [SerializeField, Tooltip("Rango de visión del enemigo")] 
    private float viewRange;
    [SerializeField, Tooltip("Rango de ataque del enemigo")] 
    private float attackRange;

    [Space]
    [Header("---- ATTACK ATTRIBUTES ---")] 
    [SerializeField, Tooltip("Tiempo entre ataques")]
    private float timeBetweenAttacks;
    [SerializeField, Tooltip("Daño del enemigo que causará al atacar al jugador")]
    private float damage;
    
    // Private variables
    private Transform player;
    private Waypoints _waypoints;
    private Vector3 targetWaypoint;
    private bool playerInViewRange;
    private bool playerInAttackRange;
    private bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        _waypoints = GetComponent<Waypoints>();
        targetWaypoint = _waypoints.CurrentWaypoint;
    }

    void Update()
    {
        // Revisamos si el enemigo está colisionando con el player dependiendo de los rangos de visión y ataque
        playerInViewRange = Physics2D.OverlapCircle(transform.position, viewRange, playerMask);
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerMask);
        
        // Si el player no está ni en rango de visión ni en rango de ataque el enemigo patrullará
        if (!playerInViewRange && !playerInAttackRange)
        {
            Patrolling();
        }
        
        
        // Si el player no está en rango de ataque pero si que está en rango de visón el enemigo lo perseguirá
        if (playerInViewRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        
        // Si el player está en rango de ataque y por lo tanto en rango de visión el enemigo atacará
        if (playerInViewRange && playerInAttackRange)
        {
            AttackPlayer();
        }
        
        
    }


    /// <summary>
    /// Método para seguir los puntos de ruta del enemigo
    /// </summary>
    private void Patrolling()
    {
        // Cuando la distancia entre el enemigo y su waypoint objetivo es muy pequeña, pasará a tener un nuevo waypoint
        if (Vector2.Distance(transform.position, targetWaypoint) < 0.01f)
        {
            targetWaypoint = _waypoints.GetNextWayPoint();
        }
        
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
    }

    
    /// <summary>
    /// Método para seguir al jugador
    /// </summary>
    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.localPosition, player.position, speed * Time.deltaTime);
    }

    
    private void AttackPlayer()
    {
        Debug.Log("ATTACK");

    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
