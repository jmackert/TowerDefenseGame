using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    private Quaternion NewRotation;
    private Vector3 newPosition;
    private Vector3 zoomAmount = new Vector3(0f,-1f,1f);
    private Vector3 newZoom;
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;
    private float panSpeed = 0.5f;
    private float rotationSpeed = 1f;
    private float movementTime = 5f;
    private float minZoom = -5f;
    private float maxZoom = 20f;
    private float minPan = 0f;
    private float maxPan = 14f;

    private void Start() {
        newPosition = transform.position;
        NewRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    private void Update() {
        HandleMouseInput();
        HandleKeyBoardInput();
    }
    private void HandleMouseInput(){
        // Camera Zoom
        if(Input.mouseScrollDelta.y != 0){
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        newZoom.y = Mathf.Clamp(newZoom.y, -minZoom, maxZoom);
        newZoom.z = Mathf.Clamp(newZoom.z, -maxZoom, minZoom);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, movementTime * Time.deltaTime);
        // Camera Pan
        if(Input.GetMouseButtonDown(2)){
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry)){
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if(Input.GetMouseButton(2)){
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry)){
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        newPosition.x = Mathf.Clamp(newPosition.x, minPan, maxPan);
        newPosition.z = Mathf.Clamp(newPosition.z, -maxPan, minPan);
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
        // Camera Rotation
        if(Input.GetMouseButtonDown(1)){
            rotateStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(1)){
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;
            NewRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRotation, movementTime * Time.deltaTime);
    }
    private void HandleKeyBoardInput(){
        if(Input.GetKey(KeyCode.W)){
            newPosition += (transform.forward * panSpeed);
        }
        if(Input.GetKey(KeyCode.S)){
            newPosition += (transform.forward * -panSpeed);
        }
        if(Input.GetKey(KeyCode.D)){
            newPosition += (transform.right * panSpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            newPosition += (transform.right * -panSpeed);
        }
        if(Input.GetKey(KeyCode.Q)){
            NewRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
        }
        if(Input.GetKey(KeyCode.E)){
            NewRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
        }   
    }
}