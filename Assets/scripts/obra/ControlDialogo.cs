using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDialogo : MonoBehaviour
{
    public dialogo manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.controlDialogo();
        }
    }
}
