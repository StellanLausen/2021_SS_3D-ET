using UnityEngine;

namespace MapObjects
{
    public class BridgeScript : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private bool active = false;

        private readonly Quaternion finishRotation = new Quaternion(0, 0, 0, 1);
        private Quaternion startRotation;

        private void Start()
        {
            EventSystem.Current.ONSwitchChange += ChangeActive;
            startRotation = transform.rotation;
            
            playerController.resetMap.AddListener(Reset);
        }

        private void Update()
        {
            switch (active)
            {
                case true:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, finishRotation, 10 * Time.deltaTime);
                    break;
                case false:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, 10 * Time.deltaTime);
                    break;
            }
        }
        
        private void ChangeActive(int id)
        {
            if (id == 1) active = !active;
        }

        private void Reset()
        {
            transform.rotation = startRotation;
            active = false;
        }
    }
}
