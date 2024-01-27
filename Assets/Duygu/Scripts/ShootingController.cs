using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Games.GameName
{
    public class ShootingController : MonoBehaviour
    {

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Vector3 resetPosition;

        [SerializeField] float bulletSpeed = 50f;

        private PlayerController playerController;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }
        
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (playerController.attackMode)
                {
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * bulletSpeed;
                }
            }
        }

        public void ResetPosition()
        {
            //bulletSpawnPoint.transform.localPosition = resetPosition;
        }
    }
}
