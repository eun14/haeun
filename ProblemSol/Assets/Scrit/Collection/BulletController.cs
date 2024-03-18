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
        if (Input.GetMouseButtonDown(0) && canShoot && currentBulletCount < maxBulletCount) // ��Ŭ��
        {

            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletPrefab, greenBox.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bullet.AddComponent<Rigidbody>();
            }
            bullet.tag = "Bullet"; // �Ѿ˿� �±� �߰�
            bulletQueue.Enqueue(bullet);
            currentBulletCount++;

            // �Ѿ� �߻�
            bullet.SetActive(true);
            bullet.transform.position = greenBox.position;
            rb.velocity = (redBox.position - greenBox.position).normalized * 20f; // ��Ŭ�� �� �ӵ� ����
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

            // �Ѿ��� �ٽ� �߻��� �� �ֵ��� ���� ����
            if (currentBulletCount < maxBulletCount)
            {
                canShoot = true;
            }
        }
    }


}