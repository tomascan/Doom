using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 10f;  // Velocidad de caminata normal
    public float momentumDamping = 5f;  // Frenado gradual
    public float jumpStrength = 12f;  // Fuerza del salto
    public float gravityScale = 4f;  // "Pesadez" del jugador al aplicar la gravedad
    private float verticalVelocity;  // Velocidad vertical actual
    private float myGravity = -10f;  // Gravedad aplicada al jugador

    private CharacterController myCC;  // Referencia al CharacterController
    public Animator camAnim;  // Referencia al Animator para la cámara
    private bool isWalking;  // Indica si el jugador está caminando
    private bool isGrounded;  // Indica si el jugador está tocando el suelo

    private Vector3 inputVector;  // Vector de entrada basado en el movimiento del jugador
    private Vector3 movementVector;  // Vector de movimiento para aplicar al CharacterController

    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = myCC.isGrounded;
        GetInput();
        MovePlayer();
        
        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        float currentSpeed = playerSpeed;

        // Comprobar si el jugador está presionando Shift para correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 1.5f;  // Aumentar la velocidad para correr
        }

        // Si se presionan las teclas WASD...
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);
            isWalking = true;
        }
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
            isWalking = false;
        }

        // Determinar la velocidad vertical basada en si el jugador está en el suelo o en el aire
        if (isGrounded)
        {
            verticalVelocity = -gravityScale;  // Mantener el jugador en el suelo

            // Permitir al jugador saltar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpStrength;
            }
        }
        else
        {
            verticalVelocity += myGravity * gravityScale * Time.deltaTime;  // Aplicar gravedad progresiva
        }

        // Construir el vector de movimiento final
        movementVector = (inputVector * currentSpeed) + Vector3.up * verticalVelocity;
    }

    //Power Up Speed
    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        playerSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        playerSpeed /= multiplier;
    }

    
    
    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
}
