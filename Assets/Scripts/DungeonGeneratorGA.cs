using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorGA : MonoBehaviour
{
    public static DungeonGeneratorGA gener;
    public int populationSize = 20;
    public int maxGenerations = 100;
    public int mutationRate = 5;

    public Vector2Int dungeonSize = new Vector2Int();
    public Vector2Int minRoomSize = new Vector2Int();
    public Vector2Int maxRoomSize = new Vector2Int();

    private List<Dungeon> population = new List<Dungeon>();


    void GeneratePopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Dungeon dungeon = new Dungeon(dungeonSize, minRoomSize, maxRoomSize);
            if (dungeon.Rooms.Count > 0) // Asegurarse de que la mazmorra sea válida
            {
                population.Add(dungeon);
            }
            else
            {
                i--; // Volver a intentar si la mazmorra es inválida
            }
        }
    }

    public Dungeon RunGeneticAlgorithm()
    {
        GeneratePopulation();

        for (int Gen = 0; Gen < maxGenerations; Gen++)
        {
            List<Dungeon> newGeneration = new List<Dungeon>();

            // Evaluar cada mazmorra en la población actual.
            foreach (Dungeon dungeon in population)
            {
                dungeon.EvaluateFitness();
            }

            // Ordenar por valor de aptitud.
            population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));

            // Seleccionar las mejores mazmorras (elitismo).
            newGeneration.Add(population[0]);  // Mejor mazmorra
            newGeneration.Add(population[1]);  // Segunda mejor mazmorra

            // Cruzar y mutar para generar nuevas mazmorras.
            while (newGeneration.Count < populationSize)
            {
                Dungeon parent1 = SelectParent();
                Dungeon parent2 = SelectParent();

                if (parent1 == parent2)
                {
                    continue;  // Seleccionar nuevos padres si son iguales.
                }

                Dungeon child = Crossover(parent1, parent2);
                if (child == null || child.Rooms.Count == 0)
                {
                    continue;  // Si el hijo no es válido, no lo agregues.
                }

                child.Mutate(mutationRate);
                newGeneration.Add(child);
            }

            population = newGeneration;
        }

        // Evaluar fitness de la población final antes de retornar
        foreach (Dungeon dungeon in population)
        {
            dungeon.EvaluateFitness();
        }

        population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));
        return population[0];  // Retorna la mejor mazmorra final.
    }

    Dungeon SelectParent()
    {
        int index = UnityEngine.Random.Range(0, populationSize / 2);  // Selección aleatoria entre las mejores.
        return population[index];
    }

    Dungeon Crossover(Dungeon parent1, Dungeon parent2)
    {

        // Verificar que ambos padres tengan habitaciones válidas
        if (parent1.Rooms.Count == 0 || parent2.Rooms.Count == 0)
        {
            return null;
        }

        Dungeon child = new Dungeon(dungeonSize, minRoomSize, maxRoomSize);

        // Implementar una estrategia de cruce más avanzada
        // Por ejemplo, combinar las habitaciones de ambos padres sin duplicados
        HashSet<string> roomSet = new HashSet<string>();
        foreach (Room room in parent1.Rooms)
        {
            string key = room.Position + "-" + room.Size;
            if (!roomSet.Contains(key))
            {
                child.Rooms.Add(new Room(room.Position, room.Size));
                roomSet.Add(key);
            }
        }

        foreach (Room room in parent2.Rooms)
        {
            string key = room.Position + "-" + room.Size;
            if (!roomSet.Contains(key))
            {
                child.Rooms.Add(new Room(room.Position, room.Size));
                roomSet.Add(key);
            }
        }

        // Limitar el número de habitaciones si es necesario
        if (child.Rooms.Count > Mathf.Min(parent1.Rooms.Count, parent2.Rooms.Count))
        {
            child.Rooms.RemoveRange(Mathf.Min(parent1.Rooms.Count, parent2.Rooms.Count), child.Rooms.Count - Mathf.Min(parent1.Rooms.Count, parent2.Rooms.Count));
        }

        child.ConnectRooms(); // Reconectar habitaciones después del cruce

        return child;
    }
}

public class Dungeon
{
    public List<Room> Rooms;
    public List<Connection> Connections;
    public float Fitness;

