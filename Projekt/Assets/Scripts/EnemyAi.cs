using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject PlayerController;
    
    private Transform playerTransform;
    public LayerMask Player;


    private new Vector3 startPos;
    
    //States
    public float sightRange;
    public bool playerInSightRange;

    void Awake()
    {
        PlayerController playerController = PlayerController.GetComponent<PlayerController>();
        playerTransform = PlayerController.transform;
        
        playerController.resetMap.AddListener(reset);
        playerController.LevelFinished.AddListener(reset);
        
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        if (playerInSightRange) Chase();
    }

    private void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void reset()
    {
        agent.Warp(startPos);
    }
}
