using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Obstacles
{
    public class AirObstacle : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

                //playerRb.ve
            }
        }
    }
}