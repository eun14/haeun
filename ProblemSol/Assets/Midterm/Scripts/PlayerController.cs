using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject camera;
    public Animation anim;

    private bool isRotating = false; // ȸ�� ������ ���θ� ��Ÿ���� ����
    private Quaternion targetRotation; // ��ǥ ȸ�� ������ �����ϴ� ����

    void Start()
    {
        anim = camera.GetComponent<Animation>();
    }

    void Update()
    {
        // ȸ�� ���̸� �̵��� ����
        if (isRotating)
            return;

        // ī�޶��� ������ ���͸� ����ϴ�.
        Vector3 cameraRight = camera.transform.right;

        // �¿� �̵�
        if (Input.GetKey(KeyCode.A))
        {
            // ī�޶��� ������ �������� �̵��մϴ�.
            transform.Translate(-cameraRight * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // ī�޶��� ������ �������� �̵��մϴ�.
            transform.Translate(cameraRight * speed * Time.deltaTime, Space.World);
        }

        // ���� �̵�
        if (Input.GetKey(KeyCode.W))
        {
            // �÷��̾��� �������� �̵��մϴ�.
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // �÷��̾��� �ݴ� �������� �̵��մϴ�.
            transform.Translate(-transform.forward * speed * Time.deltaTime, Space.World);
        }

        // O Ű �Է¿� ���� ī�޶� �������� 90�� ȸ����ŵ�ϴ�.
        if (Input.GetKeyDown(KeyCode.O) && !isRotating)
        {
            isRotating = true;
            targetRotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y - 90f, camera.transform.rotation.eulerAngles.z); // ��ǥ ȸ�� ���� ����
            StartCoroutine(RotateCamera());
        }
        // P Ű �Է¿� ���� ī�޶� ���������� 90�� ȸ����ŵ�ϴ�.
        else if (Input.GetKeyDown(KeyCode.P) && !isRotating)
        {
            isRotating = true;
            targetRotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y + 90f, camera.transform.rotation.eulerAngles.z); // ��ǥ ȸ�� ���� ����
            StartCoroutine(RotateCamera());
        }
    }

    IEnumerator RotateCamera()
    {
        // 1�� ���� ȸ���ϴ� �ִϸ��̼�
        float duration = 1.0f;
        float elapsed = 0f;
        Quaternion startRotation = camera.transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            camera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        // ȸ�� ���� �� ���� �ʱ�ȭ
        camera.transform.rotation = targetRotation;
        isRotating = false;
    }
}
