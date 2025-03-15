public interface IInteractable
{
    public bool IsLock { get; }
    public Key Key { get; }
    public void Interact();
    public void Unlock(Key key);
}
