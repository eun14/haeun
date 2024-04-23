using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1;      // 프러스텀 내부에 있는 오브젝트에 적용될 머티리얼
    public Material material2;      // 프러스텀 밖에 있는 오브젝트에 적용될 머티리얼
    public GameObject sphere;       // GameObject sphere 설정
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
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        FrustumPlanes frustum = new FrustumPlanes(thisCamera);      // 카메라의 프러스텀을 가져오기
        Renderer[] renderers = FindObjectsOfType<Renderer>();       // 모든 씬에 있는 모든 Renderer 가져오기

        foreach (Renderer renderer in renderers)
        {
            if (frustum.IsInsideFrustum(renderer.bounds.center))    // Renderer의 중심점이 프러스텀 내에 있는지 확인
            {
                // 프러스텀 내에 있는 경우 Material1 적용
                sphere.SetActive(true);
                renderer.enabled = true;    // 특정 GameObject의 렌더러 구성 요소가 활성화
                renderer.material = material1;
            }
            else
            {
                // 프러스텀 밖에 있는 경우 Material2 적용
                /*renderer.material = material2;*/
                sphere.SetActive(false);    // 프러스텀 밖에 있는 경우 sphere 비활성화
                renderer.enabled = false;   // 렌더러가 비활성화
            }
        }
    }
}

// 프러스텀 플레인 클래스
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