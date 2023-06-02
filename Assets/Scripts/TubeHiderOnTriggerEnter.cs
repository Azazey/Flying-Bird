using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeHiderOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Tube"))
        {
            collider.GetComponentInParent<Tube>().gameObject.SetActive(false);
        }
    }
}