    public Vector2Int dungeonSize;
    public Vector2Int minRoomSize;
    public Vector2Int maxRoomSize;

    public Dungeon(Vector2Int dungeonSize, Vector2Int minRoomSize, Vector2Int maxRoomSize)
    {
        this.dungeonSize = dungeonSize;
        this.minRoomSize = minRoomSize;
        this.maxRoomSize = maxRoomSize;

        Rooms = new List<Room>();
        Connections = new List<Connection>();

        GenerateRooms();
        ConnectRooms();
    }

    public void Mutate(int mutationRate)
    {
        for (int i = 1; i < Rooms.Count; i++)  // Empezamos desde la segunda habitación
        {
            if (UnityEngine.Random.Range(0, 100) < mutationRate)
            {
                bool mutatePosition = UnityEngine.Random.value > 0.5f;

                Room originalRoom = Rooms[i];
                Room mutatedRoom = new Room(originalRoom.Position, originalRoom.Size);

                if (mutatePosition)
                {
                    Vector2Int newPosition = new Vector2Int(
                        UnityEngine.Random.Range(0, dungeonSize.x - mutatedRoom.Size.x),
                        UnityEngine.Random.Range(0, dungeonSize.y - mutatedRoom.Size.y)
                    );
                    mutatedRoom.Position = newPosition;
                }
                else
                {
                    Vector2Int newSize = new Vector2Int(
                        UnityEngine.Random.Range(minRoomSize.x, maxRoomSize.x),
                        UnityEngine.Random.Range(minRoomSize.y, maxRoomSize.y)
                    );
                    mutatedRoom.Size = newSize;
                }

                // Validar si la habitación no se superpone a las demás
                bool isValid = true;
                foreach (Room otherRoom in Rooms)
                {
                    if (otherRoom != originalRoom && IsOverlapping(mutatedRoom, otherRoom))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    Rooms[i] = mutatedRoom; // Aceptar la mutación si es válida
                }
                else
                {
                    UnityEngine.Debug.LogError("Mutación inválida: habitación superpuesta");
                }
            }
        }

        ConnectRooms();
    }


    bool IsOverlapping(Room roomA, Room roomB)
    {
        return !((roomA.Position.x + roomA.Size.x)*32 <= roomB.Position.x *32 ||
                 roomA.Position.x*32 >= (roomB.Position.x + roomB.Size.x)*32 ||
                 (roomA.Position.y + roomA.Size.y)*32 <= roomB.Position.y*32 ||
                 roomA.Position.y*32 >= (roomB.Position.y + roomB.Size.y) * 32);
    }

    void GenerateRooms()
    {
        int roomCount = UnityEngine.Random.Range(5, 8);
        float minRoomDistance = 100.0f; // Establecer la distancia mínima entre habitaciones
        int gridStep = 16; // Tamaño del paso de la cuadrícula

        // Generar la primera habitación en el centro (0,0) con tamaño impar
        Vector2Int firstRoomSize = new Vector2Int(160, 160);
        Vector2Int firstRoomPosition = new Vector2Int(-firstRoomSize.x / 2, -firstRoomSize.y / 2);
        Room firstRoom = new Room(firstRoomPosition, firstRoomSize);
        Rooms.Add(firstRoom);

        // Generar las demás habitaciones
        for (int i = 1; i < roomCount; i++)
        {
            Vector2Int roomSize = new Vector2Int(
                Mathf.RoundToInt(UnityEngine.Random.Range(minRoomSize.x, maxRoomSize.x) / (float)gridStep) * gridStep,
                Mathf.RoundToInt(UnityEngine.Random.Range(minRoomSize.y, maxRoomSize.y) / (float)gridStep) * gridStep
            );

            Room lastRoom = Rooms[Rooms.Count - 1];

            // Elegir una dirección aleatoria
            int direction = UnityEngine.Random.Range(0, 4);  // 0: derecha, 1: arriba, 2: izquierda, 3: abajo

            Vector2Int roomPosition;
            switch (direction)
            {
                case 0: // Derecha
                    roomPosition = new Vector2Int(
                        lastRoom.Position.x + lastRoom.Size.x + Mathf.RoundToInt(minRoomDistance / gridStep) * gridStep,
                        lastRoom.Position.y
                    );
                    break;
                case 1: // Arriba
                    roomPosition = new Vector2Int(
                        lastRoom.Position.x,
                        lastRoom.Position.y + lastRoom.Size.y + Mathf.RoundToInt(minRoomDistance / gridStep) * gridStep
                    );
                    break;
                case 2: // Izquierda
                    roomPosition = new Vector2Int(
                        lastRoom.Position.x - roomSize.x - Mathf.RoundToInt(minRoomDistance / gridStep) * gridStep,
                        lastRoom.Position.y
                    );
                    break;
                case 3: // Abajo
                    roomPosition = new Vector2Int(
                        lastRoom.Position.x,
                        lastRoom.Position.y - roomSize.y - Mathf.RoundToInt(minRoomDistance / gridStep) * gridStep
                    );
                    break;
                default:
                    roomPosition = lastRoom.Position;
                    break;
            }

            Room newRoom = new Room(roomPosition, roomSize);

            bool overlaps = false;
            foreach (Room existingRoom in Rooms)
            {
                // Revisar superposición
                if (IsOverlapping(newRoom, existingRoom) || IsTooClose(newRoom, existingRoom, minRoomDistance))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                Rooms.Add(newRoom);
            }
            else
            {
                i--; // Intentar de nuevo
            }
        }
    }


