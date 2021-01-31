using System.Collections;
using UnityEngine;

using Palomas.Requests;

namespace Palomas.Pigeon
{
    public class ShitController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;

        private bool hasImpacted = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ShitReceiver shitReceiver = collision.GetComponent<ShitReceiver>();
            if (!collision.CompareTag("Player") && shitReceiver != null)
            {
                hasImpacted = true;
                col.isTrigger = false;

                shitReceiver.DisabelBadCollider();

                StartCoroutine(Delete());
            }
        }

        private void Update()
        {
            if (hasImpacted)
            {
                rb.velocity = Vector3.zero;
            }
        }

        private IEnumerator Delete()
        {
            yield return new WaitForSeconds(1f);

            Destroy(this.gameObject);
        }
    }
}