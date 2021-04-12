using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public Camera cam;

    public float movementTime;
    
    [Header("Movement")]
    public float movementSpeed;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    [Header("Rotation")]
    public float rotationAmount;
    
    [Header("Zoom")]
    public float zoomAmount;
    public float zoomSpeed;
    public float minZoom = 1.0f;
    public float maxZoom = 20.0f;
    
    public Vector3 newPosition;
    public Quaternion newRotation;
    public float zoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    private bool _isMouseOn;
    void Start()
    {
        newPosition = cam.transform.position;
        newRotation = cam.transform.rotation;
        zoom = cam.orthographicSize;
    }
    
    void Update()
    {
        HandeMouseInput();
        HandleMovementInput();   
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GameManager.Instance.gameState != GameManager.GameState.Building)
            GameManager.Instance.gameState = GameManager.GameState.Play;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.gameState = GameManager.GameState.Menu;
    }

    private void HandeMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0.0f && GameManager.Instance.gameState == GameManager.GameState.Play)
        {
            zoom -= Input.mouseScrollDelta.y * zoomSpeed;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = MousePosGet();
        }

        if (Input.GetMouseButton(0))
        {
            dragStartPosition = MousePosGet();

            newPosition = cam.transform.position + dragStartPosition - dragCurrentPosition;
        }
    }

    private Vector3 MousePosGet()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        float entry;

        if (plane.Raycast(ray, out entry))
        {
            return ray.GetPoint(entry);
        }
        
        return Vector3.zero;
    }
    
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += (transform.up * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            newPosition += (transform.up * -movementSpeed);
        }    
        
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += (transform.right * movementSpeed);
        }     
        
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += (transform.right * -movementSpeed);
        }   
        
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.forward * rotationAmount);
        }    
        
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.back * rotationAmount);
        }   
        
        if (Input.GetKey(KeyCode.R))
        {
            zoom += zoomAmount;
        }

        if (Input.GetKey(KeyCode.F))
        {
            zoom -= zoomAmount;
        }
        
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * movementTime);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRotation, Time.deltaTime * movementTime);
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, zoom, Time.deltaTime * movementTime);
    }

}
