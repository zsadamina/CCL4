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
    [SerializeField] private GameObject _clipboardFurniture;
    [SerializeField] private Sprite[] _clipboardItemSprites;
    [SerializeField] private GameObject _clipBoardHealth;
    [SerializeField] private GameObject _clipboardPage2;
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
        DontDestroyOnLoad(this);
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
        if (!this._clipboardFurniture)
        {
            return;
        }
        GameObject gameObject = Instantiate(_clipboardFurniture, this.gameObject.transform);
        
        gameObject.transform.SetParent(this.gameObject.transform);

        var iconContainer = _clipboardFurniture.transform.Find("Icon");
        if (iconContainer == null)
        {
            Debug.LogError("Icon container not found in clipboard furniture prefab.");
            return;
        }

        iconContainer.GetComponent<Image>().sprite = item.sprite;

        var countContainer = _clipboardFurniture.transform.Find("Count");
        countContainer.GetComponent<TMP_Text>().text = "x" + count.ToString();

        var checkContainer = _clipboardFurniture.transform.Find("Check");

        if (done)
        {
            checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[0];
        }
        else
        {
            checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[check + 1];
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
