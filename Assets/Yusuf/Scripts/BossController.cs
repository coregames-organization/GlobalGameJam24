using CoreGames.GameName.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace Core.Games.GameName
{
    public class BossController : MonoBehaviour
    {
        [Header("Rotate to player")] [SerializeField]
        private Transform player;

        [SerializeField] private float rotationDuration;

        [Header("Jump")] [SerializeField] private float jumpHeight;
        [SerializeField] private float gravityDuration;
        [SerializeField] private float groundLevel;
        [SerializeField] private int jumpDamageAmount;

        [Header("Stick Out")] [SerializeField] private Transform tongue;
        [SerializeField] private float maxLimit;
        [SerializeField] private float moveDuration;

        private void Start()
        {
            InvokeRepeating(nameof(StickOut), 0f, 2f);
        }

        private void RotateToPlayer()
        {
            transform.DOLookAt(player.position, rotationDuration).SetEase(Ease.OutQuad).OnComplete(() =>
                Debug.Log($"Rotation completed"));
        }

        private void JumpAndFallAnimation()
        {
            transform.DOMoveY(jumpHeight, gravityDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                transform.DOMoveY(groundLevel, gravityDuration).SetEase(Ease.InQuad).OnComplete(() =>
                    Debug.Log($"Boss is falled"));
                EventBus<SetDamageThePlayerEvent>.Emit(this, new SetDamageThePlayerEvent(jumpDamageAmount));
            });
        }

        private void StickOut()
        {
            float initialZValue = tongue.localScale.z;

            tongue.DOScaleZ(maxLimit, moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                tongue.DOScaleZ(initialZValue, moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    Debug.Log($"Stick out completed");
                });
            });
        }
    }
}