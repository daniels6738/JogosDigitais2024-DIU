using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {


	public CharacterController2D controller;
	public Animator animator;

	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private LayerMask m_WhatIsGround;	

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	
	[SerializeField]
	private int health;

	[SerializeField]
	private int hpLimit;
	[SerializeField]
    private bool blocking = false;
	private SpriteRenderer sp;
	private int gokuCount;
	public AudioSource[] goku;
	public SpecialBarScript bar;
	
	// Update is called once per frame

	void Start(){
		animator = GetComponent<Animator>();
		sp = GetComponent<SpriteRenderer>();
		gokuCount = 0;
	}
	void Update () {
		
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("Jump", true);
			
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if(Input.GetKeyDown(KeyCode.Escape)){ //só um jeito facil de parar o jogo
			UnityEditor.EditorApplication.isPlaying = false;
		}

		if(Input.GetKeyDown(KeyCode.T)){ //TODO testar
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if(Input.GetKeyDown(KeyCode.Q)){
            blocking = true;
			sp.material.color = new Color(0,0,0,0.7f);
            Invoke(nameof(EndBlock), 0.3f);

        }


		
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	public void TakeDamage(int i, GameObject attacker){
		if(!blocking){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else{
			goku[gokuCount].Play();
			if(gameObject.GetComponent<CharacterController2D>().isFacingRight()){
				transform.Translate(2f,0f,0f);
				
			} else {transform.Translate(-2f,0f,0f);}
			
			attacker.GetComponent<BasicEnemyScript>().TakeDamage(gameObject.GetComponent<PlayerAttack>().dmg*10);
			UpdateGoku();
			bar.IncreaseVal();

			
		}
	}

	/* public void Heal(int i){
		health += i;
		if(health >= hpLimit) health = hpLimit;
	} */

	private void ResetOnDeath(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void EndBlock(){
		sp.material.color = Color.white;
        blocking = false;
    }

	void UpdateGoku(){
		gokuCount++;
		if(gokuCount >= 4){
			gokuCount = 0;
		}
	}
}
