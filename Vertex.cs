using System;
using System.Collections.Generic;
using System.Text;

class Vertex
{
    public float x;
    public float y;
    public List<Vertex> neighbourhood;
    public float distance;
    public float cost;
    public float costDistance;
    public Vertex parent;
    public bool closed;
    public bool path;

    public Vertex(float x, float y)
    {
        this.x = x;
        this.y = y;
        Reset();
    }

    public void SetNeighbourhood(List<Vertex> neighbourhood)
    {
        this.neighbourhood = neighbourhood;
    }

    public void Reset()
    {
        distance = float.MaxValue;
        cost = float.MaxValue;
        costDistance = float.MaxValue;
        parent = null;
        closed = false;
        path = false;
    }

    public void SetCostDistance(float cost, float costDistance, Vertex parent)
    {
        this.cost = cost;
        this.costDistance = costDistance;
        this.parent = parent;
    }

    public void CalculateDestinationDistance(Vertex destination)
    {
        if (distance == float.MaxValue)
            distance = CalculateDistance(destination);
    }

    public float CalculateDistance(Vertex other)
    {
        float y = MathF.Abs(this.y - other.y);
        float x = MathF.Abs(this.x - other.x);
        
        return MathF.Sqrt(y * y + x * x);
    }

    public void CalculateNeighbourhood(Vertex destination)
    {
        foreach(var neighbour in neighbourhood)
        {
            if (neighbour.closed)
                continue;

            neighbour.CalculateDestinationDistance(destination);
            float cost = this.cost + neighbour.CalculateDistance(this);
            float costDistance = cost + neighbour.distance;

            if (neighbour.parent == null || neighbour.costDistance > costDistance)
                neighbour.SetCostDistance(cost, costDistance, this);
        }
    }
}
