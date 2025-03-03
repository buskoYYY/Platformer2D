using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> InteractacleFounded;
    public event Action<MedKit> MedKidFounded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
            InteractacleFounded?.Invoke(interactable);

        if (collision.TryGetComponent(out IItem item))
        {
            if (item is MedKit mekit)
                MedKidFounded?.Invoke(mekit);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable _))
            InteractacleFounded?.Invoke(null);
    }
}
