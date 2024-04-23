using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1;      // �������� ���ο� �ִ� ������Ʈ�� ����� ��Ƽ����
    public Material material2;      // �������� �ۿ� �ִ� ������Ʈ�� ����� ��Ƽ����
    public GameObject sphere;       // GameObject sphere ����
    private Camera thisCamera;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        sphere.SetActive(true);
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        FrustumPlanes frustum = new FrustumPlanes(thisCamera);      // ī�޶��� ���������� ��������
        Renderer[] renderers = FindObjectsOfType<Renderer>();       // ��� ���� �ִ� ��� Renderer ��������

        foreach (Renderer renderer in renderers)
        {
            if (frustum.IsInsideFrustum(renderer.bounds.center))    // Renderer�� �߽����� �������� ���� �ִ��� Ȯ��
            {
                // �������� ���� �ִ� ��� Material1 ����
                sphere.SetActive(true);
                renderer.enabled = true;    // Ư�� GameObject�� ������ ���� ��Ұ� Ȱ��ȭ
                renderer.material = material1;
            }
            else
            {
                // �������� �ۿ� �ִ� ��� Material2 ����
                /*renderer.material = material2;*/
                sphere.SetActive(false);    // �������� �ۿ� �ִ� ��� sphere ��Ȱ��ȭ
                renderer.enabled = false;   // �������� ��Ȱ��ȭ
            }
        }
    }
}

// �������� �÷��� Ŭ����
public class FrustumPlanes
{
    private readonly Plane[] planes;

    public FrustumPlanes(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Vector3 point)
    {
        foreach (var plane in planes)
        {
            if (!plane.GetSide(point))
            {

                return false;
            }
        }
        return true;
    }
}