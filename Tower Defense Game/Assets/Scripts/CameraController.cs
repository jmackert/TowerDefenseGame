using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 15f;
    public float panBoarderThickness = 15f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 20f;
    public float minX = 0f;
    public float maxX = 15f;
    public float minZ = -20f;
    public float maxZ = -10f;    

    private void Update()
    {
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness){
            transform.Translate(Vector3.forward.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness){
            transform.Translate(Vector3.back.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("a") || Input.mousePosition.x <= Screen.width - panBoarderThickness){
            transform.Translate(Vector3.left.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("d") || Input.mousePosition.x >= panBoarderThickness){
            transform.Translate(Vector3.right.normalized * panSpeed * Time.deltaTime, Space.World);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}
