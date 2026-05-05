public interface IInteractable
{
    string GetPrompt();          // e.g. "Press E to buy"
    KeyType GetKey();             // which key triggers it
    void Interact();
}

public enum KeyType
{
    E,
    R
}