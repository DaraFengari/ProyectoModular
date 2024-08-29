using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public Vector2Int size;
    public int squareNumber;
    public Vector2Int hallsSize = new Vector2Int(2, 3);

    [Range(0, 100)]
    public float relativeSquareSize = 10;

    public GameObject northWallSprite; // Asset para las paredes al norte
    public GameObject southWallSprite; // Asset para las paredes al sur
    public GameObject wallSprite;
    public GameObject floorSprite;
    public GameObject decoratorSprite;

    public Vector2Int spawnPoint;
    public bool playerPlaced = false;

    void Start()
    {
        int[,] dungeon = Dungeon.SpawnDungeon(size, squareNumber, 100 / relativeSquareSize, hallsSize, out List<Vector2Int> roomCenters);

        // Seleccionar una habitación aleatoria y obtener su centro
        int randomRoomIndex = Random.Range(0, roomCenters.Count);
        Vector2Int selectedRoomCenter = roomCenters[randomRoomIndex];

        // Desplazar la mazmorra de manera que el centro de la habitación seleccionada esté en (0, 0)
        InstantiateDungeon(dungeon, selectedRoomCenter);
    }

    void InstantiateDungeon(int[,] dungeon, Vector2Int selectedRoomCenter)
    {
        float spriteSizeX = wallSprite.GetComponent<SpriteRenderer>().bounds.size.x;
        float spriteSizeY = wallSprite.GetComponent<SpriteRenderer>().bounds.size.y;

        for (int i = 0; i < dungeon.GetLength(0); i++)
        {
            for (int j = 0; j < dungeon.GetLength(1); j++)
            {
                // Ajustar la posición con base en la habitación seleccionada centrada en (0, 0)
                Vector3 adjustedPosition = new Vector3((i - selectedRoomCenter.x) * spriteSizeX, (j - selectedRoomCenter.y) * spriteSizeY, 0);

                if (dungeon[i, j] == 0)
                {
                    if (CheckPosition(dungeon, new Vector2Int(i, j)))
                    {
                        // Detectar si la pared está al norte o al sur y asignar el sprite correspondiente
                        if (IsNorthWall(dungeon, i, j))
                        {
                            GameObject northWall = Instantiate(northWallSprite, adjustedPosition, Quaternion.identity);
                            northWall.transform.SetParent(transform);
                        }
                        else if (IsSouthWall(dungeon, i, j))
                        {
                            GameObject southWall = Instantiate(southWallSprite, adjustedPosition, Quaternion.identity);
                            southWall.transform.SetParent(transform);
                        }
                        else
                        {
                            GameObject wall = Instantiate(wallSprite, adjustedPosition, Quaternion.identity);
                            wall.transform.SetParent(transform);
                        }
                    }
                }
                else
                {
                    GameObject floor = Instantiate(floorSprite, adjustedPosition, Quaternion.identity);
                    floor.transform.SetParent(transform);

                    // Lógica de colocar al jugador
                    if (!playerPlaced)
                    {
                        spawnPoint = new Vector2Int(i, j);
                        playerPlaced = true;
                    }
                    else
                    {
                        int r = Random.Range(0, 100);
                        if (r < 1)
                        {
                            GameObject decorator = Instantiate(decoratorSprite, adjustedPosition, Quaternion.identity);
                            decorator.transform.SetParent(transform);
                        }
                    }
                }
            }
        }
    }

    bool IsNorthWall(int[,] dungeon, int x, int y)
    {
        // Verifica si está al borde norte y si el espacio encima está vacío
        return y + 1 < dungeon.GetLength(1) && dungeon[x, y + 1] == 0 && dungeon[x, y] == 0 && dungeon[x, y - 1] != 0;
    }

    bool IsSouthWall(int[,] dungeon, int x, int y)
    {
        // Verifica si está al borde sur y si el espacio debajo está vacío
        return y - 1 >= 0 && dungeon[x, y - 1] == 0 && dungeon[x, y] == 0 && dungeon[x, y + 1] != 0;
    }


    bool CheckPosition(int[,] dungeon, Vector2Int position)
    {
        if (position.x >= 1 &&
            position.y >= 1 &&
            position.y < dungeon.GetLength(1) - 1 &&
            position.x < dungeon.GetLength(0) - 1)
        {
            if (dungeon[position.x + 1, position.y] != 0) return true;
            if (dungeon[position.x - 1, position.y] != 0) return true;
            if (dungeon[position.x, position.y + 1] != 0) return true;
            if (dungeon[position.x, position.y - 1] != 0) return true;
        }
        return false;
    }
}

