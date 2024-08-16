using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OdysseusBehavior : MonoBehaviour
{
    public enum FSMStates {
        Patrol,
        Chase,
        Attack
    }
    public GameObject battlePrompt;
    public float attackDistance = 5;
    public float chaseDistance = 10;
    public float patrolSpeed = 0;
    public float chaseSpeed;

    public Transform enemyEyes;
    public float fieldOfView = 90f;

    UnityEngine.AI.NavMeshAgent agent;
    FSMStates currentState;
    GameObject player;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    Animator anim;
    float distanceToPlayer;

    void Awake() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState) {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
        }
    }

    void Initialize() {
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdatePatrolState() {
        print("Patrolling!");
        // Walking animation
        anim.SetBool("isWalking", true);

        if (agent == null) {
            Debug.Log("ERROR");
        }

        agent.stoppingDistance = 1;
        agent.speed = patrolSpeed;

        if (Vector3.Distance(transform.position, nextDestination) < 1) {
            FindNextPoint();
        } else if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV()) {
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        
        agent.SetDestination(nextDestination);
    }

    void UpdateChaseState() {
        Debug.Log("Chasing!");
        anim.SetBool("isWalking", true);
        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;
        agent.speed = chaseSpeed;

        if (distanceToPlayer <= attackDistance) {
            currentState = FSMStates.Attack;
        } else if (distanceToPlayer > chaseDistance) {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState() {
        Debug.Log("Attack");
        anim.SetBool("isWalking", false);
        nextDestination = player.transform.position;
        agent.speed = 0;

        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance) {
            currentState = FSMStates.Attack;
        } else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance) {
            currentState = FSMStates.Chase;
        } else if (distanceToPlayer > chaseDistance) {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);

        battlePrompt.SetActive(true);
        Invoke("CallBattleWon", 3);
    }

    // Done this way because you cannot call invoke directly on LevelManager.BattleWon since it's a static method
    void CallBattleWon() {
        LevelManager.BattleWon();
    }

    void FindNextPoint() {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target) {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
 
    /*private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        SceneView.RepaintAll();

        // Field of view
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        // 22.5 degrees to the left
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }*/

    bool IsPlayerInClearFOV() {
        RaycastHit hit;

        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView * 0.5f) {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance)) {
                if (hit.collider.CompareTag("Player")) {
                    print("Player in sight!");
                    return true;
                }
            }
        }
        return false;
    }
}
