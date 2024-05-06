using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f; // 시야 거리
    public float fieldOfViewAngle = 45f; // 시야각
    public float patrolDuration = 6f; // 돌아다니는 시간
    public float patrolAngle = -90f; // 돌아다니는 각도
    public float patrolRange = 6f; // 돌아다니는 범위
    public float wanderDuration = 6f; // 두리번거리는 시간
    public Camera enemyCamera; // 적 시점 카메라
    private Vector3 originalPosition; // 적의 초기 위치
    private Quaternion originalRotation; // 적의 초기 회전
    private bool isPatrolling = false; // 돌아다니는 중인지 여부
    private bool isWandering = false; // 두리번거리는 중인지 여부

    void Start()
    {
        // 적의 초기 위치와 회전을 저장합니다.
        originalPosition = transform.position;
        originalRotation = transform.rotation;

#if UNITY_EDITOR
        // 에디터에서만 돌아다니는 범위를 표시합니다.
        if (Application.isEditor)
        {
            DrawPatrolRange();
        }
#endif
    }

    void Update()
    {
        // 플레이어가 시야 내에 있는지 확인합니다.
        if (CanSeePlayer())
        {
            // 플레이어를 감지하면 적 시점 카메라를 활성화합니다.
            enemyCamera.gameObject.SetActive(true);
            // 적을 플레이어 쪽으로 이동시킵니다.
            MoveTowardsPlayer();
        }
        else
        {
            // 돌아다니는 중이 아니고 두리번거리는 중이 아니라면 돌아다니도록 합니다.
            if (!isPatrolling && !isWandering)
            {
                StartCoroutine(Patrol());
            }
        }
    }

    bool CanSeePlayer()
    {
        // 플레이어와 적의 거리를 계산합니다.
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // 시야 거리 내에 있고, 플레이어가 시야 각도 내에 있는지 확인합니다.
        if (distanceToPlayer < detectionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer < fieldOfViewAngle * 0.5f)
            {
                // 플레이어를 감지했습니다.
                return true;
            }
        }

        // 플레이어를 감지하지 못했습니다.
        return false;
    }

    void MoveTowardsPlayer()
    {
        // 플레이어 쪽으로 이동합니다.
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * patrolDuration); // 이동 속도 조절
    }

    IEnumerator Patrol()
    {
        // 돌아다니는 상태로 변경합니다.
        isPatrolling = true;

        // 적이 오른쪽으로 회전합니다.
        Quaternion targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + patrolAngle, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < patrolDuration / 2)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / (patrolDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 적이 왼쪽으로 회전합니다.
        targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y - patrolAngle, 0f);
        elapsedTime = 0f;
        while (elapsedTime < patrolDuration / 2)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / (patrolDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 돌아다니는 상태를 해제합니다.
        isPatrolling = false;

        // 범위를 벗어나면 두리번거리도록 합니다.
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        // 두리번거리는 상태로 변경합니다.
        isWandering = true;

        // 두리번거리는 시간 동안 기다립니다.
        yield return new WaitForSeconds(wanderDuration);

        // 두리번거리는 상태를 해제하고 다시 돌아다니도록 합니다.
        isWandering = false;
    }

#if UNITY_EDITOR
    void DrawPatrolRange()
    {
        // transform 객체가 null인지 확인합니다.
        if (transform == null)
        {
            Debug.LogWarning("Transform component is not assigned.");
            return;
        }

        // Box 형태의 가이드라인을 그립니다.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        
        using (new Handles.DrawingScope(cubeTransform))
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
#endif
}
