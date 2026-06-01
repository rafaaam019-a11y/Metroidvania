using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float velocidadePatrulha = 1f;
    public float velocidadePerseguicao = 2.5f;
    public float distanciaVisao = 4f;
    public float distanciaPatrulha = 0.3f;

    private Vector3 posicaoInicial;
    private int direcao = 1;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float distanciaPlayer =
            Vector2.Distance(transform.position, player.position);

        if (distanciaPlayer <= distanciaVisao)
        {
            SeguirPlayer();
        }
        else
        {
            Patrulhar();
        }
    }

    void SeguirPlayer()
    {
        Vector2 destino = new Vector2(
            player.position.x,
            transform.position.y
        );

        transform.position = Vector2.MoveTowards(
            transform.position,
            destino,
            velocidadePerseguicao * Time.deltaTime
        );

        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Patrulhar()
    {
        transform.Translate(
            Vector2.right * direcao * velocidadePatrulha * Time.deltaTime
        );

        if (Vector2.Distance(posicaoInicial, transform.position)
            >= distanciaPatrulha)
        {
            direcao *= -1;

            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}