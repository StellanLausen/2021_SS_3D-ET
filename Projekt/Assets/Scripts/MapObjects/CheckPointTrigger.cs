using UnityEngine;

namespace MapObjects
{
   public class CheckPointTrigger : MonoBehaviour
   {
      [SerializeField] private int id;
      private void OnTriggerEnter(Collider other)
      {
         if(other.CompareTag("Player"))
         {
            EventSystem.Current.CheckpointEnter(id);
         }
      }
   }
}
