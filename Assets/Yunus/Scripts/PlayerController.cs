using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Core.Games.GameName
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Move Stats")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxSpeed;

        private Rigidbody rb;
        private int pipeIndex;

        [Header("Change Pipe Stats")]
        [SerializeField] private Transform innerMesh;
        [SerializeField] private float changePipeWaitTime;
        [SerializeField] private float jumpUpSpeed;
        [SerializeField] private float jumpHorizontalSpeed;

        private bool canChangePipe;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            pipeIndex = 0;
            canChangePipe = true;
        }
        private void Update()
        {
            StartRunning();


            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangePipe(-1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                ChangePipe(1);
            }
        }

        private void StartRunning()
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(moveSpeed * Time.deltaTime * Vector3.forward);
            }
        }

        private void ChangePipe(int index)
        {
            if (!canChangePipe)
                return;

            pipeIndex += index;

            StopCoroutine(ChangePipeCD());
            StartCoroutine(ChangePipeCD());

            if (index > 0)
            {
                if (pipeIndex < 2)
                {
                    rb.AddForce(jumpUpSpeed * Vector3.up);

                    rb.AddForce(jumpHorizontalSpeed * Vector3.right);
                }
                else
                {
                    innerMesh.DOShakePosition(0.5f, 0.15f, 20);
                }
            }

            if (index < 0)
            {
                if (pipeIndex > -2)
                {
                    rb.AddForce(jumpUpSpeed * Vector3.up);

                    rb.AddForce(jumpHorizontalSpeed * Vector3.left);
                }
                else
                {
                    innerMesh.DOShakePosition(0.5f, 0.15f, 20);
                }
            }

            if (pipeIndex > 1) pipeIndex = 1;
            else if (pipeIndex < -1) pipeIndex = -1;
        }

        private IEnumerator ChangePipeCD()
        {
            canChangePipe = false;
            rb.constraints = RigidbodyConstraints.None;

            yield return new WaitForSeconds(changePipeWaitTime);

            canChangePipe = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
        }
    }
}