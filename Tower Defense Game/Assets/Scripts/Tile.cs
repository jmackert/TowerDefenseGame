using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    BuildManager buildManager;
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject tower;
    private Vector3 offSet = new Vector3(0f,1.25f,0f);

    private void Start(){
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    private void OnMouseEnter() {
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if(buildManager.GetTowerToBuild() == null){
            return;
        }
        rend.material.color = hoverColor;
    }
    private void OnMouseExit() {
        rend.material.color = startColor;
    }
    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if(buildManager.GetTowerToBuild() == null){
            return;
        }
        if(tower != null)
        {
            Debug.Log("Can't Place Turret Here - TODO: Add to UI");
            return;
        }
        GameObject towerToBuild = buildManager.GetTowerToBuild();
        tower = (GameObject)Instantiate(towerToBuild, transform.position + offSet, Quaternion.identity);
    }
}
