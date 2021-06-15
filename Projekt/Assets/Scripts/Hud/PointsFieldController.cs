using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsFieldController : MonoBehaviour
{

    private Text textCompPoints;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        textCompPoints = GetComponent<Text>();
        
        gameManager.pointsChanged.AddListener(UpdatePoints);
        textCompPoints.text = gameManager.Points.ToString();
    }

    private void UpdatePoints()
    {
        textCompPoints.text = gameManager.Points.ToString();
    }
}
