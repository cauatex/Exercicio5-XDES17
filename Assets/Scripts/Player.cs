//Cauã Teixeira Gomes Vieira - 2024009195
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    int qtdPulos = 0;
    Rigidbody2D _rb;
    float xDir;
    [SerializeField] float forçaPulo;
    [SerializeField] float xVel;
    Animator animador;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animador = GetComponentInChildren<Animator>();
    }

    void OnMove(InputValue inputValue)
    {
        xDir = inputValue.Get<Vector2>().x;    
    }

    void OnJump()
    {
        if (qtdPulos < 2 ){
            qtdPulos = qtdPulos + 1;
            Pular();
        }
    }

    void Mover()
    {
        _rb.linearVelocityX = xDir * xVel;
        bool correndo = Mathf.Abs(_rb.linearVelocityX) > Mathf.Epsilon;
        animador.SetBool("Correndo", correndo);

        if (correndo)
        {
            Flip();
        }   
    }

    void Pular()
    {
        if (qtdPulos == 1){
            _rb.AddForceY(forçaPulo);
        }
        else if (qtdPulos == 2)
        {
            _rb.AddForceY(1.5f*forçaPulo);
        }

        
    }

    void Flip()
    {
        transform.localScale = new Vector3(Mathf.Sign(_rb.linearVelocityX), 1, 1);
    }

    void FixedUpdate()
    {
        Mover();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        qtdPulos = 0;
    }
}
