using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();

    Animator animator;
    Rigidbody2D rb2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize() sorgt dafür, dass der Vektor seine Richtung beibehält, aber die Länge 1.0 hat. Wichtig, damit beim diagonalen Laufen der Charakter nicht schneller wird als bei horizontaler/vertikaler Bewegung.
        // Zur Erinnerung: horizontaler Vektor: -1.0 = links, +1.0 = rechts // vertikaler Vektor: -1.0 = runter, +1.0 = hoch. Werden die entsprechenden Tasten gedrückt, werden diese Vektoren "addiert", um eine neue Vektorrichtung zu erhalten.
        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
    }

    private void UpdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
}
