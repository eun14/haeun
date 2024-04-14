using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]

public class StaticMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.StaticMesh();
        }
    }
}

public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void StaticMesh()
    {
        Mesh mesh = new Mesh();

        // 별기둥의 각 꼭지점의 위치 정의
        Vector3[] vertices = new Vector3[]
        {
            // 오각형
            new Vector3(0.0f, 0.0f, 0.0f),     // 꼭지점 0 (아래)
            new Vector3(-0.7f, 0.5f, 0.0f),    // 꼭지점 1 (왼쪽1)
            new Vector3(-0.5f, 1.2f, 0.0f),    // 꼭지점 2 (왼쪽2)
            new Vector3(0.5f, 1.2f, 0.0f),     // 꼭지점 3 (오른쪽2)
            new Vector3(0.7f, 0.5f, 0.0f),     // 꼭지점 4 (오른쪽1)
           
            // 삼각형
            new Vector3(-1.0f, -0.5f, 0.0f),   // 꼭지점 5 (왼쪽1)
            new Vector3(-1.5f, 1.2f, 0.0f),    // 꼭지점 6 (왼쪽2)
            new Vector3(0.0f, 1.9f, 0.0f),     // 꼭지점 7 (위)
            new Vector3(1.5f, 1.2f, 0.0f),     // 꼭지점 8 (오른쪽2)
            new Vector3(1.0f, -0.5f, 0.0f),    // 꼭지점 9 (오른쪽1)
           
            // 사각형1_왼쪽1
            new Vector3(0.0f, 0.0f, 1.0f),     // 꼭지점 10 
            new Vector3(-1.0f, -0.5f, 1.0f),   // 꼭지점 11
            
            // 사각형2_왼쪽1
            new Vector3(-0.5f, 0.5f, 1.0f),    // 꼭지점 12
            new Vector3(-1.0f, -0.5f, 1.0f),   // 꼭지점 13
           
            // 사각형3_왼쪽2
            new Vector3(-0.5f, 0.5f, 1.0f),    // 꼭지점 14
            new Vector3(-1.5f, 1.2f, 1.0f),    // 꼭지점 15
            
            // 사각형4_왼쪽2
            new Vector3(-0.5f, 1.2f, 1.0f),    // 꼭지점 16
            new Vector3(-1.5f, 1.2f, 1.0f),    // 꼭지점 17
           
            // 사각형5_위
            new Vector3(0.0f, 1.9f, 1.0f),     // 꼭지점 18
            new Vector3(-0.5f, 1.2f, 1.0f),    // 꼭지점 19
         
            // 사각형6_위
            new Vector3(0.5f, 1.2f, 1.0f),     // 꼭지점 20
            new Vector3(0.0f, 1.9f, 1.0f),     // 꼭지점 21
           
            // 사각형7_오른쪽2
            new Vector3(1.5f, 1.2f, 1.0f),     // 꼭지점 22
            new Vector3(0.5f, 1.2f, 1.0f),     // 꼭지점 23
         
            // 사각형8_오른쪽2
            new Vector3(0.7f, 0.5f, 1.0f),     // 꼭지점 24
            new Vector3(1.5f, 1.2f, 1.0f),     // 꼭지점 25
           
            // 사각형9_오른쪽1
            new Vector3(1.0f, -0.5f, 1.0f),    // 꼭지점 26
            new Vector3(0.7f, 0.5f, 1.0f),     // 꼭지점 27
          
            // 사각형10_오른쪽1
            new Vector3(1.0f, -0.5f, 1.0f),    // 꼭지점 28
            new Vector3(0.0f, 0.0f, 1.0f),     // 꼭지점 29

            // 뒤 오각형
            new Vector3(0.0f, 0.0f, 1.0f),     // 꼭지점 30 (아래)
            new Vector3(-0.7f, 0.5f, 1.0f),    // 꼭지점 31 (왼쪽1)
            new Vector3(-0.5f, 1.2f, 1.0f),    // 꼭지점 32 (왼쪽2)
            new Vector3(0.5f, 1.2f, 1.0f),     // 꼭지점 33 (오른쪽2)
            new Vector3(0.7f, 0.5f, 1.0f),     // 꼭지점 34 (오른쪽1)
            
            // 뒤 삼각형
            new Vector3(-1.0f, -0.5f, 1.0f),   // 꼭지점 35 (왼쪽1)
            new Vector3(-1.5f, 1.2f, 1.0f),    // 꼭지점 36 (왼쪽2)
            new Vector3(0.0f, 1.9f, 1.0f),     // 꼭지점 37 (위)
            new Vector3(1.5f, 1.2f, 1.0f),     // 꼭지점 38 (오른쪽2)
            new Vector3(1.0f, -0.5f, 1.0f),    // 꼭지점 39 (오른쪽1)
        };

        // 각 삼각형의 세 꼭지점의 인덱스
        int[] triangleIndices = new int[]
        {
            // 오각형
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            
            // 삼각형
            0, 5, 1,
            1, 6, 2,
            2, 7, 3,
            3, 8, 4,
            4, 9, 0,
            
            // 사각형1
            0, 10, 11,
            0, 11, 5,
            
            // 사각형2
            1, 12, 11,
            1, 11, 5,
            
            // 사각형3
            1, 12, 15,
            1, 15, 6,
           
            // 사각형4
            2, 16, 15,
            2, 15, 6,
           
            // 사각형5
            7, 18, 16,
            7, 16, 2,
         
            // 사각형6
            3, 20, 18,
            3, 18, 7,
          
            // 사각형7
            8, 22, 20,
            8, 20, 3,
         
            // 사각형8
            8, 22, 24,
            8, 24, 4,
     
            // 사각형9
            9, 26, 24,
            9, 24, 4,
      
            // 사각형10
            9, 26, 10,
            9, 10, 0,
         
            // 뒤 오각형
            30, 31, 32,
            30, 32, 33,
            30, 33, 34,
         
            // 뒤 삼각형
            30, 35, 31,
            31, 36, 32,
            32, 37, 33,
            33, 38, 34,
            34, 39, 30,
        };

        Vector3[] normals = new Vector3[vertices.Length];                   // 각 꼭지점의 법선 벡터를 저장할 배열 초기화 (배열의 길이는 꼭지점의 개수와 같음)
        for (int i = 0; i < triangleIndices.Length; i += 3)                 // 모든 삼각형에 대해 반복
        {
            Vector3 v0 = vertices[triangleIndices[i]];                      // 현재 삼각형의 세 꼭지점
            Vector3 v1 = vertices[triangleIndices[i + 1]];
            Vector3 v2 = vertices[triangleIndices[i + 2]];

            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;    // 현재 삼각형의 법선 벡터 계산 -> 삼각형의 두 변에 대한 외적을 구한 뒤 정규화

            normals[triangleIndices[i]] += normal;                          // 현재 삼각형의 각 꼭지점에 대해 계산된 법선 벡터 누적
            normals[triangleIndices[i + 1]] += normal;
            normals[triangleIndices[i + 2]] += normal;
        }

        for(int i = 0; i < normals.Length;  i++)                            // 모든 꼭지점에 대해 반복하여 각 법선 벡터를 정규화 -> 모든 법선 벡터의 길이가 1인 단위 벡터가 됨
        {
            normals[i] = normals[i].normalized;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndices;
        mesh.normals = normals;

        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf == null)
            mf = gameObject.AddComponent<MeshFilter>();

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
            mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}