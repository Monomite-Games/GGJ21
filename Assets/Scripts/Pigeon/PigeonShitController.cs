using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonShitController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
            {
                rb.isKinematic = true;
                col.enabled = false;
                StartCoroutine(Delete());
            }
        }

        private IEnumerator Delete()
        {
            yield return new WaitForSeconds(2f);

            Destroy(this.gameObject);
        }
    }
}