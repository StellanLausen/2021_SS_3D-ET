using System.Collections;
using UnityEngine;

/*
Enemys:			Änderungen vorbehalten. Siehe in "Projekt Datei" für genaue Zeilenangaben.
Name Arbeit:	youtube Video Titel: 	FULL 3D ENEMY AI in 6 MINUTES! || Unity Tutorial
Name Autor:		youtube Channel name: 	Dave / GameDevelopment	
URL:			https://www.youtube.com/watch?v=UjkSFoLxesw&t=267s	
Abrufdatum:		01.06.2021
Lizenzmodel:	-
 */
namespace Enemy
{
    public class ShootingEnemyAi : MonoBehaviour
    {
        //source Enemy start -source code changed
        //Reference
        [SerializeField] private Transform playerTransform;
        [SerializeField] public LayerMask isPlayer;
        [SerializeField] private GameObject bullet;
    
        //States
        [SerializeField] private float sightRange, secondsTillNextShoot, shootForce;
        private float destroyTime = 5;
        private bool playerInSightRange, prevPlayerInSightRange;

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            if (playerInSightRange)
            {
                //source Enemy end
                //look in Player Direction
                transform.LookAt(playerTransform.position, Vector3.up);
                //source Enemy start -source code changed
                
                if (prevPlayerInSightRange) return;
                StartCoroutine(ShootCall());
                prevPlayerInSightRange = true;
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
            //source Enemy end
            var enemyPos = transform.position;
        
            //ShootDirection
            var shootDir = playerTransform.position - enemyPos;
            var shootDirNormalized = shootDir.normalized;

            //Spawn Bullet 0.5f in front of Enemy
            var bulletSpawnPoint = enemyPos + (shootDir.normalized * 0.5f);

            //Instantiate Bullet
            //source Enemy start -source code changed
            var bulletInstance = Instantiate(bullet, bulletSpawnPoint, Quaternion.identity);
            var rb = bulletInstance.GetComponent<Rigidbody>();
            //source Enemy end
            
            //Destroy Bullet
            Destroy(bulletInstance, destroyTime);

            //Shoot Bullet
            rb.AddForce(shootDirNormalized * shootForce, ForceMode.Impulse);
        }
    }
}
