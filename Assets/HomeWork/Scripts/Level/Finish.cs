using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    [SerializeField] public Door[] _doors;
    public void Interact()
    {
        if (_doors.All(i => i.IsActive))
        {
            gameObject.SetActive(false);
        }
    }
}