    // Nuevo método para verificar si las habitaciones están demasiado cerca
    bool IsTooClose(Room roomA, Room roomB, float minDistance)
    {
        // Calcular los bordes de ambas habitaciones
        float roomA_Left = roomA.Position.x;
        float roomA_Right = roomA.Position.x + roomA.Size.x;
        float roomA_Bottom = roomA.Position.y;
        float roomA_Top = roomA.Position.y + roomA.Size.y;

        float roomB_Left = roomB.Position.x;
        float roomB_Right = roomB.Position.x + roomB.Size.x;
        float roomB_Bottom = roomB.Position.y;
        float roomB_Top = roomB.Position.y + roomB.Size.y;

        // Calcular la distancia mínima requerida con el margen adicional
        float leftDistance = roomB_Right - roomA_Left;
        float rightDistance = roomA_Right - roomB_Left;
        float bottomDistance = roomB_Top - roomA_Bottom;
        float topDistance = roomA_Top - roomB_Bottom;

        // Si las habitaciones están demasiado cerca en cualquier dirección
        return (leftDistance < minDistance && rightDistance < minDistance) &&
               (topDistance < minDistance && bottomDistance < minDistance);
    }


    // Implementación del Algoritmo de Kruskal para conectar habitaciones
    public void ConnectRooms()
    {
        Connections.Clear();

        if (Rooms.Count < 2)
            return;

        List<ConnectionWithDistance> allConnections = new List<ConnectionWithDistance>();
        for (int i = 0; i < Rooms.Count; i++)
        {
            for (int j = i + 1; j < Rooms.Count; j++)
            {
                // Ahora los centros estarán alineados correctamente, incluso si el tamaño de las habitaciones es par
                float distance = Vector2Int.Distance(RoomCenter(Rooms[i]), RoomCenter(Rooms[j]));
                allConnections.Add(new ConnectionWithDistance(i, j, distance));
            }
        }

        allConnections.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        DisjointSet ds = new DisjointSet(Rooms.Count);

        foreach (var conn in allConnections)
        {
            if (ds.FindSet(conn.RoomA) != ds.FindSet(conn.RoomB))
            {
                ds.Union(conn.RoomA, conn.RoomB);
                Vector2Int start = AlignToGrid(RoomCenter(Rooms[conn.RoomA]), 16);
                Vector2Int end = AlignToGrid(RoomCenter(Rooms[conn.RoomB]), 16);

                // Crear pasillo alineado en forma de "L"
                Vector2Int intermediate = AlignToGrid(new Vector2Int(end.x, start.y), 16);
                Connections.Add(new Connection(start, intermediate));
                Connections.Add(new Connection(intermediate, end));
            }

            if (Connections.Count >= (Rooms.Count - 1) * 2)
                break;
        }
    }

