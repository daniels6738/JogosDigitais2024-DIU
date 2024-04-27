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
    void Start()
    {
        animator = GetComponent<Animator>();
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

        
        
    }

    void Punch(){
        
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, enemyLayer);
        foreach(Collider2D enemy in inimigosAcertados){
            enemy.GetComponent<BasicEnemyScript>().TakeDamage(0.5f);
            Debug.Log("acertei");
        }
        Collider2D[] breakablesAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, breakableLayer);
        foreach(Collider2D breakable in breakablesAcertados){
            Destroy(breakable.gameObject);
        }
    }

    void RoundHouse(){
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(Hitbox.position, alcanceAtaque, enemyLayer);
        foreach(Collider2D enemy in inimigosAcertados){
            enemy.GetComponent<BasicEnemyScript>().TakeDamage(1);
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

    
}
