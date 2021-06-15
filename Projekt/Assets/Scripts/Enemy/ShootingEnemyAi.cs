using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class ShootingEnemyAi : MonoBehaviour
    {
        //Reference
        [SerializeField] private Transform playerTransform;
        [SerializeField] public LayerMask isPlayer;
        [SerializeField] private GameObject bullet;
    
        //States
        [SerializeField] private float sightRange, secondsTillNextShoot, shootForce, destroyTime;
        private bool playerInSightRange, prevPlayerInSightRange;
    
        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            if (playerInSightRange)
            {

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
    }
}
