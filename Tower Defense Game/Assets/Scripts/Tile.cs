using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    /*BuildManager buildManager;
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    [Header("Optional")]
    public GameObject tower;
    private Vector3 offSet = new Vector3(0f,1.25f,0f);

    private void Start(){
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition(){
        return transform.position + offSet;
    }
    private void OnMouseEnter() {
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if(buildManager.CanBuild){
            rend.material.color = hoverColor;
        }
    }
    private void OnMouseExit() {
        rend.material.color = startColor;
    }
    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if(buildManager.isTowerUIOpen == true){
            buildManager.HideTowerUI();
            return;
        }
        if(tower != null){
            Debug.Log("Can't Place Turret Here - TODO: Add to UI");
            return;
        }       
        if(buildManager.CanBuild){
            buildManager.BuildTowerOn(this);;
        }
    }*/
}
