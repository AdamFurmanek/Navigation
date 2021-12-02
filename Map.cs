using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Map
{
    Vertex[,] map;
    int height;
    int width;

    public Map(int height, int width)
    {
        map = new Vertex[height, width];
        this.height = height;
        this.width = width;
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                map[y, x] = new Vertex(x, y);

        RandomHoles(540, new Vertex[] { map[0, 0], map[height - 1, width - 1] });

        SetNeighbourhood();
    }

    public void RandomHoles(int count, Vertex[] avoid = null)
    {
        var random = new Random();
        while (count > 0)
        {
            int y = random.Next(0, height);
            int x = random.Next(0, width);
            if (map[y, x] != null && (avoid == null || !avoid.Contains(map[y, x])))
            {
                map[y, x] = null;
                count--;
            }
        }
    }

    public void SetNeighbourhood()
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                if (GetVertex(y, x) != null)
                    SetNeighbourhood(y, x);
    }

    public void SetNeighbourhood(int height, int width)
    {
        List<Vertex> neighbourhood = new List<Vertex>();
        for (int y = height - 1; y <= height + 1; y++)
            for (int x = width - 1; x <= width + 1; x++)
                if (y != height || x != width)
                    neighbourhood.Add(GetVertex(y, x));

        GetVertex(height, width).SetNeighbourhood(neighbourhood.Where(v => v != null).ToList());
    }

    public Vertex GetVertex(int height, int width)
    {
        if (height < 0 || height >= this.height)
            return null;
        if (width < 0 || width >= this.width)
            return null;

        return map[height, width];
    }

    public void PrintMap()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.ResetColor();
                var vertex = map[y, x];
                if(vertex == null)
                    Console.Write("   ");
                else if (vertex.costDistance == float.MaxValue)
                    Console.Write("xx ");
                else
                {
                    if (vertex.path)
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write((int)vertex.costDistance + " ");
                }
            }
            Console.WriteLine();
        }
    }

    public List<Vertex> searching = new List<Vertex>();
    public void FindPath(Vertex start, Vertex destination)
    {
        start.CalculateDestinationDistance(destination);
        start.SetCostDistance(0, start.CalculateDistance(destination), null);
        searching.Add(start);
        while(true)
        {
            var current = BestInALL();

            if (current == destination || current == null)
                break;

            current.CalculateNeighbourhood(destination);
            foreach (var neighbour in current.neighbourhood)
                if (neighbour.parent != null && !neighbour.closed)
                    searching.Add(neighbour);

            current.closed = true;
        }

        var path = destination;
        path.path = true;
        while (path != start && path != null)
        {
            path = path.parent;
            if(path != null)
                path.path = true;
        }
    }

    Vertex BestInALL()
    {
        Vertex best = null;
        foreach(var vertex in searching)
            if (!vertex.closed && (best == null || vertex.costDistance < best.costDistance || (vertex.costDistance == best.costDistance && vertex.distance < best.distance)))
                best = vertex;

        return best;
    }

    public void Reset()
    {
        foreach (var vertex in searching)
            vertex.Reset();

        searching.Clear();
    }

}