    private Vector2Int AlignToGrid(Vector2Int point, int gridStep)
    {
        return new Vector2Int(
            Mathf.RoundToInt(point.x / (float)gridStep) * gridStep,
            Mathf.RoundToInt(point.y / (float)gridStep) * gridStep
        );
    }

    Vector2Int RoomCenter(Room room)
    {
        int centerX = room.Position.x + room.Size.x / 2;
        int centerY = room.Position.y + room.Size.y / 2;

        // Ajustar el centro a la cuadrícula de 16x16
        return AlignToGrid(new Vector2Int(centerX, centerY), 16);
    }


    public void EvaluateFitness()
    {
        float fitness = 0;

        // Penalizar por la longitud total de pasillos
        float totalHallwayLength = 0;
        foreach (Connection connection in Connections)
        {
            totalHallwayLength += Vector2Int.Distance(connection.start, connection.end);
        }
        fitness -= totalHallwayLength;

        // Penalizar por tamaños de habitaciones no deseados
        foreach (Room room in Rooms)
        {
            if (room.Size.x < minRoomSize.x || room.Size.y < minRoomSize.y ||
                room.Size.x > maxRoomSize.x || room.Size.y > maxRoomSize.y)
            {
                fitness -= 10;
            }
        }

        // Penalización por habitaciones demasiado cercanas
        float minRoomDistance = 100f; // Define la distancia mínima entre habitaciones
        float maxRoomDistance = 120f;
        for (int i = 0; i < Rooms.Count; i++)
        {
            for (int j = i + 1; j < Rooms.Count; j++)
            {
                float distanceBetweenRooms = Vector2Int.Distance(Rooms[i].Center, Rooms[j].Center);
                if (distanceBetweenRooms < minRoomDistance && distanceBetweenRooms > maxRoomDistance)
                {
                    fitness -= 20 * (minRoomDistance - distanceBetweenRooms);  // Penalización proporcional a la cercanía
                }
            }
        }

        // Recompensar si todas las habitaciones están conectadas
        if (Rooms.Count == Connections.Count + 1)
        {
            fitness += 50;
        }

        // Penalizar conexiones duplicadas
        for (int i = 0; i < Connections.Count - 1; i++)
        {
            for (int j = i + 1; j < Connections.Count; j++)
            {
                if (Connections[i].Equals(Connections[j]))
                {
                    fitness -= 50;
                }
            }
        }

        Fitness = fitness;
    }


}

public class Room
{
    public Vector2Int Position;
    public Vector2Int Size;

    public Vector2Int Center => new Vector2Int(Position.x + Size.x / 2, Position.y + Size.y / 2);

    public Room(Vector2Int position, Vector2Int size)
    {
        Position = position;
        Size = size;
    }
}

public class Connection
{
    public Vector2Int start;
    public Vector2Int end;

    public Connection(Vector2Int start, Vector2Int end)
    {
        this.start = start;
        this.end = end;
    }

    public override bool Equals(object obj)
    {
        if (obj is Connection other)
        {
            return (start.Equals(other.start) && end.Equals(other.end)) ||
                   (start.Equals(other.end) && end.Equals(other.start));
        }
        return false;
    }

    public override int GetHashCode()
    {
        return start.GetHashCode() ^ end.GetHashCode();
    }
}

public class ConnectionWithDistance
{
    public int RoomA;
    public int RoomB;
    public float Distance;

    public ConnectionWithDistance(int roomA, int roomB, float distance)
    {
        RoomA = roomA;
        RoomB = roomB;
        Distance = distance;
    }
}

public class DisjointSet
{
    private int[] parent;
    private int[] rank;

    public DisjointSet(int size)
    {
        parent = new int[size];
        rank = new int[size];
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    public int FindSet(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = FindSet(parent[x]);
        }
        return parent[x];
    }

    public void Union(int x, int y)
    {
        int xRoot = FindSet(x);
        int yRoot = FindSet(y);

        if (xRoot == yRoot)
            return;

        if (rank[xRoot] < rank[yRoot])
        {
            parent[xRoot] = yRoot;
        }
        else if (rank[xRoot] > rank[yRoot])
        {
            parent[yRoot] = xRoot;
        }
        else
        {
            parent[yRoot] = xRoot;
            rank[xRoot]++;
        }
    }
}
