﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    public Rigidbody2D rigiBird;
    public Animator ani;
    public float speed=5f;
    private bool death = false;

    public delegate void DeathNotify();
    public event DeathNotify Ondeath;

    private Vector3 initpos;

    public UnityAction<int>  onSorce;

    public GameObject BulletTemplate;
    public float FireRate=10;//开火间隔/子弹间隔

    // Use this for initialization
    void Start () {
        this.ani = GetComponent<Animator>();
        this.Idle();
        initpos = this.transform.position;
	}

    public void Init()
    {
        this.transform.position = initpos;
        this.death = false;
        this.Idle();
        
    }

    float firetime = 0;
	// Update is called once per frame
	void Update () {
        if (this.death)
            return;

        firetime += Time.deltaTime;

        Vector2 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal")* Time.deltaTime* speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime*speed;
        this.transform.position = pos;

        if (Input.GetButton("Fire1"))
        {
            this.fire();
        }
    }
    public void fire()
    {
        if (FireRate <= 0)
            FireRate = 1;
        
        if (firetime > 1f/FireRate)
        {
            GameObject go = Instantiate(BulletTemplate);
            go.transform.position = this.transform.position;
            firetime = 0;
        }
       
    }

    public void Idle()
    {
        this.ani.SetTrigger("Idle");
        // this.ani.applyRootMotion = false;
        this.rigiBird.simulated = false;
        //this.ani.updateMode = AnimatorUpdateMode.AnimatePhysics;

        // this.ani.applyRootMotion = false;
        this.ani.updateMode = AnimatorUpdateMode.Normal;
        this.ani.applyRootMotion = false;



    }
    public void Fly()
    {
        
        this.ani.SetTrigger("Fly");
        this.rigiBird.simulated = true;
        this.ani.applyRootMotion = true;
        //if (this.death == false)
        //    this.death = false;
         this.ani.updateMode = AnimatorUpdateMode.AnimatePhysics;
        


    }
    public void Die()
    {
        this.death = true;
        if (this.Ondeath != null)
        {
            this.Ondeath();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D : " + collision.gameObject.name+ ";;"+  collision.transform.name+";"+ Time.time);
        this.Die();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("OnTriggerEnter2D : " + collision.gameObject.name + ";;" + collision.transform.name + ";" + Time.time);
        if (collision.gameObject.name.Equals("scorearea"))
        {

        }else
        {
         //   this.Die();
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("OnTriggerEnter2D : " + collision.gameObject.name + ";;" + collision.transform.name + ";" + Time.time);
        if (collision.gameObject.name.Equals("scorearea"))
        {
            if (onSorce != null)
                this.onSorce(1);
        }
        else
        {
          //  this.Die();
        }

    }

}
