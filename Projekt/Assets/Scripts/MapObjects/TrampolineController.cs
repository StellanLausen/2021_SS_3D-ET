using UnityEngine;

namespace MapObjects
{
    public class TrampolineController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rbPlayerController;
        [SerializeField] private float jumpForce = 700;
        private bool shouldJump;
        private void Start()
        {
            shouldJump = false;
            jumpForce = 600;
        }
        private void FixedUpdate()
        {
            if (shouldJump)
            {
                rbPlayerController.AddForce(0,jumpForce,0);
                shouldJump = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) shouldJump = true;
        }
    }
}
