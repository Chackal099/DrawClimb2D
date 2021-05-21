using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private Collider2D drag;
    public LayerMask layer;
    [SerializeField]
    private bool clicked;
    private Touch touch;

    void Start()
    {
        drag = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(wp, -Vector2.up, Mathf.Infinity, layer.value);

            if (hit.collider != null)
            {
                clicked = true;
            }
            if (clicked)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector3 tPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                    transform.position = tPos;
                }
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                clicked = false;
            }
        }
    }
}