using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab;
    [SerializeField] private GameObject healthGlobePrefab;
    [SerializeField] private GameObject staminaGlobePrefab;

    public void DropItems() {

        int randomNub = Random.Range(1, 5);

        if (randomNub == 1) {
            int randomAmountOfGold = Random.Range(1, 4);

            for (int i = 0; i < randomAmountOfGold; i++){
            Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
        }

        if (randomNub == 2) {
            Instantiate(healthGlobePrefab, transform.position, Quaternion.identity);
        }

        if (randomNub == 3) {
            Instantiate(staminaGlobePrefab, transform.position, Quaternion.identity);
        }
    }
}
