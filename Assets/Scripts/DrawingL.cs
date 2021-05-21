using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingL : MonoBehaviour
{
    public GameObject LineP; //Prefab feito da linha
    public GameObject Square; //O quadrado (player)
    public GameObject currentL; //Como será feita a linha
    public LineRenderer lineRenderer; //A linha criada
    public EdgeCollider2D edgeCollider; //O colisor para dar colisão a linha
    public WheelJoint2D springCollider;
    public List<Vector2> fingerPositions; //Array que determina quantos vértices tem a linha, baseado no quanto a tela foi tocada
    private Touch touch; //Facilita a escrita do Input.GetTouch(0), ou seja, quando a tela for tocada

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.touchCount > 0) //Se houver toque na tela
        {
            touch = Input.GetTouch(0); //touch é o toque na tela
            if (touch.phase == TouchPhase.Began) //Se o toque começou
            {
                Destroy(lineRenderer); //Destrua a linha anterior criada
                CreateLine(); //Use a função Create Line
            }
        }
        if (touch.phase == TouchPhase.Moved) //Se o toque moveu
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(touch.position); //Variável que determina a posição do toque na tela, dando APENAS NESSA FASE a informação para a variável tempFingerPos
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f) //Se a tela continuar sendo tocada, e a variável Finger Positions for maior que 0.1
            {
                UpdateLine(tempFingerPos); //Executa a função UpdateLine em tempFingerPos
                
            }
        }
        if (touch.phase == TouchPhase.Ended) //Quando a fase de toque acabar
        {
            EndLine(); //Função EndLine é executada
        }
    }
    void CreateLine() //Função da criação da linha de desenho feita pelo player
    {
        currentL = Instantiate(LineP, Vector3.zero, Quaternion.identity); //Cria a linha usando a prefab colocada no Editor
        lineRenderer = currentL.GetComponent<LineRenderer>(); //A linha vai receber o material da prefab
        edgeCollider = currentL.GetComponent<EdgeCollider2D>(); 
        springCollider = currentL.GetComponent<WheelJoint2D>();//A linha vai receber o Collider da prefab
        fingerPositions.Clear(); //A posição da linha aterior é ignorada
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(touch.position)); //Aonde a tela for tocada, a linha vai ser feita
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(touch.position)); //Add tem que ser feito duas vezes, ou vai sair com vários pontinhos
        lineRenderer.SetPosition(0, fingerPositions[0]); //Coloca o primeiro Array
        lineRenderer.SetPosition(1, fingerPositions[1]); //Coloca a segunda posição do Array
        edgeCollider.points = fingerPositions.ToArray(); //Os pontos do Edge Collider vão ser os mesmos do Array fingerPositions
    }

    void UpdateLine(Vector2 newFingerPos) //função que desenha a linha (faz update) conforme a nova posição do toque (newFingerPos)
    {
        fingerPositions.Add(newFingerPos); //A linha vai ser desenhada, dependendo da variável newFingerPos
        lineRenderer.positionCount++; //O contador da linha
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos); //A posição da linha vai ser a newFingerPos
        edgeCollider.points = fingerPositions.ToArray(); //Os pontos do Edge Collider vão ser os mesmos do Array fingerPositions
    }
 
    void EndLine() //rotaciona a linha desenhada pelo player, torcando a posição dela para o quadrado
    {
        Square.transform.Rotate (0,0,-100*Time.deltaTime, Space.Self);
        lineRenderer.transform.position = new Vector2(Square.transform.position.x, Square.transform.position.y);
    }
}
