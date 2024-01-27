using UnityEngine;

namespace Core.Games.GameName
{
    public class PipeCollideReset : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                player.transform.position = new(transform.position.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }
}
