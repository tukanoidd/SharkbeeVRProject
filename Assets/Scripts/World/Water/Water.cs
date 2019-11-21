using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Water : MonoBehaviour
{
    public Camera playerCam;

    public Vector3 waveSource = new Vector3(2, 0, 2);
    public float waveFrequency = 0.53f;
    public float waveHeight = 0.48f;
    public float waveLength = 0.71f;
    public bool edgeBlend = true;
    public bool forceFlatShading = true;

    private Mesh mesh;
    private Vector3[] verts;

    private MeshFilter mf;

    private void Start()
    {
        playerCam.depthTextureMode = DepthTextureMode.Depth;
        mf = GetComponent<MeshFilter>();
        MakeMeshLowPoly(mf);
    }

    MeshFilter MakeMeshLowPoly(MeshFilter mf)
    {
        mesh = mf.sharedMesh;
        Vector3[] oldVerts = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = oldVerts[triangles[i]];
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        verts = mesh.vertices;

        return mf;
    }

    void Update()
    {
        CalcWave();
        SetEdgeBlend();
    }

    void CalcWave()
    {
        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v = verts[i];
            v.y = 0;

            float distance = Vector3.Distance(v, waveSource);
            distance = (distance % waveLength) / waveLength;
            
            v.y = waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2 * waveFrequency + Mathf.PI * 2 * distance);
            verts[i] = v;
        }

        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.MarkDynamic();

        mf.mesh = mesh;
    }
    
    void SetEdgeBlend()
    {
        if (!SystemInfo.SupportsTextureFormat((TextureFormat) RenderTextureFormat.Depth))
        {
            edgeBlend = false;
        }

        if (edgeBlend)
        {
            Shader.EnableKeyword("WATER_EDGEBLEND_ON");
            if (playerCam)
            {
                playerCam.depthTextureMode |= DepthTextureMode.Depth;
            }
        }
    }
}
