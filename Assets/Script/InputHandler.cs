using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    #region Variables

    private Camera _mainCamera;
    private TileController _tileController;

    #endregion

    private void Awake()
    {
        _mainCamera = Camera.main;
        _tileController = FindObjectOfType<TileController>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (rayHit.collider)
        { 
            Debug.Log(rayHit.collider.gameObject.name);

            // Check if the collider belongs to a tile
            GameObject hitObject = rayHit.collider.gameObject;
            int tileIndex = _tileController.GetTileIndex(hitObject);
            if (tileIndex != -1)
            {
                Debug.Log("Clicked on tile at index: " + tileIndex);
                // Perform any action you need with the index
                _tileController.ToggleTile(hitObject);
            }
            else
            {
                Debug.Log("Clicked on something other than a tile.");
            }
        }
        return;

       
    }
}