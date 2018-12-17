using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public List<terrainTypes> terrains = new List<terrainTypes>();

    public Image sprite;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        if(sprite != null)
        {
            sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        }
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        Mesh mesh = meshData.CreatMesh();

        //flat Shading
        Vector3[] oldVerts = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];
        //t
        Color[] colors = new Color[vertices.Length];
        
        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = oldVerts[triangles[i]];
            triangles[i] = i;                
        }

        //t
        for (int i = 0; i < vertices.Length; i+=3)
        {
            for (int j = 0; j < terrains.Count; j++)
            {
                if(vertices[i].y > terrains[j].height || vertices[i + 1].y > terrains[j].height || vertices[i + 2].y > terrains[j].height)
                {                    
                    colors[i] = terrains[j].color;
                    colors[i+1] = terrains[j].color;
                    colors[i+2] = terrains[j].color;
                }
            }
                        
        }


        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.colors = colors;

        //apply new triangles
        meshCollider.sharedMesh = mesh;
        meshFilter.sharedMesh = mesh;
        //meshRenderer.sharedMaterial.mainTexture = texture;

    }


    [System.Serializable]
    public class terrainTypes
    {
        public string name;
        public float height;
        public Color color;
    }

}
