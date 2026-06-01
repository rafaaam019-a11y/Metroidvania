using UnityEngine;

public class EnemyPlatformAI : MonoBehaviour
{
    public Transform player;

    public float velocidadePatrulha = 1f;
    public float velocidadePerseguicao = 2f;
    public float distanciaVisao = 3f;
    public float distanciaPatrulha = 0.3f;

    private Vector3 posicaoInicial;
    private int direcao = 1;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distanciaPlayer = Vector2.Distance(transform.position, player.position);

        if (distanciaPlayer <= distanciaVisao)
        {
            SeguirPlayerNaPlataforma();
        }
        else
        {
            Patrulhar();
        }
    }

    void SeguirPlayerNaPlataforma()
    {
        float limiteEsquerdo = posicaoInicial.x - distanciaPatrulha;
        float limiteDireito = posicaoInicial.x + distanciaPatrulha;

        float novoX = Mathf.MoveTowards(
            transform.position.x,
            player.position.x,
            velocidadePerseguicao * Time.deltaTime
        );

        novoX = Mathf.Clamp(novoX, limiteEsquerdo, limiteDireito);

        transform.position = new Vector2(novoX, transform.position.y);

        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Patrulhar()
    {
        transform.Translate(Vector2.right * direcao * velocidadePatrulha * Time.deltaTime);

        if (Vector2.Distance(posicaoInicial, transform.position) >= distanciaPatrulha)
        {
            direcao *= -1;
            Virar();
        }
    }

    void Virar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}