using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] Material inactiveMaterial;
    [SerializeField] Material activeMaterial;
    [SerializeField] GameObject safeZoneGround;
    [SerializeField] GameObject furniturePrefab;
    [SerializeField] GameObject furnitureSpawnPoint;

    private bool _isActive = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _isActive = StateManager.allItemsCollected;

        if (_isActive)
        {
            safeZoneGround.GetComponent<Renderer>().material = activeMaterial;
        }
        else
        {
            safeZoneGround.GetComponent<Renderer>().material = inactiveMaterial;
        }
    }

    public void BuildFurniture()
    {
        if (StateManager.allItemsCollected)
        {
            StateManager.itemBuild = true;
            GameObject furniture = Instantiate(furniturePrefab, furnitureSpawnPoint.transform.position, Quaternion.identity);
            furniture.transform.Rotate(-90, 0, 0);
            furniture.transform.SetParent(furnitureSpawnPoint.transform);
        }
    }
}
