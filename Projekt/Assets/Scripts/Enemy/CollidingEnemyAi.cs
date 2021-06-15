using System;
using UnityEngine;
using UnityEngine.AI;

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
                Chase();
            }
        }
        private void Chase()
        {
            agent.SetDestination(playerTransform.position);
        }
        private void Reset()
        {
            agent.Warp(startPos);
        }
    }
}
