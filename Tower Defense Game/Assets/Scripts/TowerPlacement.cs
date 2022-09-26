
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementCollideMask;
    [SerializeField] private LayerMask placementCheckMask;
    [SerializeField] private Material validPlacementMaterial;
    [SerializeField] private Material invalidPlacementMaterial;
    [SerializeField] private Player player;
    
    private GameObject currentPlacingTower;
    private GameObject currentTowerIndicator;
    private Ray ray;
    private RaycastHit hitInfo;
    private MeshRenderer towerMaterial;
    private GameObject selectedTower;
    private int layerTower;
    private TowerController selectedTowerScript;

    BuildManager buildManager;
    private void Start() {
        buildManager = BuildManager.instance;
        layerTower = LayerMask.NameToLayer("Towers");
    }

    private void Update()
    {
        HandleTowerSelection();
        if(buildManager.GetUIState() == true)
        {
            buildManager.CheckUpgradePaths();
        }
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
            return;
        }
        currentPlacingTower = _tower;
    }

    public void SetTowerIndicator(GameObject _currentTowerIndicator)
    {
        if(currentTowerIndicator != null)
        {
            Destroy(currentTowerIndicator.gameObject);
            currentTowerIndicator = null;
            return;
        }

        if(currentPlacingTower == null)
        {
            currentTowerIndicator = null;
            return;
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
        GameObject tower = currentPlacingTower;
        IPurchasble purchasble = tower.GetComponent<IPurchasble>();
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
                        player.DecreasePlayerGold(purchasble.GetTowerCost());
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

    private void HandleTowerSelection()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, 100f, ~layerTower) && buildManager.GetUIState()  == false)
        {
            selectedTower = hitInfo.transform.gameObject;
            selectedTowerScript = selectedTower.GetComponent<TowerController>();
            buildManager.ShowTowerUI(selectedTower,selectedTowerScript.GetTowerName(),selectedTowerScript.GetUpgradeOneCost(), selectedTowerScript.GetUpgradeTwoCost(), selectedTowerScript.GetUpgradeThreeCost());
        }
        else if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, 100f, ~layerTower) && hitInfo.transform.gameObject != selectedTower && buildManager.GetUIState()  == true)
        {
            selectedTower = hitInfo.transform.gameObject;
            selectedTowerScript = selectedTower.GetComponent<TowerController>();
            buildManager.ShowTowerUI(selectedTower,selectedTowerScript.GetTowerName(),selectedTowerScript.GetUpgradeOneCost(), selectedTowerScript.GetUpgradeTwoCost(), selectedTowerScript.GetUpgradeThreeCost());
        }
        else if(buildManager.GetUIState()  == true)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
            return;
            }
            if(Input.GetMouseButtonDown(0))
            {
                buildManager.HideTowerUI();
            }
        }
    }
}