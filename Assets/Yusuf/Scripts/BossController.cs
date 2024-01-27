using System;
using System.Collections;
using CoreGames.GameName.EventSystem;
using CoreGames.GameName.Sound;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Games.GameName
{
    public class BossController : MonoBehaviour
    {
        [Header("Rotate to player")] 
        [SerializeField] private Transform player;
        [SerializeField] private float rotationDuration;

        [Header("Jump")] [SerializeField] private float jumpHeight;
        [SerializeField] private float gravityDuration;
        [SerializeField] private float groundLevel;
        [SerializeField] private int jumpDamageAmount;

        [Header("Stick Out")] [SerializeField] private Transform tongue;
        [SerializeField] private float maxLimit;
        [SerializeField] private float moveDuration;

        [Header("Health")] 
        public int health;
        private bool isDie;
        private bool canAttack;

        private Animator frogAnimator;
        private Collider coll;

        private void Awake()
        {
            frogAnimator = GetComponentInChildren<Animator>();
            coll = GetComponent<Collider>();
        }

        private void Start()
        {
            StartCoroutine(PlayAttacks());
        }

        private void OnEnable()
        {
            EventBus<SetValueOfCanAttackEnemyEvent>.AddListener(SetValueOfCanAttack);
        }

        private void OnDisable()
        {
            EventBus<SetValueOfCanAttackEnemyEvent>.RemoveListener(SetValueOfCanAttack);
        }

        private void Update()
        {
            if (canAttack)
            {
                StartCoroutine(PlayAttacks());
                canAttack = false;
            }
        }

        private IEnumerator PlayAttacks()
        {
            while (!isDie)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                int randomValue = Random.Range(0, 2);

                switch (randomValue)
                {
                    case 0:
                        JumpAndFallAnimation();
                        break;
                    case 1:
                        StartCoroutine(nameof(StickOutAttack));
                        break;
                }
                
                yield return new WaitForSeconds(3f);
            }
        }

        private void RotateToPlayer()
        {
            transform.DOLookAt(player.position, rotationDuration).SetEase(Ease.OutQuad).OnComplete(() =>
                Debug.Log($"Rotation completed"));
        }

        private void JumpAndFallAnimation()
        {
            frogAnimator.SetTrigger("Jump");
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
        
        private IEnumerator StickOutAttack()
        {
            RotateToPlayer();
            yield return new WaitForSeconds(rotationDuration);
            StickOut();
        }

        private void SetValueOfCanAttack(object sender, SetValueOfCanAttackEnemyEvent e)
        {
            canAttack = e.canAttack;
        }

        public void DecreaseHealth()
        {
            health -= 25;

            if (health <= 0)
            {
                isDie = true;
                coll.enabled = false;
                frogAnimator.SetTrigger("Die");
                SoundManager.instance.PlayOneShot(FMODEvents.instance.sfx_frogLaugh, transform.position);
                EventBus<FinishEvent>.Emit(this, new FinishEvent());
            }
        }
    }
}