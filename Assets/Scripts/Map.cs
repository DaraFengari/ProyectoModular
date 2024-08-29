using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Vector2Int
{
    public int x;
    public int y;

    public Vector2Int(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

}

static class Dungeon
{
    static public int[,] SpawnDungeon(Vector2Int size, int nbrSquare4Generation, float dpc, Vector2Int hallsSize, out List<Vector2Int> roomCenters)
    {
        int[,] DungeonMap;
        int maxRetries = 100; // Limitar el número de intentos
        int retryCount = 0;

        do
        {
            // Generar las habitaciones
            DungeonMap = CreateDungeonSquares(size.x, size.y, nbrSquare4Generation, dpc, out roomCenters);
            int nbrRoom = FindRooms(DungeonMap, size.x, size.y);
            
            // Verificar que haya más de una habitación
            if (nbrRoom > 1)
            {
                DungeonMap = CreateDungeonHalls(DungeonMap, size.x, size.y, nbrRoom, hallsSize);
            }
            else
            {
                Debug.LogWarning("Menos de 2 habitaciones encontradas, reintentando...");
            }

            retryCount++;
            if (retryCount >= maxRetries)
            {
                Debug.LogError("Número máximo de intentos alcanzado.");
                break;
            }
        } while (!AllRoomsConnected(DungeonMap, size.x, size.y) && retryCount < maxRetries);

        return DungeonMap;
    }

    static int[,] CreateDungeonSquares(int sizeX, int sizeZ, int squareNbr, float dpc, out List<Vector2Int> roomCenters)
    {
        int[,] newMap = new int[sizeX, sizeZ];
        roomCenters = new List<Vector2Int>();

        for (int i = 0; i < squareNbr; i++)
        {
            int roomSizeX = Random.Range(10, Mathf.RoundToInt(sizeX / dpc));
            int roomSizeZ = Random.Range(10, Mathf.RoundToInt(sizeZ / dpc));
            int roomPosX = Random.Range(2, sizeX - roomSizeX - 1);
            int roomPosZ = Random.Range(2, sizeZ - roomSizeZ - 1);

            for (int j = roomPosX; j < roomPosX + roomSizeX; j++)
            {
                for (int k = roomPosZ; k < roomPosZ + roomSizeZ; k++)
                {
                    if (j >= 0 && j < sizeX && k >= 0 && k < sizeZ) // Verificación de límites
                    {
                        newMap[j, k] = 1;
                    }
                }
            }

            // Calcular el centro de la habitación
            Vector2Int roomCenter = new Vector2Int(roomPosX + roomSizeX / 2, roomPosZ + roomSizeZ / 2);
            roomCenters.Add(roomCenter);
        }
        return newMap;
    }

    static int FindRooms(int[,] dungeonMap, int sizeX, int sizeZ)
    {
        List<Vector2> ListCoordToTest = new List<Vector2>();
        int[,] modifiedMap = new int[sizeX, sizeZ];
        int nbrRoomFound = 0;

        System.Array.Copy(dungeonMap, modifiedMap, sizeX * sizeZ);

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                if (modifiedMap[i, j] == 1)
                {
                    ListCoordToTest.Add(new Vector2(i, j));
                    while (ListCoordToTest.Count > 0)
                    {
                        int x = (int)ListCoordToTest[0].x;
                        int z = (int)ListCoordToTest[0].y;
                        ListCoordToTest.RemoveAt(0);
                        dungeonMap[x, z] = nbrRoomFound + 1;

                        // Verificar límites
                        for (int xAround = Mathf.Max(x - 1, 0); xAround <= Mathf.Min(x + 1, sizeX - 1); xAround++)
                        {
                            for (int zAround = Mathf.Max(z - 1, 0); zAround <= Mathf.Min(z + 1, sizeZ - 1); zAround++)
                            {
                                if (modifiedMap[xAround, zAround] == 1)
                                {
                                    ListCoordToTest.Add(new Vector2(xAround, zAround));
                                    modifiedMap[xAround, zAround] = 0;
                                }
                            }
                        }
                    }
                    nbrRoomFound++;
                }
            }
        }
        return nbrRoomFound;
    }

    static private int[,] CreateDungeonHalls(int[,] dungeonMap, int sizeX, int sizeZ, int nbrRoom, Vector2Int hallSize)
    {
        // Array para rastrear el conjunto al que pertenece cada sala
        int[] roomSet = new int[nbrRoom + 1];
        for (int i = 0; i <= nbrRoom; i++)
        {
            roomSet[i] = i; // Inicialmente, cada sala es su propio conjunto
        }

        int x1, z1, x2, z2;

        for (int curRoomNbr = 1; curRoomNbr <= nbrRoom; curRoomNbr++)
        {
            // Buscamos conectar salas solo si no están conectadas
            for (int targetRoomNbr = 1; targetRoomNbr <= nbrRoom; targetRoomNbr++)
            {
                if (FindSet(roomSet, curRoomNbr) != FindSet(roomSet, targetRoomNbr)) // Solo conectar si pertenecen a conjuntos diferentes
                {
                    // Selección de puntos dentro de las salas para conectar
                    x1 = Random.Range(1, sizeX - 1);
                    z1 = Random.Range(1, sizeZ - 1);

                    while (dungeonMap[x1, z1] != curRoomNbr)
                    {
                        x1 = Random.Range(1, sizeX - 1);
                        z1 = Random.Range(1, sizeZ - 1);
                    }

                    x2 = Random.Range(1, sizeX - 1);
                    z2 = Random.Range(1, sizeZ - 1);

                    while (dungeonMap[x2, z2] != targetRoomNbr)
                    {
                        x2 = Random.Range(1, sizeX - 1);
                        z2 = Random.Range(1, sizeZ - 1);
                    }

                    // Creación del pasillo
                    CreateHall(dungeonMap, x1, z1, x2, z2, sizeX, sizeZ, hallSize);

                    // Unión de conjuntos (una vez conectadas, pertenecen al mismo conjunto)
                    UnionSets(roomSet, curRoomNbr, targetRoomNbr);
                }
            }
        }
        return dungeonMap;
    }

    static void CreateHall(int[,] dungeonMap, int x1, int z1, int x2, int z2, int sizeX, int sizeZ, Vector2Int hallSize)
    {
        int diffX = x2 - x1;
        int diffZ = z2 - z1;

        int xDirection = diffX != 0 ? diffX / Mathf.Abs(diffX) : 0;
        int zDirection = diffZ != 0 ? diffZ / Mathf.Abs(diffZ) : 0;

        int hallWidth = Random.Range(hallSize.x, hallSize.y);
        int hallHeight = Random.Range(hallSize.x, hallSize.y);

        // Creación del pasillo en dirección X y luego Z, asegurando que no accedemos fuera de los límites
        for (int i = x1; i != x1 + xDirection * hallWidth && i >= 0 && i < sizeX; i += xDirection)
        {
            for (int j = z1; j != z2 && j >= 0 && j < sizeZ; j += zDirection)
            {
                if (dungeonMap[i, j] == 0)
                {
                    dungeonMap[i, j] = -1;
                }
            }
        }

        for (int i = x1; i != x2 && i >= 0 && i < sizeX; i += xDirection)
        {
            for (int j = z2; j != z2 + zDirection * hallHeight && j >= 0 && j < sizeZ; j += zDirection)
            {
                if (dungeonMap[i, j] == 0)
                {
                    dungeonMap[i, j] = -1;
                }
            }
        }
    }

    // Funciones de Union-Find para manejar conjuntos disjuntos

    static int FindSet(int[] roomSet, int room)
    {
        if (roomSet[room] != room)
        {
            roomSet[room] = FindSet(roomSet, roomSet[room]); // Compresión de caminos
        }
        return roomSet[room];
    }

    static void UnionSets(int[] roomSet, int room1, int room2)
    {
        int root1 = FindSet(roomSet, room1);
        int root2 = FindSet(roomSet, room2);

        if (root1 != root2)
        {
            roomSet[root1] = root2; // Unión de conjuntos
        }
    }

    static bool AllRoomsConnected(int[,] dungeonMap, int sizeX, int sizeZ)
    {
        int firstRoomNumber = dungeonMap[0, 0];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                if (dungeonMap[i, j] > 0 && dungeonMap[i, j] != firstRoomNumber)
                {
                    return false;
                }
            }
        }

        return true;
    }
}





