using System.Data.Common;
using Cinemachine.Utility;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class StaticEnemy : MonoBehaviour
    {
        private Vector3 startPos;
        private Vector3 finishPos;

        private bool active = false;
        private float speed;

        [SerializeField] private float moveToX, moveToY, moveToZ;
    
        private void Start()
        {
            startPos = transform.position;
            speed = Random.Range(0.4f, 1f);
            finishPos = new Vector3(startPos.x - moveToX, startPos.y - moveToY, startPos.z - moveToZ);
        }

        private void Update()
        {
            switch (active)
            {
                case false :
                    transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                    break;
                case true :
                    transform.position= Vector3.MoveTowards(transform.position, finishPos,speed * Time.deltaTime);
                    break;
            }

            if (transform.position == startPos)
            {
                active = true;
            }else if (transform.position == finishPos)
            {
                active = false;
            }
            
        }
    }
}
