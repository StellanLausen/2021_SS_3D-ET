using UnityEngine;

namespace MapObjects
{
    public class CoinRotator : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(new Vector3(0,25,0) * Time.deltaTime);
        }
    }
}
