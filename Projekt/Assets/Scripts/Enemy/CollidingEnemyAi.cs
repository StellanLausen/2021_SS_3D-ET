using UnityEngine;
using UnityEngine.AI;

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
    public class CollidingEnemyAi : MonoBehaviour
    {
        //Reference
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform playerTransform;
        [SerializeField] public LayerMask isPlayer;

        //Reset
        private Vector3 startPos;
    
        //States
        [SerializeField] private float sightRange, enemySpeed;
        private bool playerInSightRange;
        
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

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            if (playerInSightRange)
            {
                Follow();
            }
        }
        private void Follow()
        {
            agent.SetDestination(playerTransform.position);
        }
        private void Reset()
        {
            agent.Warp(startPos);
        }
    }
}
