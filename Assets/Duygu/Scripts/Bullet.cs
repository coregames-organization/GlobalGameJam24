using System;
using System.Collections;
using System.Collections.Generic;
using CoreGames.GameName.Sound;
using UnityEngine;

namespace Core.Games.GameName
{
    public class Bullet : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 4);
        }

        private void Start()
        {
            SoundManager.instance.PlayOneShot(FMODEvents.instance.sfx_ha,transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<BossController>().DecreaseHealth();
            }
        }
    }
}
