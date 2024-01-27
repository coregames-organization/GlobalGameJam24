using System;
using System.Collections;
using System.Collections.Generic;
using Smart;
using UnityEngine;

namespace Core.Games.GameName
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private GameObject shield;

        private bool isShieldActive = false;
        private PlayerController playerController;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            shield.SetActive(false);
            Debug.Log("Shield is not active");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isShieldActive && playerController.attackMode)
                {
                    ActiveShield();
                    Debug.Log("Shield is active");
                }
            }
        }

        private void ActiveShield()
        {
            isShieldActive = !isShieldActive;
            shield.SetActive(isShieldActive);

            if (isShieldActive)
            {
                Invoke("DeactivateShield", 5f);
            }
        }

        private void DeactivateShield()
        {
            isShieldActive = !isShieldActive;
            shield.SetActive(false);
        }
    }
}
