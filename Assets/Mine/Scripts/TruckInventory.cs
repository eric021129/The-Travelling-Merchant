using System.Collections.Generic;
using UnityEngine;

public class TruckInventory : MonoBehaviour
{
    public static TruckInventory Instance;

    private Dictionary<string, int> _stock = new Dictionary<string, int>();

    private void Awake()
    {
    Instance = this;
    Debug.Log($"TruckInventory.Awake() on {gameObject.name}");
    }

    public int GetCount(string itemID)
    {
        return _stock.TryGetValue(itemID, out int count) ? count : 0;
    }

    public void Add(string itemID, int amount = 1)
    {
        if (_stock.ContainsKey(itemID))
            _stock[itemID] += amount;
        else
            _stock[itemID] = amount;
    }

    public bool Remove(string itemID, int amount = 1)
    {
        if (!_stock.ContainsKey(itemID) || _stock[itemID] < amount)
            return false;

        _stock[itemID] -= amount;
        if (_stock[itemID] <= 0)
            _stock.Remove(itemID);

        return true;
    }

    // Get all items currently in stock (for Truck UI display)
    public IEnumerable<KeyValuePair<string, int>> GetAllItems()
    {
        return _stock;
    }
}