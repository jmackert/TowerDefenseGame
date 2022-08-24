using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementCollideMask;
    [SerializeField] private LayerMask placementCheckMask;
    private GameObject currentPlacingTower;
    private Ray ray;
    private RaycastHit hitInfo;

    void Update()
    {
        if(currentPlacingTower != null)
        {
            MoveCurrentTowerToMouse();
            PlaceCurrentTower();
            DeselectCurrentTower();
            /*Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(camRay, out RaycastHit hitInfo, 100f) && hitInfo.transform.tag != "Tower")
            {
                currentPlacingTower.transform.position = hitInfo.point;
            }
            if(Input.GetMouseButtonDown(0))
            {
                currentPlacingTower = null;
            }*/
        }
    }

    public void SetTowerToPlace(GameObject tower){
        currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }

    private void MoveCurrentTowerToMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        //if(Physics.Raycast(ray, out hitInfo, 100f, placementCollideMask)) //&& hitInfo.transform.tag == "PlaceableLand")
        if(Physics.Raycast(ray, out hitInfo, 100f, placementCheckMask)) //&& hitInfo.transform.tag == "PlaceableLand")
        {
            currentPlacingTower.transform.position = hitInfo.point;
        }
    }

    private void PlaceCurrentTower()
    {
        if(currentPlacingTower != null)
        {
            if(Input.GetMouseButtonDown(0) && hitInfo.collider.gameObject != null)
            {
                if(!hitInfo.collider.gameObject.CompareTag("CantPlace"))
                {
                    BoxCollider towerCollider = currentPlacingTower.gameObject.GetComponent<BoxCollider>();
                    towerCollider.isTrigger = true;

                    Vector3 boxCenter = currentPlacingTower.gameObject.transform.position + towerCollider.center;
                    Vector3 halfExtents = towerCollider.size / 2;

                    if(!Physics.CheckBox(boxCenter, halfExtents, Quaternion.identity, placementCheckMask, QueryTriggerInteraction.Ignore))
                    {
                        towerCollider.isTrigger = false;
                        currentPlacingTower = null;
                    }
                }
            }
        }
    }
    
    private void DeselectCurrentTower()
    {
        if(currentPlacingTower != null)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Destroy(currentPlacingTower.gameObject);
            }
        }
    }
}