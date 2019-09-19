using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private GameObject body;
    private Material material;
    private Vector3 start, end;
    private float width, height;
    private float bendAngleStart, bendAngleEnd;

    public void Initialize(Vector3 start, Vector3 end, float bendAngleStart, float bendAngleEnd, Material material)
    {
        this.start = start;
        this.end = end;
        this.bendAngleStart = bendAngleStart;
        this.bendAngleEnd = bendAngleEnd;
        this.material = material;
    }

    public void Awake()
    {
        width = 3.0f;
        height = 10.0f;
    }

    // Start is called before the first frame update
    public void Start()
    {
        body = new GameObject("Barrier", typeof(MeshFilter), typeof(BoxCollider), typeof(MeshRenderer));
        //body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.transform.SetParent(this.transform);
        Vector3 middle = start + (end - start) / 2;
        body.transform.localPosition = new Vector3(middle.x, 0.0f, middle.z);
        //body.transform.localScale = new Vector3(0.3f, 1, (b - a).magnitude);
        body.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, end - start, Vector3.up), 0);
        body.GetComponent<MeshRenderer>().material = material;
        body.GetComponent<MeshFilter>().mesh = CreateMesh();

        BoxCollider boxCollider = body.GetComponent<BoxCollider>();
        boxCollider.center = 0.5f * height * Vector3.up;
        boxCollider.size = new Vector3(width, height, (end - start).magnitude);
    }

    // Update is called once per frame
    public void Update()
    {
    }

    private Mesh CreateMesh()
    {
        float length = (end - start).magnitude;
        float insetLengthStart = Mathf.Tan(Mathf.Deg2Rad*bendAngleStart) * width / 2;
        insetLengthStart = (insetLengthStart > length / 2) ? length / 2 : insetLengthStart;
        float insetLengthEnd = Mathf.Tan(Mathf.Deg2Rad*bendAngleEnd) * width / 2;
        insetLengthEnd = (insetLengthEnd > length / 2) ? length / 2 : insetLengthEnd;

        Mesh mesh = new Mesh();

        Vector3[] corners = new Vector3[4*2];
        Vector3[] vertices = new Vector3[4*6];
        Vector2[] uvs = new Vector2[8];
        int[] triangles = new int[36];

        corners[0] = new Vector3(-width / 2, height, -(length / 2 - insetLengthStart));
        corners[1] = new Vector3(-width / 2, height, +(length / 2 - insetLengthEnd));
        corners[2] = new Vector3(+width / 2, height, +(length / 2 + insetLengthEnd));
        corners[3] = new Vector3(+width / 2, height, -(length / 2 + insetLengthStart));

        for(int i = 0; i < 4; ++i)
        {
            corners[7 - i] = corners[i] - height * Vector3.up;
        }

        for (int i = 0; i < 8; i++)
        {
            vertices[0 + i] = vertices[8 + i] = vertices[16 + i] = corners[i];
        }
               
        uvs[0] = new Vector2(0,1);
        uvs[1] = new Vector2(1,1);
        uvs[2] = new Vector2(1,0);
        uvs[3] = new Vector2(0,0);
        uvs[4] = new Vector2(0,1);
        uvs[5] = new Vector2(1,1);
        uvs[6] = new Vector2(1,0);
        uvs[7] = new Vector2(0,0);

        triangles[00] = 0;  //top face
        triangles[01] = 1;
        triangles[02] = 3;
        triangles[03] = 1;
        triangles[04] = 2;
        triangles[05] = 3;
        triangles[06] = 4;  //bottom face
        triangles[07] = 5;
        triangles[08] = 7;
        triangles[09] = 5;
        triangles[10] = 6;
        triangles[11] = 7;
        triangles[12] = 0 + 8;  //left face
        triangles[13] = 7 + 8;  
        triangles[14] = 1 + 8;
        triangles[15] = 7 + 8;
        triangles[16] = 6 + 8;
        triangles[17] = 1 + 8;
        triangles[18] = 3 + 8;  //right face
        triangles[19] = 2 + 8;  
        triangles[20] = 4 + 8;
        triangles[21] = 4 + 8;
        triangles[22] = 2 + 8;
        triangles[23] = 5 + 8;
        triangles[24] = 0 + 8 + 8;  //front face
        triangles[25] = 3 + 8 + 8;
        triangles[26] = 4 + 8 + 8;
        triangles[27] = 0 + 8 + 8;
        triangles[28] = 4 + 8 + 8;
        triangles[29] = 7 + 8 + 8;
        triangles[30] = 1 + 8 + 8;  //back face
        triangles[31] = 6 + 8 + 8;
        triangles[32] = 2 + 8 + 8;
        triangles[33] = 6 + 8 + 8;
        triangles[34] = 5 + 8 + 8;
        triangles[35] = 2 + 8 + 8;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }
}
