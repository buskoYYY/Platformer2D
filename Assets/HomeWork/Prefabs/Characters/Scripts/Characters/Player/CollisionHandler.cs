using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> InteractacleFounded;
    public event Action<MedKit> MedKitFounded;
    public event Action<Key> KeyFounded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
            InteractacleFounded?.Invoke(interactable);

        if (collision.TryGetComponent(out MedKit mekit))
        {
                MedKitFounded?.Invoke(mekit);
        }
        if (collision.TryGetComponent(out Key key))
        {
                KeyFounded?.Invoke(key);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable _))
            InteractacleFounded?.Invoke(null);
    }
}
