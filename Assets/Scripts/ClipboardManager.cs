using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Creates rows in the clipboard UI, reduces health meter above and switches between the two pages of the clipboard UI
public class ClipboardManager : MonoBehaviour
{
    // Singleton instance of ClipboardManager
    public static ClipboardManager Instance;

    // Row prefab containing the item icon, count, and checkmark
    [SerializeField] private GameObject _clipboardItemPrefab;
    // Array of sprites for the checkmarks in the clipboard UI
    [SerializeField] private Sprite[] _clipboardItemSprites;
    // Reference to the health meter GameObject in the clipboard UI
    [SerializeField] private GameObject _clipBoardHealth;
    // Reference to the second page of the clipboard UI (inactive by default)
    [SerializeField] private GameObject _clipboardPage2;
    // Container for the list of items in the clipboard UI
    [SerializeField] public GameObject itemListContainer;

    // Current page of the clipboard UI, starts at 1
    private int _currentPage = 1;

    // Awake to create a singleton instance
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    // Function to remove a heart from the health meter in the clipboard UI
    public void ReduceHealth()
    {
        if (_clipBoardHealth.transform.childCount > 0)
        {
            Destroy(_clipBoardHealth.transform.GetChild(0).gameObject);
        }
    }

    // Function that creats a row in the clipboard UI for a given item
    // Parameters: item = item to be displayed, count = how many instances of the item exist in the scene to be picked up, check = how many of this item have been picked up, done = whether all instances of the item have been picked up
    public void setupClipboard(PickupItemClass item, int count, int check, bool done)
    {
        // Creates a new row and attaches it to the itemListContainer
        GameObject gameObject = Instantiate(_clipboardItemPrefab
        , this.transform);
        gameObject.transform.SetParent(itemListContainer.transform, false);

        // gets the icon, count, and checkmark UI elements from the new row
        var iconContainer = gameObject.transform.Find("Icon");
        var countContainer = gameObject.transform.Find("Count");
        var checkContainer = gameObject.transform.Find("Check");

        // Sets the icon, count, and checkmark sprites/text based on the item and its state
        if (iconContainer != null)
            iconContainer.GetComponent<Image>().sprite = item.sprite;

        if (countContainer != null)
            countContainer.GetComponent<TMP_Text>().text = "x" + count.ToString();

        if (checkContainer != null)
        {
            if (done) // if all instances have been picked up the sprite is number 6, a checkmark
            {
                checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[5];
            }
            else // else its the sprite corresponding to how many instances have been picked up
            {
                checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[check];
            }
        }
    }

    // Function to switch between the two pages of the clipboard UI
    public void SwitchPage()
    {
        if (_currentPage == 1)
        {
            _clipboardPage2.SetActive(true);
            _currentPage = 2;
        }
        else if (_currentPage == 2)
        {
            _clipboardPage2.SetActive(false);
            _currentPage = 1;
        }
    }
}
