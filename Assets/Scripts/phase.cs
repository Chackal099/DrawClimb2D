using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phase : MonoBehaviour
{
    public Touch toque;
    Material mat;
    Vector3 startVertex;
    Vector3 touchPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - Screen.width) / Screen.width;
                pos.y = (pos.y - Screen.height) / Screen.height;
                startVertex = new Vector3(-pos.x, pos.y, 0.0f);

            }
        }
        void OnPostRender()
        {

            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadOrtho();

            GL.Begin(GL.LINES);
            GL.Color(Color.red);
            GL.Vertex(startVertex);
            GL.Vertex(new Vector3(touchPos.x / Screen.width, touchPos.y / Screen.height, 0));
            GL.End();

            GL.PopMatrix();
        }
    }
}
