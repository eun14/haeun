using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject camera;
    public Animation anim;

    private bool isRotating = false; // 회전 중인지 여부를 나타내는 변수
    private Quaternion targetRotation; // 목표 회전 각도를 저장하는 변수

    void Start()
    {
        anim = camera.GetComponent<Animation>();
    }

    void Update()
    {
        // 회전 중이면 이동을 막음
        if (isRotating)
            return;

        // 카메라의 오른쪽 벡터를 얻습니다.
        Vector3 cameraRight = camera.transform.right;

        // 좌우 이동
        if (Input.GetKey(KeyCode.A))
        {
            // 카메라의 오른쪽 방향으로 이동합니다.
            transform.Translate(-cameraRight * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // 카메라의 오른쪽 방향으로 이동합니다.
            transform.Translate(cameraRight * speed * Time.deltaTime, Space.World);
        }

        // 상하 이동
        if (Input.GetKey(KeyCode.W))
        {
            // 플레이어의 전방으로 이동합니다.
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // 플레이어의 반대 방향으로 이동합니다.
            transform.Translate(-transform.forward * speed * Time.deltaTime, Space.World);
        }

        // O 키 입력에 따라 카메라를 왼쪽으로 90도 회전시킵니다.
        if (Input.GetKeyDown(KeyCode.O) && !isRotating)
        {
            isRotating = true;
            targetRotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y - 90f, camera.transform.rotation.eulerAngles.z); // 목표 회전 각도 설정
            StartCoroutine(RotateCamera());
        }
        // P 키 입력에 따라 카메라를 오른쪽으로 90도 회전시킵니다.
        else if (Input.GetKeyDown(KeyCode.P) && !isRotating)
        {
            isRotating = true;
            targetRotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y + 90f, camera.transform.rotation.eulerAngles.z); // 목표 회전 각도 설정
            StartCoroutine(RotateCamera());
        }
    }

    IEnumerator RotateCamera()
    {
        // 1초 동안 회전하는 애니메이션
        float duration = 1.0f;
        float elapsed = 0f;
        Quaternion startRotation = camera.transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            camera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        // 회전 종료 후 변수 초기화
        camera.transform.rotation = targetRotation;
        isRotating = false;
    }
}
