using UnityEngine;



public class StorehouseUI : MonoBehaviour
{
    public ItemCatalog catalog;
    public Transform contentParent;     // the Content inside the Scroll View
    public StorehouseItemTile tilePrefab;

    private bool _initialized = false;

    
    private void OnEnable()
    {
        if (!_initialized)
        {
            BuildGrid();
            _initialized = true;
        }
        else
        {
            // Refresh existing tiles when reopening
            foreach (Transform child in contentParent)
            {
                StorehouseItemTile tile = child.GetComponent<StorehouseItemTile>();
                if (tile != null) tile.SendMessage("UpdateUI");
            }
        }
    }

    private void BuildGrid()
    {
        Debug.Log($"Building grid with {catalog.items.Length} items");
        foreach (var item in catalog.items)
        {
            Debug.Log($"Creating tile for: {item.itemName}");
            StorehouseItemTile tile = Instantiate(tilePrefab, contentParent);
            tile.Setup(item);
        }
    }
}