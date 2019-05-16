using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    public Material flatMat;
    public Material smoothMat;
    

    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public bool flatShadding;

    public Gradient gradient;

    public List<terrainTypes> terrains = new List<terrainTypes>();

    public Image sprite;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        if (sprite != null)
        {
            sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        }
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        Mesh mesh = meshData.CreatMesh();

        if (flatShadding) //FlatShadding
        {
            //NoShared Verts
            Vector3[] oldVerts = mesh.vertices;
            int[] triangles = mesh.triangles;
            Vector3[] vertices = new Vector3[triangles.Length];
            for (int i = 0; i < triangles.Length; i++)
            {
                vertices[i] = oldVerts[triangles[i]];
                triangles[i] = i;
            }


            //Vertex Colors
            Color[] colors = new Color[vertices.Length];

            //perVertex
            /*for (int i = 0; i < vertices.Length; i++)
            {

                for (int j = 0; j < terrains.Count; j++)
                {
                    if(vertices[i].y < terrains[0].height)
                    {
                        colors[i] = new Vector4(0,0,0,1);
                    }
                    else if (vertices[i].y >= terrains[j].height)
                    {
                        colors[i] = terrains[j].color;
                    }
                }


            }*/

            //perTriangle
            for (int i = 0; i < vertices.Length; i += 3)
            {
                for (int j = 0; j < terrains.Count; j++)
                {
                    if (vertices[i].y < terrains[0].height || vertices[i + 1].y < terrains[0].height || vertices[i + 2].y < terrains[0].height)
                    {
                        colors[i] = new Vector4(0, 0, 0, 1);
                        colors[i + 1] = new Vector4(0, 0, 0, 1);
                        colors[i + 2] = new Vector4(0, 0, 0, 1);
                    }

                    //all verts
                    if (vertices[i].y >= terrains[j].height || vertices[i + 1].y > terrains[j].height || vertices[i + 2].y > terrains[j].height)
                    //average
                    //if((vertices[i].y + vertices[i+1].y + vertices[i+2].y) / 3 > terrains[j].height)                
                    {
                        colors[i] = terrains[j].color;
                        colors[i + 1] = terrains[j].color;
                        colors[i + 2] = terrains[j].color;
                    }
                }
            }

            mesh.Clear();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.colors = colors;
            Debug.Log(mesh.vertexCount);

            meshRenderer.sharedMaterial = flatMat;
        }
        else //smooth Shadding
        {
            meshRenderer.sharedMaterial = smoothMat;
            meshRenderer.sharedMaterial.SetVector("_elevationMinMax", new Vector4(Noise.minNoiseHeight, Noise.maxNoiseHeight));
            ColorGenerator colorGen = new ColorGenerator();
            meshRenderer.sharedMaterial.SetTexture("_texture", colorGen.updateColors(gradient));
        }
            //apply new triangles
            meshCollider.sharedMesh = mesh;
            meshFilter.sharedMesh = mesh;
            //meshRenderer.sharedMaterial.mainTexture = texture;
    }

    public class ColorGenerator
    {
        Texture2D texture;
        const int textureResolution = 50;

        public ColorGenerator()
        {
            texture = new Texture2D(textureResolution, 1);
        }

        public Texture updateColors(Gradient gradient)
        {
            Color[] colors = new Color[textureResolution];
            for (int i = 0; i < textureResolution; i++)
            {
                colors[i] = gradient.Evaluate(i / (textureResolution - 1f));
            }
            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }
    }

    [System.Serializable]
    public class terrainTypes
    {
        public string name;
        public float height;
        public Color color;
    }

}
