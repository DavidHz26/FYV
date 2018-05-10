﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float velWalk;
	public float actualWalk;

	public float Jump;

	public bool ground;
	public bool idle;
	
	public float vida = 3;

	SpriteRenderer spr;

	public Sprite Default;
	public Sprite Right;
	public Sprite Up;
	public Sprite Down;
	public Sprite sJump;
	
	public GameObject SecondCamera;
	public GameObject CanvasGO;
	public GameObject actSub;
	public GameObject actJef;

	void Start () 
	{
		spr = GetComponent<SpriteRenderer> ();
	
		actualWalk = velWalk;
	}

	void Update () 
	{
		idle = true;
		
		//Movimiento
		
		float xAxis = Input.GetAxis ("Horizontal");
		
		Vector3 movement = transform.TransformDirection (xAxis, 0, 0);

		float leftxAxis = Input.GetAxis ("LeftJoystick_X");
		
		transform.Translate (movement * velWalk);
		

		///ANIMACIONES!!!!

		//Movimiento Derecha
		if (xAxis >= 1) {
			idle = false;
			spr.sprite = Right;
			spr.flipX = false;
		}

		//Movimiento Izquierda
		else if (xAxis <= -1) {
			idle = false;
			spr.sprite = Right;
			spr.flipX = true;
		}

		//Movimiento Derecha con Salto
		if (xAxis >= 1 && ground == false) {
			idle = false;
		}

		//Movimiento Izquierda con Salto
		if (xAxis <= -1 && ground == false) {
			idle = false;
		}

		//------------------------------------------------------------------
		
		if(Input.GetButton ("Start")){
			print("En pausa");
			
		}
		
		//Saltar
		if (Input.GetButton ("A") && ground == true) {
			idle = false;
			ground = false;
			transform.Translate(Vector3.up * Jump);
			spr.sprite = sJump;
			
		}
		else {
			if(ground==true &&  idle == true){
				spr.sprite = Default;
			}
		}
		
		//Morir
		if(vida <= 0)
		{
			SecondCamera.SetActive(true);
			CanvasGO.SetActive(true);
			gameObject.SetActive(false);
			Time.timeScale = 0.0f;
		}
	}

	void OnCollisionEnter2D (Collision2D _col)
	{
		//Controlador del salto
		if(_col.gameObject.CompareTag("Suelo")){

			ground = true;

			if (spr.sprite == sJump) {
				spr.sprite = Default;
			} 
		}
		
		//Controlador de vida
		if(_col.gameObject.tag == "enemigote" || _col.gameObject.tag == "balin" || _col.gameObject.tag == "balon")
		{
			vida--;
		}
		
		if(_col.gameObject.tag == "vida+")
		{
			if(vida < 3)
			{
				vida++;
			}
		}
		
		if(_col.gameObject.tag == "vida++")
		{
			if(vida < 3)
			{
				vida = 3;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D otro)
	{
		if(otro.gameObject.CompareTag("activa1"))
		{
			actSub.GetComponent<patron_subjefe>().enabled = true;
		}
		
		if(otro.gameObject.CompareTag("activa2"))
		{
			actJef.GetComponent<patron_jefe>().enabled = true;
		}
	}
}
