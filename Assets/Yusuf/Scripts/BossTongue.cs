using CoreGames.GameName.EventSystem;
using UnityEngine;

namespace Core.Games.GameName
{
    public class BossTongue : MonoBehaviour
    {
        [SerializeField] private int damageAmount;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventBus<SetDamageThePlayerEvent>.Emit(this, new SetDamageThePlayerEvent(damageAmount));
                Debug.Log($"Stick out attack is succesfull");
            }
        }
    }
}
