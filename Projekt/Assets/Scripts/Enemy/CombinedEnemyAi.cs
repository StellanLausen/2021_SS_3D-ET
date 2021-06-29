using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Enemy
{
    public class CombinedEnemyAi : MonoBehaviour
    {
        //source Enemy start -source code changed
        //Reference
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform playerTransform;
        [SerializeField] public LayerMask isPlayer;
        [SerializeField] private GameObject hittingBullet;
    
        //Reset
        private Vector3 startPos;
    
        //States
        [SerializeField] private float sightRange, enemySpeed, secondsTillNextShoot, shootForce, destroyTime;
        private bool playerInSightRange, prevPlayerInSightRange;
        //source Enemy end
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
            //source Enemy start -source code changed
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            if (playerInSightRange)
            {
                //Chase the Player
                Follow();
                //look in Player Direction
                transform.LookAt(playerTransform.position, Vector3.up);

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
        private void Follow()
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
            //source Enemy end
            var enemyPos = transform.position;
        
            //ShootDirection
            var shootDir = playerTransform.position - enemyPos;
            var shootDirNormalized = shootDir.normalized;
            
            //Spawn Bullet 0.5f in front of Enemy
            var bulletSpawnPoint = enemyPos + (shootDirNormalized * 0.5f);

            //Instantiate Bullet
            //source Enemy start -source code changed
            var bulletInstance = Instantiate(hittingBullet, bulletSpawnPoint, Quaternion.identity);
            var rb = bulletInstance.GetComponent<Rigidbody>();
            //source Enemy end
            
            //Destroy Bullet
            Destroy(bulletInstance, destroyTime);

            //Shoot Bullet
            rb.AddForce(shootDirNormalized * shootForce, ForceMode.Impulse);
        }
        private void Reset()
        {
            agent.Warp(startPos);
        }
    }
}
