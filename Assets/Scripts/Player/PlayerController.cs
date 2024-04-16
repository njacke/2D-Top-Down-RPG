using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : Singleton<PlayerController>
{

    public bool FacingLeft { get {return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerControls playerControls;
    private Vector2 movement; //used to store values from player controls
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float startingMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake() {
        base.Awake(); //will call Singleton's Awake first

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();       
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    //required to enable with new Unity control system
    private void OnEnable(){
        playerControls.Enable();
    }

    private void Update() {
        PlayerInput();        
    }

    private void FixedUpdate() {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput(){
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move(){
        //combine floats first to only do vector math once (performance)
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection(){
        var mousePos = Input.mousePosition;
        var playerPos = Camera.main.WorldToScreenPoint(transform.position);
        
        if (mousePos.x < playerPos.x){
            //flip sprite over X axis
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else{
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash(){
        if (!isDashing){
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine(){
        float dashTime = .2f;
        float dashCD = .3f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
