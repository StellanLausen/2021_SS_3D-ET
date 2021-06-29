using UnityEngine;

namespace Enemy
{
    public class StaticEnemy : MonoBehaviour
    {
        [SerializeField] private float moveToX, moveToY, moveToZ;

        private Vector3 startPos, finishPos;
        private bool active = false;
        private float speed;

        private void Start()
        {
            startPos = transform.position;
            speed = Random.Range(0.4f, 1f);
            finishPos = new Vector3(startPos.x - moveToX, startPos.y - moveToY, startPos.z - moveToZ);
        }

        private void Update()
        {
            var transformPos = transform.position;
            transform.position = active switch
            {
                false => Vector3.MoveTowards(transformPos, startPos, speed * Time.deltaTime),
                true => Vector3.MoveTowards(transformPos, finishPos, speed * Time.deltaTime)
            };

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
