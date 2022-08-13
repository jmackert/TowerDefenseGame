using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private GameObject CurrentPlacingTower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentPlacingTower != null)
        {
            Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(camRay, out RaycastHit hitInfo, 100f) && hitInfo.transform.tag != "Tower")
            {
                CurrentPlacingTower.transform.position = hitInfo.point;
            }
            if(Input.GetMouseButtonDown(0))
            {
                CurrentPlacingTower = null;
            }
        }
    }

    public void SetTowerToPlace(GameObject tower){
        CurrentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
}