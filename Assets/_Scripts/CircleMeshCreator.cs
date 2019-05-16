using System;
using System.Collections.Generic;
using Drolegames.Extensions;
using UnityEngine;

public class CircleMeshCreator
{
    public Vector2[] ColliderPoints { get; private set; }

    public int Degrees { get; set; } = 270;
    public Vector3 Origin { get; set; } = Vector3.zero;
    public float Radius { get; set; } = 1f;
    public float OutlineWidth { get; set; } = 0.2f;

    private readonly Mesh mesh;

    public CircleMeshCreator(Mesh mesh)
    {
        this.mesh = mesh;
        if (!mesh)
        {
            throw new Exception("Circle Mesh Creator must have a mesh");
        }
    }

    public CircleMeshCreator(Mesh mesh, int degrees, Vector3 origin, float radius, float outlineWidth)
    {
        Degrees = degrees;
        Origin = origin;
        Radius = radius;
        OutlineWidth = outlineWidth;
        if (!mesh)
        {
            throw new Exception("Circle Mesh Creator must have a mesh");
        }
        this.mesh = mesh;
        this.mesh.name = "Circle";
    }

    public void CreateCircle(bool autoGenerateColliderPoints = true)
    {
        if (mesh.vertexCount == Degrees * 2 + 2)
        {
            mesh.vertices = GenerateVertices(mesh.vertices, Origin, Radius, Degrees + 1, OutlineWidth);
        }
        else
        {
            mesh.Clear();
            mesh.vertices = GenerateVertices(Origin, Radius, Degrees + 1, OutlineWidth);
            mesh.triangles = GenerateTriangles(mesh.vertices);
        }
        if (autoGenerateColliderPoints)
            GenerateColliderPoints();

    }
    public void GenerateColliderPoints()
    {
        ColliderPoints = GenerateColliderPoints(mesh.vertices);
    }

    private static Vector3[] GenerateVertices(Vector3 origin, float radius, int degrees, float outlineWidth)
    {
        return GenerateVertices(new Vector3[degrees * 2], origin, radius, degrees, outlineWidth);
    }

    private static Vector3[] GenerateVertices(Vector3[] vertices, Vector3 origin, float radius, int degrees, float outlineWidth)
    {
        for (int i = 0; i < degrees; i++)
        {
            float degree = i;
            var first = origin.RotateAround2D(0 + radius - outlineWidth, degree);
            var second = origin.RotateAround2D(radius, degree);
            vertices[i * 2] = first;
            vertices[i * 2 + 1] = second;
        }

        return vertices;
    }
    private static int[] GenerateTriangles(Vector3[] vertices)
    {
        var result = new List<int>();
        for (int i = 0; i < vertices.Length - 3; i += 2)
        {
            result.Add(i + 0);
            result.Add(i + 3);
            result.Add(i + 2);
            result.Add(i + 3);
            result.Add(i + 0);
            result.Add(i + 1);
        }

        return result.ToArray();
    }

    private static Vector2[] GenerateColliderPoints(Vector3[] vertices)
    {
        var points = new List<Vector2>();
        for (int i = 0; i < vertices.Length; i += 64)
        {
            points.Add(vertices[i]);
        }
        points.Add(vertices[vertices.Length - 2]);
        return points.ToArray();
    }
}


