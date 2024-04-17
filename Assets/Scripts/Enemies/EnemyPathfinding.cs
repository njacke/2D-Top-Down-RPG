using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < 0) {
            spriteRenderer.flipX = true;
        }        
        else{
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition){
        moveDir = targetPosition;
    }
}
