using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform greenBox;
    public Transform redBox;
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();

    private bool canShoot = true;
    public int maxBulletCount = 10;
    private int currentBulletCount = 0;

    void Start()
    {
        // 초기에 총알을 생성하여 큐에 추가
        for (int i = 0; i < maxBulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, greenBox.position, Quaternion.identity);
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }
    }

    void Update()
    {
        // 총알을 발사하는 코드
        if (Input.GetMouseButtonDown(0) && canShoot && currentBulletCount < maxBulletCount)
        {
            GameObject bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = greenBox.position;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = (redBox.position - greenBox.position).normalized * 20f;

            ReuseBullet(bullet);
        }
    }

    public void ReuseBullet(GameObject bullet)
    {
        // 재활용된 총알 처리
        bullet.SetActive(true);
        bullet.transform.position = greenBox.position;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = (redBox.position - greenBox.position).normalized * 20f;
        bulletQueue.Enqueue(bullet); // 재사용된 총알을 다시 큐에 추가
    }
}