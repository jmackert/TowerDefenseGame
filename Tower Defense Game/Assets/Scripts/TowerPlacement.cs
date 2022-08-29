using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementCollideMask;
    [SerializeField] private LayerMask placementCheckMask;
    [SerializeField] private Material validPlacementMaterial;
    [SerializeField] private Material invalidPlacementMaterial;
    private GameObject currentPlacingTower;
    private GameObject currentTowerIndicator;
    private Ray ray;
    private RaycastHit hitInfo;
    private MeshRenderer towerMaterial;

    void Update()
    {
        if(currentPlacingTower != null)
        {  
            MoveCurrentTowerToMouse();
            ChangeIndicatorColor();
            PlaceCurrentTower();
            DeselectCurrentTower();
        }
    }

    public void SetTowerToPlace(GameObject _tower)
    {
        if(currentPlacingTower != null)
        {
            currentPlacingTower = null;
        }
        currentPlacingTower = _tower;
    }

    public void SetTowerIndicator(GameObject _currentTowerIndicator)
    {
        if(currentTowerIndicator != null)
        {
            Destroy(currentTowerIndicator.gameObject);
            currentTowerIndicator = null;
        }
        Vector3 pos = new Vector3 (100f,0f,0f);
        currentTowerIndicator = Instantiate(_currentTowerIndicator, pos, Quaternion.identity);
    }

    private void MoveCurrentTowerToMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo, 100f, placementCheckMask))
        {
            currentTowerIndicator.transform.position = hitInfo.point;
        }
    }
    private void PlaceCurrentTower()
    {
        if(currentTowerIndicator != null)
        {
            if(Input.GetMouseButtonDown(0) && hitInfo.collider.gameObject != null)
            {
                if(!hitInfo.collider.gameObject.CompareTag("CantPlace"))
                {
                    BoxCollider towerCollider = currentTowerIndicator.gameObject.GetComponent<BoxCollider>();
                    towerCollider.isTrigger = true;

                    Vector3 boxCenter = currentTowerIndicator.gameObject.transform.position + towerCollider.center;
                    Vector3 halfExtents = towerCollider.size / 2;

                    if(!Physics.CheckBox(boxCenter, halfExtents, Quaternion.identity, placementCheckMask, QueryTriggerInteraction.Ignore))
                    {
                        towerCollider.isTrigger = false;
                        currentPlacingTower.transform.position = hitInfo.point;
                        Instantiate(currentPlacingTower,transform.position = hitInfo.point, Quaternion.identity);
                        currentPlacingTower = null;
                        Destroy(currentTowerIndicator.gameObject);

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
                Destroy(currentTowerIndicator.gameObject);
                currentPlacingTower = null;
                currentTowerIndicator = null;
            }
        }
    }

    private void ChangeIndicatorColor()
    {
        BoxCollider towerCollider = currentTowerIndicator.gameObject.GetComponent<BoxCollider>();
        towerCollider.isTrigger = true;

        Vector3 boxCenter = currentTowerIndicator.gameObject.transform.position + towerCollider.center;
        Vector3 halfExtents = towerCollider.size / 2;

        if(Physics.CheckBox(boxCenter, halfExtents, Quaternion.identity, placementCheckMask, QueryTriggerInteraction.Ignore) || hitInfo.collider.gameObject.CompareTag("CantPlace"))                    
        {
            towerMaterial = currentTowerIndicator.gameObject.GetComponentInChildren<MeshRenderer>();
            towerMaterial.material = invalidPlacementMaterial;
        }
        else
        {
            towerMaterial = currentTowerIndicator.gameObject.GetComponentInChildren<MeshRenderer>();
            towerMaterial.material = validPlacementMaterial;
        }
    }
}