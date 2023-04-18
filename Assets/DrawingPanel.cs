using System;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(LineRenderer))]
public class DrawingPanel : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    [SerializeField] [Range(0,1)]private float width=0.5f;
    [SerializeField] private Color color;
    [SerializeField] private Material lineMat;

    LineRenderer lineRenderer;
    bool drawing;    
    Vector2 touchPos;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMat;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (drawing)
        {
            GetTouchPosition();
            Draw();
        }
    }

    private void GetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPos= touch.position;
        }
       
    }

   
    void Draw()
    {
        lineMat.color = color;
        Vector3 _touchPos = touchPos;
        _touchPos.z = 10.0f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(_touchPos);
          
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!drawing)
        {
            lineRenderer.positionCount=0;
        }
        drawing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        drawing = false;
    }
   
}
