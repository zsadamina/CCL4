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
    [SerializeField] private GameObject _clipboardItem;
    [SerializeField] private Sprite[] _clipboardItemSprites;


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

    void Start()
    {

    }

    public void setupClipboard(PickupItemClass item, int count, int check, bool done)
    {
        GameObject gameObject = Instantiate(_clipboardItem, this.gameObject.transform);
        gameObject.transform.SetParent(this.gameObject.transform);

        var iconContainer = _clipboardItem.transform.Find("Icon");
        iconContainer.GetComponent<Image>().sprite = item.sprite;

        var countContainer = _clipboardItem.transform.Find("Count");
        countContainer.GetComponent<TMP_Text>().text = "x" + count.ToString();

        var checkContainer = _clipboardItem.transform.Find("Check");

        if (done)
        {
            checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[0];
        }
        else
        {
            checkContainer.GetComponent<Image>().sprite = _clipboardItemSprites[check+1];
        }

        // var text = new GameObject("Text");
        // text.transform.SetParent(iconParent);
        // text.AddComponent<Text>().text = item.Name; 

    }

    void Update()
    {

    }
}
