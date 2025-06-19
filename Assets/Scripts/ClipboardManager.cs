using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ClipboardManager : MonoBehaviour
{
    [SerializeField] private GameObject _clipboardItem;

    void Start()
    {
        setupClipboard();
    }

    void setupClipboard()
    {
        GameObject gameObject = Instantiate (_clipboardItem, this.gameObject.transform);
        gameObject.transform.SetParent(this.gameObject.transform);
    }

    void Update()
    {
        
    }
}
