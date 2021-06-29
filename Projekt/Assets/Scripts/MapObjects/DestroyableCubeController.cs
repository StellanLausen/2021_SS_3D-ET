using UnityEngine;

namespace MapObjects
{
    public class DestroyableCubeController : MonoBehaviour
    {
        [SerializeField] private GameObject destroyedCube;
        private void Start()
        {
            //set Child of destroyedCube inactive
            for (var i = 0; i <= 7; i++)
            {
                destroyedCube.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                ActivateObj();
                DestroyObj();
            }
        }
        private void DestroyObj()
        { 
            //Destroy the show-Cube
            Destroy(gameObject);
        }
        private void ActivateObj()
        {
            //set Child of destroyedCube active
            for (int i = 0; i <= 7; i++)
            {
                destroyedCube.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
