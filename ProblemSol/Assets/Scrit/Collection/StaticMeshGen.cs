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

        // ������� �� �������� ��ġ ����
        Vector3[] vertices = new Vector3[]
        {
            // ������
            new Vector3(0.0f, 0.0f, 0.0f),     // ������ 0 (�Ʒ�)
            new Vector3(-0.7f, 0.5f, 0.0f),    // ������ 1 (����1)
            new Vector3(-0.5f, 1.2f, 0.0f),    // ������ 2 (����2)
            new Vector3(0.5f, 1.2f, 0.0f),     // ������ 3 (������2)
            new Vector3(0.7f, 0.5f, 0.0f),     // ������ 4 (������1)
           
            // �ﰢ��
            new Vector3(-1.0f, -0.5f, 0.0f),   // ������ 5 (����1)
            new Vector3(-1.5f, 1.2f, 0.0f),    // ������ 6 (����2)
            new Vector3(0.0f, 1.9f, 0.0f),     // ������ 7 (��)
            new Vector3(1.5f, 1.2f, 0.0f),     // ������ 8 (������2)
            new Vector3(1.0f, -0.5f, 0.0f),    // ������ 9 (������1)
           
            // �簢��1_����1
            new Vector3(0.0f, 0.0f, 1.0f),     // ������ 10 
            new Vector3(-1.0f, -0.5f, 1.0f),   // ������ 11
            
            // �簢��2_����1
            new Vector3(-0.5f, 0.5f, 1.0f),    // ������ 12
            new Vector3(-1.0f, -0.5f, 1.0f),   // ������ 13
           
            // �簢��3_����2
            new Vector3(-0.5f, 0.5f, 1.0f),    // ������ 14
            new Vector3(-1.5f, 1.2f, 1.0f),    // ������ 15
            
            // �簢��4_����2
            new Vector3(-0.5f, 1.2f, 1.0f),    // ������ 16
            new Vector3(-1.5f, 1.2f, 1.0f),    // ������ 17
           
            // �簢��5_��
            new Vector3(0.0f, 1.9f, 1.0f),     // ������ 18
            new Vector3(-0.5f, 1.2f, 1.0f),    // ������ 19
         
            // �簢��6_��
            new Vector3(0.5f, 1.2f, 1.0f),     // ������ 20
            new Vector3(0.0f, 1.9f, 1.0f),     // ������ 21
           
            // �簢��7_������2
            new Vector3(1.5f, 1.2f, 1.0f),     // ������ 22
            new Vector3(0.5f, 1.2f, 1.0f),     // ������ 23
         
            // �簢��8_������2
            new Vector3(0.7f, 0.5f, 1.0f),     // ������ 24
            new Vector3(1.5f, 1.2f, 1.0f),     // ������ 25
           
            // �簢��9_������1
            new Vector3(1.0f, -0.5f, 1.0f),    // ������ 26
            new Vector3(0.7f, 0.5f, 1.0f),     // ������ 27
          
            // �簢��10_������1
            new Vector3(1.0f, -0.5f, 1.0f),    // ������ 28
            new Vector3(0.0f, 0.0f, 1.0f),     // ������ 29

            // �� ������
            new Vector3(0.0f, 0.0f, 1.0f),     // ������ 30 (�Ʒ�)
            new Vector3(-0.7f, 0.5f, 1.0f),    // ������ 31 (����1)
            new Vector3(-0.5f, 1.2f, 1.0f),    // ������ 32 (����2)
            new Vector3(0.5f, 1.2f, 1.0f),     // ������ 33 (������2)
            new Vector3(0.7f, 0.5f, 1.0f),     // ������ 34 (������1)
            
            // �� �ﰢ��
            new Vector3(-1.0f, -0.5f, 1.0f),   // ������ 35 (����1)
            new Vector3(-1.5f, 1.2f, 1.0f),    // ������ 36 (����2)
            new Vector3(0.0f, 1.9f, 1.0f),     // ������ 37 (��)
            new Vector3(1.5f, 1.2f, 1.0f),     // ������ 38 (������2)
            new Vector3(1.0f, -0.5f, 1.0f),    // ������ 39 (������1)
        };

        // �� �ﰢ���� �� �������� �ε���
        int[] triangleIndices = new int[]
        {
            // ������
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            
            // �ﰢ��
            0, 5, 1,
            1, 6, 2,
            2, 7, 3,
            3, 8, 4,
            4, 9, 0,
            
            // �簢��1
            0, 10, 11,
            0, 11, 5,
            
            // �簢��2
            1, 12, 11,
            1, 11, 5,
            
            // �簢��3
            1, 12, 15,
            1, 15, 6,
           
            // �簢��4
            2, 16, 15,
            2, 15, 6,
           
            // �簢��5
            7, 18, 16,
            7, 16, 2,
         
            // �簢��6
            3, 20, 18,
            3, 18, 7,
          
            // �簢��7
            8, 22, 20,
            8, 20, 3,
         
            // �簢��8
            8, 22, 24,
            8, 24, 4,
     
            // �簢��9
            9, 26, 24,
            9, 24, 4,
      
            // �簢��10
            9, 26, 10,
            9, 10, 0,
         
            // �� ������
            30, 31, 32,
            30, 32, 33,
            30, 33, 34,
         
            // �� �ﰢ��
            30, 35, 31,
            31, 36, 32,
            32, 37, 33,
            33, 38, 34,
            34, 39, 30,
        };

        Vector3[] normals = new Vector3[vertices.Length];                   // �� �������� ���� ���͸� ������ �迭 �ʱ�ȭ (�迭�� ���̴� �������� ������ ����)
        for (int i = 0; i < triangleIndices.Length; i += 3)                 // ��� �ﰢ���� ���� �ݺ�
        {
            Vector3 v0 = vertices[triangleIndices[i]];                      // ���� �ﰢ���� �� ������
            Vector3 v1 = vertices[triangleIndices[i + 1]];
            Vector3 v2 = vertices[triangleIndices[i + 2]];

            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;    // ���� �ﰢ���� ���� ���� ��� -> �ﰢ���� �� ���� ���� ������ ���� �� ����ȭ

            normals[triangleIndices[i]] += normal;                          // ���� �ﰢ���� �� �������� ���� ���� ���� ���� ����
            normals[triangleIndices[i + 1]] += normal;
            normals[triangleIndices[i + 2]] += normal;
        }

        for(int i = 0; i < normals.Length;  i++)                            // ��� �������� ���� �ݺ��Ͽ� �� ���� ���͸� ����ȭ -> ��� ���� ������ ���̰� 1�� ���� ���Ͱ� ��
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