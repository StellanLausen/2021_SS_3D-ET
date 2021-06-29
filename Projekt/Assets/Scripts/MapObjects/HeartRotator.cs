using UnityEngine;

namespace MapObjects
{
    public class HeartRotator : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(new Vector3(0,0,25) * Time.deltaTime);
        }
    }
}
