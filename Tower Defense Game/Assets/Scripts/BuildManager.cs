using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake() {
        if (instance != null){
            Debug.LogError("More one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    private GameObject towerToBuild;
    public GameObject archerTower;

    public GameObject GetTowerToBuild(){
        return towerToBuild;
    }
    private void Start() {
        towerToBuild = archerTower;
    }
}
