using UnityEngine;

public class RedController : MonoBehaviour
{
    private GameManager gameManager; // GameManager �ν��Ͻ��� ���� ���� ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject bullet = other.gameObject;
            bullet.SetActive(false);
        }
    }
}
