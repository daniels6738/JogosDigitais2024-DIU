using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float health;
    [SerializeField]
    private float speed;
    private float animSpeed;
    public Animator animator;
    public Transform Hitbox;
    public BoxCollider2D checkRange;
    private bool canMove = true;
    public float alcanceAtaque = 0.5f;
    float nextAttackTime = 0f;
    private bool isHurting = false;

    public LayerMask playerLayer;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("canMove", true);
    }
    
    void Update()
    {
        if(canMove){
            /* Vector3 lastPosition = Vector3.zero;
            animSpeed = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
            animSpeed = Vector3.Magnitude(GetComponent<Rigidbody2D>().velocity); */
            
            Vector3 scale = transform.localScale;

            if(player.transform.position.x > transform.position.x){
                scale.x = Mathf.Abs(scale.x);
                transform.Translate(speed * Time.deltaTime, 0, 0);
            } else {
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.Translate(speed * Time.deltaTime *-1, 0, 0);
            }

            transform.localScale = scale;

            /* Vector3 direction = (target.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime); */
        }


    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && Time.time >= nextAttackTime){
            canMove = false;
            animator.SetBool("canMove", false);
            nextAttackTime = Time.time + 1.5f;
            animator.SetTrigger("Attack");
            if(!isHurting && health > 0){
                Invoke("DownStrike", 0.6f);
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        if(coll.CompareTag("Player") && Time.time >= nextAttackTime){
            nextAttackTime = Time.time + 1.5f;
            animator.SetTrigger("Attack");
            if(!isHurting && health > 0){
                Invoke("DownStrike", 0.6f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        Invoke("temp", 0.6f);
    }

    public void TakeDamage(float dmg){
        isHurting = true;
        Invoke(nameof(StopHurting), 0.4f);
        health -= dmg;
        animator.SetTrigger("Hurt");
        if(health <= 0){
            //player.GetComponent<PlayerMovement>().Heal(2);
            animator.SetBool("Dead",true);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
            this.enabled = false;
            Invoke("Vanish", 3f);
        }
    }

    void OnDrawGizmosSelected(){
        if(Hitbox == null) return;

        Gizmos.DrawWireSphere(Hitbox.position, alcanceAtaque);
    }

    void temp(){
        canMove = true;
        animator.SetBool("canMove", true);
    }

    void DownStrike(){
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, playerLayer);
        foreach(Collider2D player in inimigosAcertados){
            if(!isHurting && health > 0)player.GetComponent<PlayerMovement>().TakeDamage(1, gameObject);
        }
    }

    void StopHurting(){
        isHurting = false;
    }

    void Vanish(){
        Destroy(gameObject);
    }

    public void SetHealth(float i){
        health = i;
    }
}
