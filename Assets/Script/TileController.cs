using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileController : MonoBehaviour
{
    public GameObject tilePrefab;  // The tile prefab to be instantiated
    public Vector2 gridSize = new Vector2(4, 4);  // Size of the grid
    public float tileOffset = 240.0f;  // Offset between tiles
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
    public bool gameOver = false;
    public int moves = 0;

    public GameObject timer;
    [SerializeField] public TextMeshProUGUI uiMoveText;

    // List to store the instantiated tiles
    private List<GameObject> tiles = new List<GameObject>();

    void Start()
    {
        gameOver = false;
        moves = 0;

        GenerateTiles();

        ResetTiles();

        uiMoveText.text = $"{moves}";
    }

    public void ResetTiles()
    {
        if (gameOver) // disable function if gameover
            return;

        foreach (GameObject tile in tiles)
        {
            SetTileStatus(tile, false); // set off
        }
    }

    public void ToggleTile(GameObject tile)
    {

        if (gameOver) // disable function if gameover
            return;

        bool tileon_status = tile.gameObject.GetComponent<PrefabProfile>().tile_on;
        SetTileStatus(tile, !tileon_status);

        // set the adjacent
        // Get the index of the hit tile
        int hitIndex = GetTileIndex(tile);

        // Calculate the indices of adjacent tiles
        int gridSizeX = (int)gridSize.x;
        int gridSizeY = (int)gridSize.y;

        int leftIndex = hitIndex - 1;
        int rightIndex = hitIndex + 1;
        int upIndex = hitIndex + gridSizeX;
        int downIndex = hitIndex - gridSizeY;

        // Toggle left tile if it exists and not at the left edge
        if (leftIndex >= 0 && leftIndex % gridSizeX != gridSizeX - 1)
        {
            tileon_status = tiles[leftIndex].gameObject.GetComponent<PrefabProfile>().tile_on;
            SetTileStatus(tiles[leftIndex], !tileon_status);
        }

        // Toggle right tile if it exists and not at the right edge
        if (rightIndex < tiles.Count && rightIndex % gridSizeX != 0)
        {
            tileon_status = tiles[rightIndex].gameObject.GetComponent<PrefabProfile>().tile_on;
            SetTileStatus(tiles[rightIndex], !tileon_status);
        }

        // Toggle up tile if it exists
        if (upIndex < tiles.Count)
        {
            tileon_status = tiles[upIndex].gameObject.GetComponent<PrefabProfile>().tile_on;
            SetTileStatus(tiles[upIndex], !tileon_status);
        }

        // Toggle down tile if it exists
        if (downIndex >= 0)
        {
            tileon_status = tiles[downIndex].gameObject.GetComponent<PrefabProfile>().tile_on;
            SetTileStatus(tiles[downIndex], !tileon_status);
        }
        moves++;
        uiMoveText.text = $"{moves}";
    }


    public void SetTileStatus(GameObject tile, bool status_on)
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

                tile.gameObject.GetComponent<PrefabProfile>().tile_on = true;
            }
            else
            {
                redImgTransform.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                redImgTransform.gameObject.LeanScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f);

                redImgTransform.gameObject.SetActive(true);
                blueImgTransform.gameObject.SetActive(false);

                tile.gameObject.GetComponent<PrefabProfile>().tile_on = false;
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

    public int GetTileIndex(GameObject tile)
    {
        return tiles.IndexOf(tile);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkGameFinish())
        {
            Debug.Log("Finish");
            gameOver = true;

            timer.GetComponent<Timer>().OnPause();
        }
    }

    // check all the tiles are status on, show finish and timer
    private bool checkGameFinish()
    {
        foreach (GameObject tile in tiles)
        {
            bool status_on = tile.gameObject.GetComponent<PrefabProfile>().tile_on;
            if (!status_on)
            {
                return false;
            }
        }
        return true;

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

