using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TruckUI : MonoBehaviour
{
    public static TruckUI Instance;

    public ItemCatalog catalog;
    public Transform contentParent;
    public TruckItemTile tilePrefab;

    [Header("Held Section")]
    public Image heldIcon;
    public TextMeshProUGUI heldText;

    private List<TruckItemTile> _tiles = new List<TruckItemTile>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        BuildTiles();
        RefreshAllTiles();
        RefreshHeldSection();
    }

private void BuildTiles()
{
    Debug.Log("=== TruckUI BuildTiles called ===");

    foreach (var tile in _tiles)
    {
        if (tile != null) Destroy(tile.gameObject);
    }
    _tiles.Clear();

    if (TruckInventory.Instance == null)
    {
        Debug.LogError("TruckInventory.Instance is null!");
        return;
    }

    if (catalog == null)
    {
        Debug.LogError("Catalog is null!");
        return;
    }

    foreach (var item in catalog.items)
    {
        int count = TruckInventory.Instance.GetCount(item.itemID);
        if (count > 0)
        {
            Debug.Log($"Creating truck tile for {item.itemID} (count: {count})");
            TruckItemTile tile = Instantiate(tilePrefab, contentParent);
            tile.Setup(item);
            _tiles.Add(tile);
        }
    }

    Debug.Log($"Total tiles created: {_tiles.Count}");
}

    public void RefreshAllTiles()
    {
        // Some tiles may need to be removed (count became 0) — easiest: rebuild
        BuildTiles();
        RefreshHeldSection();
    }

    private void RefreshHeldSection()
    {
        if (PlayerState.Instance.IsCarrying)
        {
            string heldID = PlayerState.Instance.HeldItemID;
            ItemData item = null;
            foreach (var i in catalog.items)
            {
                if (i.itemID == heldID) { item = i; break; }
            }

            if (item != null)
            {
                heldIcon.enabled = true;
                heldIcon.sprite = item.icon;
                heldText.text = $"{item.itemName} x{PlayerState.Instance.HeldItemCount}";
            }
        }
        else
        {
            heldIcon.enabled = false;
            heldText.text = "Nothing";
        }
    }
}