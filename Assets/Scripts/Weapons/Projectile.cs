using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start() {
        startPosition = transform.position;
    }

    private void Update() {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo){
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        if (!other.isTrigger && (enemyHealth || indestructible)){
            Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance(){
        if (Vector3.Distance(startPosition, transform.position) > weaponInfo.weaponRange){
            Destroy(gameObject);
        }
    }

    private void MoveProjectile(){
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
    }
}
