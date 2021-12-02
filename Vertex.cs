using System;
using System.Collections.Generic;
using System.Text;

class Vertex
{
    public float x;
    public float y;
    public List<Vertex> neighbourhood;
    public float cost;
    public float distance;
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
        cost = float.MaxValue;
        cost = float.MaxValue;
        costDistance = float.MaxValue;
        parent = null;
        closed = false;
        path = false;
    }

    public void SetCostDistance(float cost, float distance, float costDistance, Vertex parent)
    {
        this.cost = cost;
        this.distance = distance;
        this.costDistance = costDistance;
        this.parent = parent;
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

            float cost = this.cost + neighbour.CalculateDistance(this);
            float distance = neighbour.CalculateDistance(destination);
            float costDistance = cost + distance;

            if (neighbour.parent == null || neighbour.costDistance > costDistance)
                neighbour.SetCostDistance(cost, distance, costDistance, this);
        }
    }
}
