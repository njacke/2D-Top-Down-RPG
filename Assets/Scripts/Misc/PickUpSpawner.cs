using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab;

    public void DropItems() {
        Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
    }
}
