using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ClipboardManager : MonoBehaviour
{
    public static ClipboardManager Instance;
    [SerializeField] private GameObject _clipboardItemPrefab;
    [SerializeField] private Sprite[] _clipboardItemSprites;
    [SerializeField] private GameObject _clipBoardHealth;
    [SerializeField] private GameObject _clipboardPage2;
    [SerializeField] public GameObject itemListContainer;
    private int _currentPage = 1;


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

    public void ReduceHealth()
    {
        if (_clipBoardHealth.transform.childCount > 0)
        {
            Destroy(_clipBoardHealth.transform.GetChild(0).gameObject);
        }
    }

    public void setupClipboard(PickupItemClass item, int count, int check, bool done)
    {
        GameObject gameObject = Instantiate(_clipboardItemPrefab
        , this.transform);
        gameObject.transform.SetParent(itemListContainer.transform, false);

        var iconContainer = gameObject.transform.Find("Icon");
        var countContainer = gameObject.transform.Find("Count");
        var checkContainer = gameObject.transform.Find("Check");

        // Now modify `gameObject`, not `_clipboardFurniture`:
        if (iconContainer != null)
            iconContainer.GetComponent<Image>().sprite = item.sprite;

        if (countContainer != null)
            countContainer.GetComponent<TMP_Text>().text = "x" + count.ToString();

        if (checkContainer != null)
        {
            if (done)
            {
                checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[5];
            }
            else
            {
            checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[check];
            }
        }
    }

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
