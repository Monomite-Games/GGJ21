using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonShitRemover : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3.0f);

            Destroy(gameObject);
        }
    }
}