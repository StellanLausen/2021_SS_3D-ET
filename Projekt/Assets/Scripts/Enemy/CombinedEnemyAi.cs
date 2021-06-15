using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Enemy
{
    public class CombinedEnemyAi : MonoBehaviour
    {
        //Reference
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform playerTransform;
        [SerializeField] public LayerMask isPlayer;
        [SerializeField] private GameObject bullet;
    
        //Reset
        private Vector3 startPos;
    
        //States
        [SerializeField] private float sightRange, enemySpeed, secondsTillNextShoot, shootForce, destroyTime;
        private bool playerInSightRange, prevPlayerInSightRange;

        private void Awake()
        {
            playerController.resetMap.AddListener(Reset);
        
            agent = GetComponent<NavMeshAgent>();
            agent.speed = enemySpeed;
        }
        private void Start()
        {
            startPos = transform.position;
        }
        private void FixedUpdate()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            if (playerInSightRange)
            {
                Chase();

                if (!prevPlayerInSightRange)
                {
                    StartCoroutine(ShootCall());
                    prevPlayerInSightRange = true;
                }
            }
            else
            {
                prevPlayerInSightRange = false;
            }
        }
        private void Chase()
        {
            agent.SetDestination(playerTransform.position);
        }
        private IEnumerator ShootCall()
        {
            while (playerInSightRange)
            {
                yield return new WaitForSeconds(secondsTillNextShoot);
                Shoot();   
            }
        }
        private void Shoot()
        {
            var enemyPos = transform.position;
        
            //ShootDirection
            var shootDir = playerTransform.position - enemyPos;

            //Spawn Bullet 0.5f in front of Enemy
            var bulletSpawnPoint = enemyPos + (shootDir.normalized * 0.5f);

            //Instantiate Bullet
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnPoint, Quaternion.identity);
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        
            //Destroy Bullet
            Destroy(bulletInstance, destroyTime);

            //Shoot Bullet
            rb.AddForce(shootDir * shootForce, ForceMode.Impulse);
        }
        private void Reset()
        {
            agent.Warp(startPos);
        }
    }
}
