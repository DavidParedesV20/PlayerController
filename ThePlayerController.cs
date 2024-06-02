using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayerController : MonoBehaviour
{
    private float speed = 4f;
    private bool isMoving;
    private Vector3 posicionObjetivo;
    private Rigidbody2D rb;
    private Animator animator;

    // Variables para almacenar la última dirección de movimiento
    private float lastMoveX;
    private float lastMoveY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("sillon"))  // Asegúrate de que el sillón tenga el tag "Sillon"
        {
            // Desactivar la animación de movimiento
            animator.SetBool("isMoving", false);
            // Activar la animación de sentarse
            animator.SetTrigger("Sit");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("sillon"))
        {
            // Cambiar la animación de Sit a walking
            animator.ResetTrigger("Sit");
            animator.SetBool("isMoving", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Detener el movimiento del jugador al entrar en colisión
        posicionObjetivo = this.transform.position;
        isMoving = false;

        // Desactivar la animación de movimiento
        animator.SetBool("isMoving", false);

        // Mantener la última dirección de movimiento para la animación de idle
        animator.SetFloat("lastMoveX", lastMoveX);
        animator.SetFloat("lastMoveY", lastMoveY);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicionObjetivo = this.transform.position;
    }

    void FixedUpdate()
    {
        // Detectar clic del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = this.transform.position.z;

            Vector3 direction = (mousePosition - this.transform.position).normalized;

            // Desactivar movimiento en diagonal
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                posicionObjetivo = new Vector3(mousePosition.x, this.transform.position.y, this.transform.position.z);
                direction.y = 0;
            }
            else
            {
                posicionObjetivo = new Vector3(this.transform.position.x, mousePosition.y, this.transform.position.z);
                direction.x = 0;
            }

            // Almacenar la última dirección de movimiento
            lastMoveX = direction.x;
            lastMoveY = direction.y;

            // Activar la animación de movimiento
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("isMoving", true);
        }

        // Mover el jugador hacia la posición objetivo
        if (Vector3.Distance(this.transform.position, posicionObjetivo) > 0.1f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, posicionObjetivo, speed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;

            // Desactivar la animación de movimiento cuando el jugador alcanza la posición objetivo
            animator.SetBool("isMoving", false);

            // Mantener la última dirección de movimiento para la animación de idle
            animator.SetFloat("lastMoveX", lastMoveX);
            animator.SetFloat("lastMoveY", lastMoveY);
        }

        // Actualizar el estado de isMoving
        animator.SetBool("isMoving", isMoving);
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayerController : MonoBehaviour
{
    private float speed = 4f;
    private bool isMoving;
    private Vector3 posicionObjetivo;
    private Rigidbody2D rb;
    private Animator animator;

    // Variables para almacenar la última dirección de movimiento
    private float lastMoveX;
    private float lastMoveY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //Contacto de player con sillon pero que sobrepase el obeto
    void OnTriggerEnter2D(Collider2D sillon)
    {
        Debug.Log("chocaste");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Detener el movimiento del jugador al entrar en colisión
        posicionObjetivo = this.transform.position;
        isMoving = false;

        // Desactivar la animación de movimiento
        animator.SetBool("isMoving", false);

        // Mantener la última dirección de movimiento para la animación de idle
        animator.SetFloat("lastMoveX", lastMoveX);
        animator.SetFloat("lastMoveY", lastMoveY);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicionObjetivo = this.transform.position;
    }

    void FixedUpdate()
    {
        // Detectar clic del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = this.transform.position.z;

            Vector3 direction = (mousePosition - this.transform.position).normalized;

            // Desactivar movimiento en diagonal
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                posicionObjetivo = new Vector3(mousePosition.x, this.transform.position.y, this.transform.position.z);
                direction.y = 0;
            }
            else
            {
                posicionObjetivo = new Vector3(this.transform.position.x, mousePosition.y, this.transform.position.z);
                direction.x = 0;
            }

            // Almacenar la última dirección de movimiento
            lastMoveX = direction.x;
            lastMoveY = direction.y;

            // Activar la animación de movimiento
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("isMoving", true);
        }

        // Mover el jugador hacia la posición objetivo
        if (Vector3.Distance(this.transform.position, posicionObjetivo) > 0.1f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, posicionObjetivo, speed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;

            // Desactivar la animación de movimiento cuando el jugador alcanza la posición objetivo
            animator.SetBool("isMoving", false);

            // Mantener la última dirección de movimiento para la animación de idle
            animator.SetFloat("lastMoveX", lastMoveX);
            animator.SetFloat("lastMoveY", lastMoveY);
        }

        // Actualizar el estado de isMoving
        animator.SetBool("isMoving", isMoving);
    }
}*/