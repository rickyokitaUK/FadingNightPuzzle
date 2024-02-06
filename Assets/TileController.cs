using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public GameObject tilePrefab;  // The tile prefab to be instantiated
    public Vector2 gridSize = new Vector2(4, 4);  // Size of the grid
    public float tileOffset = 240.0f;  // Offset between tiles
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);


    // List to store the instantiated tiles
    private List<GameObject> tiles = new List<GameObject>();


    void Start()
    {
        GenerateTiles();

        foreach (GameObject tile in tiles)
        {
            SetTileStatus(tile, false); // set off
        }
    }

    void SetTileStatus(GameObject tile, bool status_on)
    {
        // Find the child named "redImg" under the tile
        Transform redImgTransform = tile.transform.Find("redring");
        Transform blueImgTransform = tile.transform.Find("bluering");

        // Check if the child with the specified name exists
        if (redImgTransform != null && blueImgTransform != null)
        {
            // Activate or deactivate the child GameObject based on your needs
            if (status_on)
            {

                blueImgTransform.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                blueImgTransform.gameObject.LeanScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f);

                redImgTransform.gameObject.SetActive(false);
                blueImgTransform.gameObject.SetActive(true);
            }
            else
            {
                redImgTransform.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                redImgTransform.gameObject.LeanScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f);

                redImgTransform.gameObject.SetActive(true);
                blueImgTransform.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Child not found under the tile.");
        }
    }

    void GenerateTiles()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                // Calculate the position of each tile with offset
                Vector3 tilePosition = new Vector3(i * tileOffset + offset.x , j * tileOffset + offset.y, 0 + offset.z);

                // Instantiate a tile at the calculated position
                GameObject tileObject = Instantiate(tilePrefab, tilePosition, Quaternion.identity);


                BoxCollider2D boxColliderObject = tileObject.AddComponent<BoxCollider2D>();

                // Optional: You can parent the tiles to the generator for better organization
                tileObject.transform.parent = transform;


                // Add the instantiated tile to the list
                tiles.Add(tileObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        //  PlayOverlayAnimation();
    } 
    
    private void PlayOverlayAnimation()
    {
    }
}

