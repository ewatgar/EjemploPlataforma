using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    GroundDetector gd;
    public float
        maxHSpeed = 10,
        hAccel = 30,
        hDeccel = 30,
        jumpImpulse = 10;

    int dir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        gd = GetComponentInChildren<GroundDetector>();
    }

    void FixedUpdate()
    {
        //dx es 0 o 1, no es un valor continuo. Para ello se asigna el valor 100 a Gravity y Sensitivity en Horizontal
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        float vx = 0;
        //an.SetFloat("Vx", Math.Abs(vx)); //pasar el valor absoluto de la velocidad


        if (dx == 0)
        {
            float delta = hDeccel * Time.fixedDeltaTime;
            if (rb.velocityY != 0)
            {
                delta = 0;
            }
            else
            {
                delta = hDeccel * Time.fixedDeltaTime;
            }



            // Deceleramos al personaje en la dirección contraria a su movimiento
            if (rb.velocityX > 0)
            {
                // Me muevo hacia la derecha
                vx = rb.velocityX - delta;
                if (vx < 0) vx = 0;
            }
            else if (rb.velocityX < 0)
            {
                // Me muevo hacia la izquierda
                vx = rb.velocityX + delta;
                if (vx > 0) vx = 0;
            }
        }
        else
        {
            // Aceleramos en la dirección indicada por dx
            vx = rb.velocityX + dx * hAccel * Time.fixedDeltaTime;
            vx = Mathf.Clamp(vx, -maxHSpeed, maxHSpeed); // se limita hasta la velocidad máxima por la izquierda y derecha
        }

        // Que mire en un lado a otro
        if (dx > 0) dir = 1;
        if (dx < 0) dir = -1;

        transform.localScale = new Vector3(dir, 1, 1);

        an.SetFloat("Vx", Math.Abs(vx)); //pasar el valor absoluto de la velocidad
        rb.velocityX = vx;

        //jump
        Debug.Log(gd.IsGrounded);
        if (dy > 0 && gd.IsGrounded)
        {
            rb.AddForceY(jumpImpulse, ForceMode2D.Impulse);
        }
    }
}
