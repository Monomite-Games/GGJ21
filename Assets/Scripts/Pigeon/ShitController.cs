using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class ShitController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;

        private bool hasImpacted = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
            {
                hasImpacted = true;
                col.isTrigger = false;
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