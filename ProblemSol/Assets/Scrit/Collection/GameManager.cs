using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform greenBox;
    public Transform redBox;
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();     // �Ѿ��� �����ϴ� ť
    private bool canShoot = true;
    public int maxBulletCount = 10;
    private int currentBulletCount = 0;

    void Start()
    {
        for (int i = 0; i < maxBulletCount; i++)                        // �ʱ⿡ �Ѿ��� �����Ͽ� ť�� �߰�
        {
            GameObject bullet = Instantiate(bulletPrefab, greenBox.position, Quaternion.identity);
            bullet.SetActive(false);                                    // �Ѿ��� ��Ȱ��ȭ�Ͽ� ����
            bulletQueue.Enqueue(bullet);                                // �Ѿ��� ť�� �߰�
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && currentBulletCount < maxBulletCount)
        {
            GameObject bullet = bulletQueue.Dequeue();                  // ť���� �Ѿ��� ����
            bullet.SetActive(true);                                     // �Ѿ� Ȱ��ȭ
            bullet.transform.position = greenBox.position;              // �߻� ��ġ ����
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = (redBox.position - greenBox.position).normalized * 20f;

            ReuseBullet(bullet);                                        // �Ѿ� ��Ȱ�� �ż��� ȣ��
        }
    }

    public void ReuseBullet(GameObject bullet)                          // �Ѿ��� ��Ȱ���ϴ� �ż���
    {
        bullet.SetActive(true);                                         // �Ѿ� ��ġ �ʱ�ȭ
        bullet.transform.position = greenBox.position;                  // �߻� ��ġ ����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = (redBox.position - greenBox.position).normalized * 20f;
        bulletQueue.Enqueue(bullet);                                    // ����� �Ѿ��� �ٽ� ť�� �߰�
    }
}