using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f; // �þ� �Ÿ�
    public float fieldOfViewAngle = 45f; // �þ߰�
    public float patrolDuration = 6f; // ���ƴٴϴ� �ð�
    public float patrolAngle = -90f; // ���ƴٴϴ� ����
    public float patrolRange = 6f; // ���ƴٴϴ� ����
    public float wanderDuration = 6f; // �θ����Ÿ��� �ð�
    public Camera enemyCamera; // �� ���� ī�޶�
    private Vector3 originalPosition; // ���� �ʱ� ��ġ
    private Quaternion originalRotation; // ���� �ʱ� ȸ��
    private bool isPatrolling = false; // ���ƴٴϴ� ������ ����
    private bool isWandering = false; // �θ����Ÿ��� ������ ����

    void Start()
    {
        // ���� �ʱ� ��ġ�� ȸ���� �����մϴ�.
        originalPosition = transform.position;
        originalRotation = transform.rotation;

#if UNITY_EDITOR
        // �����Ϳ����� ���ƴٴϴ� ������ ǥ���մϴ�.
        if (Application.isEditor)
        {
            DrawPatrolRange();
        }
#endif
    }

    void Update()
    {
        // �÷��̾ �þ� ���� �ִ��� Ȯ���մϴ�.
        if (CanSeePlayer())
        {
            // �÷��̾ �����ϸ� �� ���� ī�޶� Ȱ��ȭ�մϴ�.
            enemyCamera.gameObject.SetActive(true);
            // ���� �÷��̾� ������ �̵���ŵ�ϴ�.
            MoveTowardsPlayer();
        }
        else
        {
            // ���ƴٴϴ� ���� �ƴϰ� �θ����Ÿ��� ���� �ƴ϶�� ���ƴٴϵ��� �մϴ�.
            if (!isPatrolling && !isWandering)
            {
                StartCoroutine(Patrol());
            }
        }
    }

    bool CanSeePlayer()
    {
        // �÷��̾�� ���� �Ÿ��� ����մϴ�.
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // �þ� �Ÿ� ���� �ְ�, �÷��̾ �þ� ���� ���� �ִ��� Ȯ���մϴ�.
        if (distanceToPlayer < detectionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer < fieldOfViewAngle * 0.5f)
            {
                // �÷��̾ �����߽��ϴ�.
                return true;
            }
        }

        // �÷��̾ �������� ���߽��ϴ�.
        return false;
    }

    void MoveTowardsPlayer()
    {
        // �÷��̾� ������ �̵��մϴ�.
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * patrolDuration); // �̵� �ӵ� ����
    }

    IEnumerator Patrol()
    {
        // ���ƴٴϴ� ���·� �����մϴ�.
        isPatrolling = true;

        // ���� ���������� ȸ���մϴ�.
        Quaternion targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + patrolAngle, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < patrolDuration / 2)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / (patrolDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� �������� ȸ���մϴ�.
        targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y - patrolAngle, 0f);
        elapsedTime = 0f;
        while (elapsedTime < patrolDuration / 2)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / (patrolDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���ƴٴϴ� ���¸� �����մϴ�.
        isPatrolling = false;

        // ������ ����� �θ����Ÿ����� �մϴ�.
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        // �θ����Ÿ��� ���·� �����մϴ�.
        isWandering = true;

        // �θ����Ÿ��� �ð� ���� ��ٸ��ϴ�.
        yield return new WaitForSeconds(wanderDuration);

        // �θ����Ÿ��� ���¸� �����ϰ� �ٽ� ���ƴٴϵ��� �մϴ�.
        isWandering = false;
    }

#if UNITY_EDITOR
    void DrawPatrolRange()
    {
        // transform ��ü�� null���� Ȯ���մϴ�.
        if (transform == null)
        {
            Debug.LogWarning("Transform component is not assigned.");
            return;
        }

        // Box ������ ���̵������ �׸��ϴ�.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        
        using (new Handles.DrawingScope(cubeTransform))
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
#endif
}
