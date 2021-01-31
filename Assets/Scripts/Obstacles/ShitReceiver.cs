using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitReceiver : MonoBehaviour
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void DisabelBadCollider()
    {
        col.enabled = false;
    }
}
