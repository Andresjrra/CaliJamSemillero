using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GridManager gridManager;

    public List<Vector2> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Vector2Int start = gridManager.WorldToGrid(startPos);
        Vector2Int target = gridManager.WorldToGrid(targetPos);

        Debug.Log($"Buscando camino de {start} a {target}");

        List<Vector2> path = new List<Vector2>();

        if (!gridManager.IsWalkable(target.x, target.y))
        {
            Debug.Log("Destino no es transitable.");
            return path;
        }

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == target)
            {
                Debug.Log("Camino encontrado.");
                break;
            }

            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(neighbor) && gridManager.IsWalkable(neighbor.x, neighbor.y))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(target)) 
        {
            Debug.Log("No se encontró un camino.");
            return path;
        }

        Vector2Int step = target;
        while (step != start)
        {
            path.Add(gridManager.GridToWorld(step.x, step.y));
            step = cameFrom[step];
        }

        path.Reverse();
        Debug.Log($"Ruta generada con {path.Count} pasos.");
        return path;
    }

    List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        return new List<Vector2Int>
        {
            new Vector2Int(node.x + 1, node.y),
            new Vector2Int(node.x - 1, node.y),
            new Vector2Int(node.x, node.y + 1),
            new Vector2Int(node.x, node.y - 1)
        };
    }
}
