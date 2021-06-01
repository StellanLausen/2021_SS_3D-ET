using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject PlayerController;
    private Transform playerPos;
    public LayerMask Ground, Player;


    private new Vector3 startPos;
    //States
    public float sightRange;
    public bool playerInSightRange;

    void Awake()
    {
        PlayerController playerController = PlayerController.GetComponent<PlayerController>();
        playerPos = PlayerController.transform;
        playerController.resetMap.AddListener(reset);
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        if (playerInSightRange) Chase();
    }

    private void Chase()
    {
        agent.SetDestination(playerPos.position);
    }

    private void reset()
    {
        //transform.position = startPos;
    }
}
