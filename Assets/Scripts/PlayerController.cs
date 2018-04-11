﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float velWalk;
	public float actualWalk;

	public float VelRun;
	public float actualRun;

	public float Jump;

	public bool ground;
	public bool running;
	public bool idle;

	SpriteRenderer spr;

	public Sprite Default;
	public Sprite Right;
	public Sprite Up;
	public Sprite Down;
	public Sprite sJump;
	public Sprite Run;

	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		actualWalk = velWalk;
		actualRun = VelRun;
	}

	// Update is called once per frame
	void Update () {

		idle = true;
		running = false;

		float xAxis = Input.GetAxis ("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");

		Vector3 movement = transform.TransformDirection (xAxis, yAxis, 0);

		float leftxAxis = Input.GetAxis ("LeftJoystick_X");
		float leftyAxis = Input.GetAxis ("LeftJoystick_Y");

		transform.Translate (movement * velWalk);


		///ANIMACIONES!!!!

		//Movimiento Derecha
		if (xAxis >= 1) {
			idle = false;
			spr.flipX = false;
		}

		//Movimiento Izquierda
		else if (xAxis <= -1) {
			idle = false;
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

		//Saltar
		if (yAxis >= 1 && ground == true) {
			idle = false;
			ground = false;
			velWalk = Jump;
		} else {
			velWalk = actualWalk;
		}

		if (idle == false) {
			spr.sprite = Right;
		}

		if (ground == false) {
			spr.sprite = sJump;
		}

		if (running == true) {
			spr.sprite = Run;
		}

		//------------------------------------------------------------------

		//Correr Derecha
		if (xAxis >= 1 && Input.GetButton ("A")) {
			idle = false;
			running = true;
			velWalk = VelRun;
		}

		//Correr Izquierda
		else if (xAxis <= -1 && Input.GetButton ("A")) {
			idle = false;
			running = true;
			velWalk = VelRun;
		} else {
			velWalk = actualWalk;
		}

		//Mover la direccion de disparo
		if (leftyAxis <= -1 && ground == true) {
			idle = false;
		} else if (leftyAxis <= -1 && ground == false) {
			idle = false;
		} else if (leftyAxis >= 1 && ground == true) {
			idle = false;
		} else if (leftyAxis >= 1 && ground == false) {
			idle = false;
		}

		else {
			if(ground==true && running == false && idle == true){
				spr.sprite = Default;
			}
			else if(ground==true && running == true && idle==false){
				spr.sprite = Run;
			}
		}



	}

	void OnCollisionEnter2D (Collision2D _col){

		//Controlador del salto
		if(_col.gameObject.CompareTag("Suelo")){

			ground = true;

			if (spr.sprite == sJump && running == false) {
				spr.sprite = Default;
			} 
			else if (spr.sprite == sJump && running == true) {
				spr.sprite = Run;
			}

		}
	}
}
