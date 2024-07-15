using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    public Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;
    public float aiDistance;
    public GameObject hideText, stopHideText;

    //Enemy RayCast
    public int numRayCasts = 3;
    public float angleBetweenRayCasts = 30f;

    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    AudioLevel1 audioLevel1;

    private void Awake()
    {
        audioLevel1 = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioLevel1>();
    }

    void Start()
    {
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
        playerHealth = player.GetComponent<PlayerHealth>(); // Get the PlayerHealth script from the player
    }

    void Update()
    {
        Vector3 forwardDirection = transform.forward;
        for (int i = 0; i < numRayCasts; i++)
        {
            Vector3 direction = Quaternion.Euler(0, -angleBetweenRayCasts * (numRayCasts - 1) / 2f + angleBetweenRayCasts * i, 0) * forwardDirection;

            RaycastHit hit;
            aiDistance = Vector3.Distance(player.position, transform.position);

            if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    walking = false;
                    StopCoroutine("stayIdle");
                    StopCoroutine("chaseRoutine");
                    StartCoroutine("chaseRoutine");
                    chasing = true;
                    break;
                }
                else if (hit.collider.gameObject.tag == "InteractiveObject")
                {
                    FindNewDestination();
                    break;
                }
            }
        }

        if (chasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("sprint");

            if (aiDistance <= catchDistance)
            {
                player.gameObject.SetActive(false);
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                hideText.SetActive(false);
                stopHideText.SetActive(false);
                aiAnim.ResetTrigger("sprint");
                aiAnim.SetTrigger("jumpscare");
                StartCoroutine(deathRoutine());
                chasing = false;
            }
        }

        if (walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    public void stopChase()
    {
        walking = true;
        chasing = false;
        StopCoroutine("chaseCoroutine");
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
        else
        {
            Debug.LogError("PlayerHealth component not found on player");
        }
    }

    void FindNewDestination()
    {
        NavMeshPath newPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, currentDest.position, NavMesh.AllAreas, newPath);

        for (int i = 0; i < newPath.corners.Length - 1; i++)
        {
            RaycastHit hit;
            Vector3 cornerDirection = newPath.corners[i + 1] - newPath.corners[i];

            if (Physics.Raycast(newPath.corners[i], cornerDirection, out hit, cornerDirection.magnitude))
            {
                if (hit.collider.gameObject.tag == "InteractiveObject")
                {
                    currentDest = destinations[Random.Range(0, destinations.Count)];
                    break;
                }
            }
        }
    }
}