using UnityEngine;
using UnityEngine.Serialization;

public class BuidingGrid : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(Platform.Width, Platform.Height);

    private Tower[,] _grid;
    private Tower _flyingBuilding;
    public Camera mainCamera;
    public int towerType;
    
    private void Awake()
    {
        _grid = new Tower[gridSize.x, gridSize.y];
        
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Tower buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }
        _flyingBuilding = Instantiate(buildingPrefab);

    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 1 || x > gridSize.x - _flyingBuilding.size.x-1) available = false;
                if (y < 1 || y > gridSize.y - _flyingBuilding.size.y-1) available = false;
                if(towerType == 2)
                {
                    if(Platform._terrain[y,x,1] != 12) available = false;
                }
                else
                {
                    if(x >= 1 && y >= 1 && x <= Platform.Width - 1 && y <= Platform.Height - 1 )
                    {
                        if(Platform._terrain[y,x,0] != 0 ) available = false;
                        
                    }
                }

                if (available && IsPlaceTaken(x, y)) available = false;

                _flyingBuilding.transform.position = new Vector3(x, 0.3f, y);
                _flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.size.y; y++)
            {
                if (_grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }
        
        _flyingBuilding.SetNormal();
        _flyingBuilding = null;
    }
}