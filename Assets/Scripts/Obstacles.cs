using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Obstacles
{
    public class Obstacles : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameEvents.OnLifeLost();
            }
        }
    }
}