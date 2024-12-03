using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonVisualizerGA : MonoBehaviour
{
    public static DungeonVisualizerGA dungeon;
    public DungeonGeneratorGA generator;
    public Tilemap tilemap; // Tilemap para dibujar los tiles de la mazmorra
    public RuleTile floorTile;  // El tile para el suelo de las habitaciones y pasillos
    public RuleTile wallTile;   // El tile para las paredes de las habitaciones
    public RuleTile sideTile;

    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject enemy2Prefab;
    public GameObject decorationPrefab;
    public GameObject exitPrefab;

    public int maxEnemies = 5;
    public int maxEnemies2 = 2;
    public int maxCoins = 10;
    public int maxDecorations = 3;

    // Tamaño de cada tile en unidades del mundo
    public float tileSize = 1.0f; // Ajusta esto según el tamaño real de las tiles (por ejemplo, 1.0 si cada tile es 1 unidad)

    void Start()
    {
        Dungeon bestDungeon = generator.RunGeneticAlgorithm();
        if (bestDungeon != null)
        {
            VisualizeDungeon(bestDungeon);
            VisualizeCorridors(bestDungeon);
            PlaceElements(bestDungeon);
        }
        else
        {
            Debug.LogError("No se ha generado una mazmorra.");
        }
    }

    // Visualiza las habitaciones usando un Tilemap
    private void VisualizeDungeon(Dungeon dungeon)
    {
        foreach (Room room in dungeon.Rooms)
        {
            // Dibujar el suelo de la habitación
            for (int x = 0; x < room.Size.x; x += 16)
            {
                for (int y = 0; y < room.Size.y; y += 16)
                {
                    Vector3Int tilePosition = new Vector3Int(
                        Mathf.FloorToInt((room.Position.x + x)),
                        Mathf.FloorToInt((room.Position.y + y)),
                        0
                    );

                    // Colocar el tile de suelo si la posición está libre
                    if (tilemap.GetTile(tilePosition) == null)
                    {
                        tilemap.SetTile(tilePosition, floorTile);
                    }
                }
            }

            // Dibujar las paredes alrededor de la habitación
            CreateWalls(room,dungeon);
        }
    }

    // Visualiza los pasillos usando un Tilemap
    // Visualiza los pasillos usando un Tilemap
    private void VisualizeCorridors(Dungeon dungeon)
    {
        int corridorWidth = 3; // Define el grosor del pasillo (en tiles)

        foreach (Connection corridor in dungeon.Connections)
        {
            Vector2Int start = corridor.start;
            Vector2Int end = corridor.end;

            // Dibujar pasillo horizontal
            if (start.y == end.y)
            {
                for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x += 16)
                {
                    bool isInRoom = IsPositionInsideRoom(new Vector2Int(x, start.y), dungeon);

                    // Dibujar el suelo del pasillo solo si no está en una habitación
                    if (!isInRoom)
                    {
                        for (int i = -corridorWidth / 2; i <= corridorWidth / 2; i++)
                        {
                            Vector3Int tilePosition = new Vector3Int(
                                Mathf.FloorToInt(x),
                                Mathf.FloorToInt(start.y + i * 16), // Añadir grosor en el eje Y
                                0
                            );

                            if (tilemap.GetTile(tilePosition) == null)
                            {
                                tilemap.SetTile(tilePosition, floorTile);
                            }
                        }

                        // Añadir un tile extra hacia la izquierda o derecha
                        if (start.x < end.x) // Pasillo hacia la derecha
                        {
                            Vector3Int extraTilePosition = new Vector3Int(
                                Mathf.FloorToInt(end.x + 16), // Añadir un tile hacia la derecha
                                Mathf.FloorToInt(start.y),
                                0
                            );
                            if (tilemap.GetTile(extraTilePosition) == null)
                            {
                                tilemap.SetTile(extraTilePosition, floorTile);
                            }
                        }
                        else if (start.x > end.x) // Pasillo hacia la izquierda
                        {
                            Vector3Int extraTilePosition = new Vector3Int(
                                Mathf.FloorToInt(start.x - 16), // Añadir un tile hacia la izquierda
                                Mathf.FloorToInt(start.y),
                                0
                            );
                            if (tilemap.GetTile(extraTilePosition) == null)
                            {
                                tilemap.SetTile(extraTilePosition, floorTile);
                            }
                        }

                        // Crear paredes solo en los bordes del pasillo
                        Vector3Int topWallPosition = new Vector3Int(
                            Mathf.FloorToInt(x),
                            Mathf.FloorToInt(start.y + (corridorWidth / 2 + 1) * 16), // Pared por arriba del pasillo
                            0
                        );

                        Vector3Int bottomWallPosition = new Vector3Int(
                            Mathf.FloorToInt(x),
                            Mathf.FloorToInt(start.y - (corridorWidth / 2 + 1) * 16), // Pared por debajo del pasillo
                            0
                        );

                        if (tilemap.GetTile(topWallPosition) == null)
                        {
                            tilemap.SetTile(topWallPosition, wallTile);
                        }

                        if (tilemap.GetTile(bottomWallPosition) == null)
                        {
                            tilemap.SetTile(bottomWallPosition, wallTile);
                        }
                    }
                }
            }

            // Dibujar pasillo vertical (sin cambios)
            if (start.x == end.x)
            {
                for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y += 16)
                {
                    

                    // Dibujar el suelo del pasillo solo si no está en una habitación
                    
                        for (int i = -corridorWidth / 2; i <= corridorWidth / 2; i++)
                        {
                            Vector3Int tilePosition = new Vector3Int(
                                Mathf.FloorToInt(start.x + i * 16), // Añadir grosor en el eje X
                                Mathf.FloorToInt(y),
                                0
                            );

                            if (tilemap.GetTile(tilePosition) == null)
                            {
                                tilemap.SetTile(tilePosition, floorTile);
                            }
                        }

                        // Crear paredes solo en los bordes del pasillo
                        Vector3Int leftWallPosition = new Vector3Int(
                            Mathf.FloorToInt(start.x - (corridorWidth / 2 + 1) * 16), // Pared a la izquierda del pasillo
                            Mathf.FloorToInt(y),
                            0
                        );

                        Vector3Int rightWallPosition = new Vector3Int(
                            Mathf.FloorToInt(start.x + (corridorWidth / 2 + 1) * 16), // Pared a la derecha del pasillo
                            Mathf.FloorToInt(y),
                            0
                        );

                        if (tilemap.GetTile(leftWallPosition) == null)
                        {
                            tilemap.SetTile(leftWallPosition, sideTile);
                        }

                        if (tilemap.GetTile(rightWallPosition) == null)
                        {
                            tilemap.SetTile(rightWallPosition, sideTile);
                        }
                    
                }
            }
        }
    }

    // Verifica si una posición está dentro de una habitación
    private bool IsPositionInsideRoom(Vector2Int position, Dungeon dungeon)
    {
        foreach (Room room in dungeon.Rooms)
        {
            if (position.x >= room.Position.x && position.x < room.Position.x + room.Size.x &&
                position.y >= room.Position.y && position.y < room.Position.y + room.Size.y)
            {
                return true; // La posición está dentro de una habitación
            }
        }
        return false; // La posición está fuera de cualquier habitación
    }

    

    // Crear paredes alrededor de una habitación usando el Tilemap
    // Crear paredes alrededor de una habitación usando el Tilemap
    private void CreateWalls(Room room, Dungeon dungeon)
    {
        int corridorWidth = 3; // Ancho del pasillo en tiles

        // Paredes a la izquierda y derecha
        for (int y = 16; y < room.Size.y; y += 16)
        {
            Vector3Int leftWallPosition = new Vector3Int(
                Mathf.FloorToInt((room.Position.x - 1)),
                Mathf.FloorToInt((room.Position.y + y)),
                0
            );

            Vector3Int rightWallPosition = new Vector3Int(
                Mathf.FloorToInt((room.Position.x + room.Size.x - 1)),
                Mathf.FloorToInt((room.Position.y + y)),
                0
            );

            // Omitir la pared si hay una conexión de pasillo
            if (!IsCorridorAtPosition(new Vector2Int(room.Position.x - 1, room.Position.y + y), dungeon, corridorWidth))
            {
                if (tilemap.GetTile(leftWallPosition) == null)
                {
                    tilemap.SetTile(leftWallPosition, sideTile);
                }
            }

            if (!IsCorridorAtPosition(new Vector2Int(room.Position.x + room.Size.x - 1, room.Position.y + y), dungeon, corridorWidth))
            {
                if (tilemap.GetTile(rightWallPosition) == null)
                {
                    tilemap.SetTile(rightWallPosition, sideTile);
                }
            }
        }

        // Paredes superior e inferior
        for (int x = 0; x <= room.Size.x; x += 16)
        {
            Vector3Int topWallPosition = new Vector3Int(
                Mathf.FloorToInt((room.Position.x - 1 + x)),
                Mathf.FloorToInt((room.Position.y + room.Size.y)),
                0
            );

            Vector3Int bottomWallPosition = new Vector3Int(
                Mathf.FloorToInt((room.Position.x - 1 + x)),
                Mathf.FloorToInt((room.Position.y)),
                0
            );

            if (!IsCorridorAtPosition(new Vector2Int(room.Position.x + x, room.Position.y + room.Size.y), dungeon, corridorWidth))
            {
                if (tilemap.GetTile(topWallPosition) == null)
                {
                    tilemap.SetTile(topWallPosition, wallTile);
                }
            }

            if (!IsCorridorAtPosition(new Vector2Int(room.Position.x + x, room.Position.y), dungeon, corridorWidth))
            {
                if (tilemap.GetTile(bottomWallPosition) == null)
                {
                    tilemap.SetTile(bottomWallPosition, wallTile);
                }
            }
        }
    }

    // Verifica si en una posición de pared hay un pasillo
    private bool IsCorridorAtPosition(Vector2Int wallPosition, Dungeon dungeon, int corridorWidth)
    {
        foreach (Connection corridor in dungeon.Connections)
        {
            Vector2Int start = corridor.start;
            Vector2Int end = corridor.end;

            // Revisa si la pared está en una posición donde hay un pasillo horizontal o vertical
            if (start.y == end.y) // Pasillo horizontal
            {
                if (Mathf.Abs(wallPosition.y - start.y) <= corridorWidth / 2 * 16 &&
                    wallPosition.x >= Mathf.Min(start.x, end.x) &&
                    wallPosition.x <= Mathf.Max(start.x, end.x))
                {
                    return true;
                }
            }
            else if (start.x == end.x) // Pasillo vertical
            {
                if (Mathf.Abs(wallPosition.x - start.x) <= corridorWidth / 2 * 16 &&
                    wallPosition.y >= Mathf.Min(start.y, end.y) &&
                    wallPosition.y <= Mathf.Max(start.y, end.y))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Coloca enemigos, monedas, decoraciones y la salida
    // Coloca enemigos, monedas y decoraciones en la mazmorra
    private void PlaceElements(Dungeon dungeon)
    {
        // Colocar salida a la izquierda del centro
        Vector3 exitPosition = new Vector3(
            dungeon.Rooms[0].Position.x/2, // A la izquierda del centro
            dungeon.Rooms[0].Position.y/2, 0);

        Instantiate(exitPrefab, exitPosition, Quaternion.identity);

        // Colocar enemigos, monedas y decoraciones de manera aleatoria
        for (int i = 0; i < maxEnemies; i++)
        {
            PlaceRandomElement(enemyPrefab, dungeon);
        }

        for (int i = 0; i < maxCoins; i++)
        {
            PlaceRandomElement(coinPrefab, dungeon);
        }

        for (int i = 0; i < maxDecorations; i++)
        {
            PlaceRandomElement(decorationPrefab, dungeon);
        }

        for (int i = 0; i < maxEnemies2; i++)
        {
            PlaceRandomElement(enemy2Prefab, dungeon);
        }

    }

    // Coloca un objeto en una posición aleatoria de una habitación
    // Coloca un objeto en una posición aleatoria de una habitación, asegurándote de que no esté en los bordes
    private void PlaceRandomElement(GameObject prefab, Dungeon dungeon)
    {
        Room randomRoom = dungeon.Rooms[Random.Range(1, dungeon.Rooms.Count)];

        // Definir un margen para evitar los bordes de la habitación
        float margin = 16.0f * tileSize; // Ajusta el margen según el tamaño que quieras

        // Asegúrate de que la posición aleatoria esté dentro de los márgenes
        Vector3 randomPosition = new Vector3(
            randomRoom.Position.x + margin + Random.Range(0, randomRoom.Size.x - 2 * margin) * tileSize,
            randomRoom.Position.y + margin + Random.Range(0, randomRoom.Size.y - 2 * margin) * tileSize,
            0
        );

        Instantiate(prefab, randomPosition, Quaternion.identity);
    }

}