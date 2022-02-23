using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotMachineDestroyer : MonoBehaviour
{
    public UnityAction<Slot> SlotCollided;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Slot slot))
        {
            SlotCollided?.Invoke(slot);
            Destroy(slot.gameObject);
        }
    }
}
