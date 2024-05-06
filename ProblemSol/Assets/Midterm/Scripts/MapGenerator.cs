using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 5; // ���� ���� ũ��
    public int mapHeight = 5; // ���� ���� ũ��
    public GameObject planePrefab; // Plane ������
    public GameObject wallLowPrefab; // ���� �� ������
    public GameObject wallHighPrefab; // ���� �� ������
    public string csvFileName = "Map.csv"; // CSV ���� �̸� (Assets ���� �ȿ� �־�� ��)

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // CSV ���� ���
        string folderPath = "Assets/Scrit"; // ���� ���
        string filePath = Path.Combine(folderPath, csvFileName);

        // CSV ���� �б�
        string[] lines = File.ReadAllLines(filePath);

        // Plane ����
        GameObject planeObj = Instantiate(planePrefab, Vector3.zero, Quaternion.identity);

        // Plane�� �������� ���� ũ�⿡ �°� ����
        planeObj.transform.localScale = new Vector3(mapWidth, 3f, mapHeight);

        // �� ����
        for (int z = 0; z < mapHeight * 2; z++)
        {
            for (int x = 0; x < mapWidth * 2; x++)
            {
                // CSV ������ Ȯ��
                int cellValue = int.Parse(lines[z].Split(',')[x]);

                float xPos = (x * 10 / 2) - ((mapWidth * 10) / 2) + (4.5f - (mapWidth-1.5f)+1.5f);
                float zPos = (z * 10 / 2) - ((mapHeight * 10) / 2) + (4.5f - (mapHeight-1.5f)+1.5f);



                // ������Ʈ ��ġ
                switch (cellValue)
                {
                    case 1:
                        // ���� �� ��ġ
                        Instantiate(wallLowPrefab, new Vector3(xPos, 1f, zPos), Quaternion.identity, planeObj.transform);
                        break;
                    case 2:
                        // ���� �� ��ġ
                        Instantiate(wallHighPrefab, new Vector3(xPos, 2f, zPos), Quaternion.identity, planeObj.transform);
                        break;
                    default:
                        // 0�� ��쿡�� �ƹ� ������Ʈ�� ��ġ���� ����
                        break;
                }
            }
        }
    }
}
