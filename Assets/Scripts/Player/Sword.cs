using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown, isAttacking = false;


    private GameObject slashAnim;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void Start() {
        weaponCollider.gameObject.SetActive(false);
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update() {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking(){
        attackButtonDown = true;
    }

    private void StopAttacking(){
        attackButtonDown = false;
    }

    private void Attack(){

        if (attackButtonDown && !isAttacking){
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, quaternion.identity);
            slashAnim.transform.parent = this.transform.parent; //instantiate it in same parent as this object
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine(){
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent(){
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft){
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent(){
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if(playerController.FacingLeft){
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset(){
        var mousePos = Input.mousePosition;
        var playerPos = Camera.main.WorldToScreenPoint(playerController.transform.position);

        //adding offset so sword turns towards mouse
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerPos.x){
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else{
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
