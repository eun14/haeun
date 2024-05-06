using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 5; // 맵의 가로 크기
    public int mapHeight = 5; // 맵의 세로 크기
    public GameObject planePrefab; // Plane 프리팹
    public GameObject wallLowPrefab; // 낮은 벽 프리팹
    public GameObject wallHighPrefab; // 높은 벽 프리팹
    public string csvFileName = "Map.csv"; // CSV 파일 이름 (Assets 폴더 안에 있어야 함)

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // CSV 파일 경로
        string folderPath = "Assets/Scrit"; // 폴더 경로
        string filePath = Path.Combine(folderPath, csvFileName);

        // CSV 파일 읽기
        string[] lines = File.ReadAllLines(filePath);

        // Plane 생성
        GameObject planeObj = Instantiate(planePrefab, Vector3.zero, Quaternion.identity);

        // Plane의 스케일을 맵의 크기에 맞게 조절
        planeObj.transform.localScale = new Vector3(mapWidth, 3f, mapHeight);

        // 맵 생성
        for (int z = 0; z < mapHeight * 2; z++)
        {
            for (int x = 0; x < mapWidth * 2; x++)
            {
                // CSV 데이터 확인
                int cellValue = int.Parse(lines[z].Split(',')[x]);

                float xPos = (x * 10 / 2) - ((mapWidth * 10) / 2) + (4.5f - (mapWidth-1.5f)+1.5f);
                float zPos = (z * 10 / 2) - ((mapHeight * 10) / 2) + (4.5f - (mapHeight-1.5f)+1.5f);



                // 오브젝트 배치
                switch (cellValue)
                {
                    case 1:
                        // 낮은 벽 배치
                        Instantiate(wallLowPrefab, new Vector3(xPos, 1f, zPos), Quaternion.identity, planeObj.transform);
                        break;
                    case 2:
                        // 높은 벽 배치
                        Instantiate(wallHighPrefab, new Vector3(xPos, 2f, zPos), Quaternion.identity, planeObj.transform);
                        break;
                    default:
                        // 0인 경우에는 아무 오브젝트도 배치하지 않음
                        break;
                }
            }
        }
    }
}
