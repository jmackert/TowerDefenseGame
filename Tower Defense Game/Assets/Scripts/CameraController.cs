using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    private float panSpeed = 0.5f;
    private float rotationSpeed = 1f;
    private float movementTime = 5f;

    private Quaternion NewRotation;
    public Vector3 newPosition;
    private Vector3 zoomAmount = new Vector3(0f,-1f,1f);
    public Vector3 newZoom;
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    private void Start() {
        newPosition = transform.position;
        NewRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    private void Update() {
        HandleMouseInput();
        HandleMovementInput();
    }
    private void HandleMouseInput(){
        if(Input.mouseScrollDelta.y != 0){
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, movementTime * Time.deltaTime);
        if(Input.GetMouseButtonDown(0)){
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry)){
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if(Input.GetMouseButtonDown(0)){
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry)){
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }
    private void HandleMovementInput(){
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
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRotation, movementTime * Time.deltaTime);
        
    }
}
    /*public float panSpeed = 15f;
    public float panBoarderThickness = 15f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 20f;
    public float minX = 0f;
    public float maxX = 15f;
    public float minZ = -20f;
    public float maxZ = -10f;    
    public float rotateSpeed = 25f;

    private void Update()
    {
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness){
            transform.Translate(Vector3.forward.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness){
            transform.Translate(Vector3.back.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness){
            transform.Translate(Vector3.left.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness){
            transform.Translate(Vector3.right.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("q")){
            transform.Rotate(Vector3.down.normalized * rotateSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("e")){
            transform.Rotate(Vector3.up.normalized * rotateSpeed * Time.deltaTime, Space.World);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 700 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }*/