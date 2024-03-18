using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform greenBox;
    public Transform redBox;

    private Queue<GameObject> bulletQueue;
    private bool canShoot = true;
    private int maxBulletCount = 10;
    private int currentBulletCount = 0;

    void Start()
    {
        bulletQueue = new Queue<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && currentBulletCount < maxBulletCount) // 좌클릭
        {

            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, greenBox.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bullet.AddComponent<Rigidbody>();
            }
            bullet.tag = "Bullet"; // 총알에 태그 추가
            bulletQueue.Enqueue(bullet);
            currentBulletCount++;

            // 총알 발사
            bullet.SetActive(true);
            bullet.transform.position = greenBox.position;
            rb.velocity = (redBox.position - greenBox.position).normalized * 20f; // 우클릭 시 속도 증가
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RedBox"))
        {
            GameObject bullet = collision.gameObject;
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
            currentBulletCount--;

            // 총알을 다시 발사할 수 있도록 상태 변경
            if (currentBulletCount < maxBulletCount)
            {
                canShoot = true;
            }
        }
    }


}