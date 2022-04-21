using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject tower;
    private Vector3 offSet = new Vector3(0f,1.25f,0f);

    private void Start(){
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    private void OnMouseEnter() {
        rend.material.color = hoverColor;
    }
    private void OnMouseExit() {
        rend.material.color = startColor;
    }
    private void OnMouseDown() {
        if(tower != null)
        {
            Debug.Log("Can't Place Turret Here - TODO: Add to UI");
            return;
        }
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        tower = (GameObject)Instantiate(towerToBuild, transform.position + offSet, Quaternion.identity);
    }
}
