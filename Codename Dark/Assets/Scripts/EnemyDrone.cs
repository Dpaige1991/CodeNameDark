using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDrone : MonoBehaviour
{
    [Header("Enemy Drone Health and Damage")]
    private float enemyHealth = 120f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar healthBar;

    [Header("Enemy Drone Things")]
    public NavMeshAgent enemyAgent;
    public Transform lookPoint;
    public Camera shootingRaycastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Enemy Drone Guarding Var")]
    public GameObject[] walkPoints;
    int currentEnemyPosition = 0;
    public float enemySpeed;
    float walkingpointRadius = 2;

    [Header("Sounds And UI")]
    public AudioClip shootingSound;
    public AudioClip flameSound;
    public AudioSource audioSource;

    [Header("Enemy Drone Shooting Var")]
    public float timeBtwShoot;
    bool previouslyShoot;

    [Header("Enemy Drone Animation And Spark Effect")]
    public Animator anim;
    public ParticleSystem muzzleSpark;
    public ParticleSystem muzzleFlash;
    public ParticleSystem DestroyEffect;

    [Header("Enemy Drone mood/situation")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInvisionRadius;
    public bool playerInshootingRadius;

    // Start is called before the first frame update
    void Awake()
    {
        presentHealth = enemyHealth;
        playerBody = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInshootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);

        if (!playerInvisionRadius && !playerInshootingRadius)
        {
            Guard();
        }

        if (playerInvisionRadius && !playerInshootingRadius)
        {
            PursuePlayer();
        }

        if (playerInvisionRadius && playerInshootingRadius)
        {
            ShootPlayer();
        }
    }

    void Guard()
    {
        if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingpointRadius)
        {
            currentEnemyPosition = Random.Range(0, walkPoints.Length);
            if (currentEnemyPosition >= walkPoints.Length)
            {
                currentEnemyPosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);
        transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
    }

    void PursuePlayer()
    {
        if (enemyAgent.SetDestination(playerBody.position))
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", true);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", false);

            visionRadius = 30f;
            shootingRadius = 16f;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", true);
        }
    }

    void ShootPlayer()
    {
        enemyAgent.SetDestination(transform.position);

        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            muzzleSpark.Play();
            muzzleFlash.Play();
            audioSource.PlayOneShot(shootingSound);
            audioSource.PlayOneShot(flameSound);

            RaycastHit hit;

            if (Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("Shooting" + hit.transform.name);

                PlayerScript playerBody = hit.transform.GetComponent<PlayerScript>();

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }

                anim.SetBool("Shoot", true);
                anim.SetBool("Walk", false);
                anim.SetBool("AimRun", false);
                anim.SetBool("Die", false);
            }

            previouslyShoot = true;
            Invoke(nameof(ActiveShooting), timeBtwShoot);
        }
    }

    void ActiveShooting()
    {
        previouslyShoot = true;
    }

    public void enemyDroneHitDamage(float takeDamage)
    {
        presentHealth = takeDamage;
        healthBar.SetHealth(presentHealth);
        visionRadius = 40f;
        shootingRadius = 19f;

        if (presentHealth <= 0)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Shoot", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Die", true);

            enemyDie();
        }
    }

    private void enemyDie()
    {
        DestroyEffect.Play();
        enemyAgent.SetDestination(transform.position);
        enemySpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerInvisionRadius = false;
        playerInshootingRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
