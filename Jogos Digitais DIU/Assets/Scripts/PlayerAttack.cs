using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public Transform Hitbox;

    public float alcanceAtaque = 0.5f;

    public LayerMask enemyLayer;

    public LayerMask breakableLayer;

    public float attackRatePunch = 2.5f;
    public float attackRateKick = 1.7f;
    float nextAttackTime = 0f;
    public SpecialBarScript bar;
    public float dmg;
    public AudioSource specialSound;
    void Start()
    {
        animator = GetComponent<Animator>();
        dmg = 0.5f;
    }

    
    void Update()
    {
        if(Time.time >= nextAttackTime){
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Punch");
                Invoke("Punch", 0.3f);
                nextAttackTime = Time.time + (1f / attackRatePunch);
            }

            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.SetTrigger("Kick");
                Invoke("RoundHouse", 0.4f);
                nextAttackTime = Time.time + (1f/attackRateKick);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(bar.slider.value == bar.maxVal){
                Special();
            }
        }

        
        
    }

    void Punch(){
        
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, enemyLayer);
        foreach(Collider2D enemy in inimigosAcertados){
            enemy.GetComponent<BasicEnemyScript>().TakeDamage(dmg);
        }
        Collider2D[] breakablesAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, breakableLayer);
        foreach(Collider2D breakable in breakablesAcertados){
            Destroy(breakable.gameObject);
        }
    }

    void RoundHouse(){
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, enemyLayer);
        foreach(Collider2D enemy in inimigosAcertados){
            enemy.GetComponent<BasicEnemyScript>().TakeDamage(dmg*2);
        }
        Collider2D[] breakablesAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, breakableLayer);
        foreach(Collider2D breakable in breakablesAcertados){
            Destroy(breakable.gameObject);
        }
    }


    //void OnTriggerEnter2D(Collider2D coll) //idk
    //{
   //     if(coll.CompareTag("Breakable")){
   //         Destroy(coll.gameObject);
    //    }
    //    if(coll.CompareTag("Enemy")){
     //       coll.gameObject.GetComponent<BasicEnemyScript>().takeDamage(1);
     //   }
  //  }

    void OnDrawGizmosSelected(){
        if(Hitbox == null) return;

        Gizmos.DrawWireSphere(Hitbox.position, alcanceAtaque);
    }

    public void IncreaseVal(){
		bar.IncreaseVal();
	}
	
	void Special(){
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, enemyLayer);
        foreach(Collider2D enemy in inimigosAcertados){
            enemy.GetComponent<BasicEnemyScript>().TakeDamage(100000f);
        }
        if(inimigosAcertados.Length > 0){
            animator.SetTrigger("Special");
            dmg += 0.5f;
            bar.SetVal(0);
            specialSound.Play();
        }
        
    }
    
}
