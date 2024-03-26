using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform greenBox;
    public Transform redBox;
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();     // 총알을 관리하는 큐
    private bool canShoot = true;
    public int maxBulletCount = 10;
    private int currentBulletCount = 0;

    void Start()
    {
        for (int i = 0; i < maxBulletCount; i++)                        // 초기에 총알을 생성하여 큐에 추가
        {
            GameObject bullet = Instantiate(bulletPrefab, greenBox.position, Quaternion.identity);
            bullet.SetActive(false);                                    // 총알을 비활성화하여 숨김
            bulletQueue.Enqueue(bullet);                                // 총알을 큐에 추가
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && currentBulletCount < maxBulletCount)
        {
            GameObject bullet = bulletQueue.Dequeue();                  // 큐에서 총알을 꺼냄
            bullet.SetActive(true);                                     // 총알 활성화
            bullet.transform.position = greenBox.position;              // 발사 위치 지정
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = (redBox.position - greenBox.position).normalized * 20f;

            ReuseBullet(bullet);                                        // 총알 재활용 매서드 호출
        }
    }

    public void ReuseBullet(GameObject bullet)                          // 총알을 재활용하는 매서드
    {
        bullet.SetActive(true);                                         // 총알 위치 초기화
        bullet.transform.position = greenBox.position;                  // 발사 위치 지정
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = (redBox.position - greenBox.position).normalized * 20f;
        bulletQueue.Enqueue(bullet);                                    // 재사용된 총알을 다시 큐에 추가
    }
}