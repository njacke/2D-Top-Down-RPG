using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaserPrefab;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack(){
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnStaffProjectileAnimEvent(){
        GameObject newMagicLaser = Instantiate(magicLaserPrefab, magicLaserSpawnPoint.position, Quaternion.identity);
        newMagicLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }

    private void MouseFollowWithOffset(){
    var mousePos = Input.mousePosition;
    var playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

    //adding offset so sword turns towards mouse
    float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

    if (mousePos.x < playerPos.x){
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
    }
    else{
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
}
