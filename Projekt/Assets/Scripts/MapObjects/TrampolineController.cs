using UnityEngine;

namespace MapObjects
{
    public class TrampolineController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rbPlayer;
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
                rbPlayer.AddForce(0,jumpForce,0);
                shouldJump = false;
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player")) shouldJump = true;
        }
    }
}
