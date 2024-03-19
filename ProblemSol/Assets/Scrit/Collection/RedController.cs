using UnityEngine;

public class RedController : MonoBehaviour
{
    private GameManager gameManager; // GameManager 인스턴스에 대한 참조 변수

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject bullet = other.gameObject;
            bullet.SetActive(false);
        }
    }
}
