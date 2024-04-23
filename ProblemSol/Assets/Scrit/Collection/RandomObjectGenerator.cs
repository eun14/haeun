using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR                                         // unity 에디터 환경에서만 컴파일되도록 함
using UnityEditor;
#endif

public class RandomObjectGenerator : MonoBehaviour
{
    public GameObject TargetObject;                     // 프리팹 참조
    public int ObjectNumber = 0;                        // 생성할 객체의 개수 정의

#if UNITY_EDITOR
    [CustomEditor(typeof(RandomObjectGenerator))]       // 클래스를 상속받는 사용자 정의 에디터 클래스로 에디터에서 해당 스크립트의 인스턴스를 선택했을 때 인스펙터에 표시될 내용을 정의
    public class RandomObjectGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()           // 메서드는 에디터의 인스펙터 창에 커스텀 UI를 그림
        {
            base.OnInspectorGUI();

            RandomObjectGenerator generator = (RandomObjectGenerator)target;
            if (GUILayout.Button("Generate Objects"))
            {
                generator.GenerateObjects();
            }
        }
    }
#endif

    public void GenerateObjects()
    {
        // 이 곳에 Object를 생성하고 배치하는 코드를 작성하세요.
        for (int i = 0; i < ObjectNumber; i++)  // ObjectNumber만큼 반복하여 객체를 생성하고 배치
        {
            // 랜덤한 위치를 생성합니다.
            Vector3 randomPosition = new Vector3(
                Random.Range(transform.position.x - transform.localScale.x * 0.5f, transform.position.x + transform.localScale.x * 0.5f),
                Random.Range(transform.position.y - transform.localScale.y * 0.5f, transform.position.y + transform.localScale.y * 0.5f),
                Random.Range(transform.position.z - transform.localScale.z * 0.5f, transform.position.z + transform.localScale.z * 0.5f)
            );

            // TargetObject를 생성하고 랜덤한 위치에 배치합니다.
            GameObject newObject = Instantiate(TargetObject, randomPosition, Quaternion.identity);
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Box 형태의 가이드라인을 그립니다.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);  //4x4 행렬을 나타내는 클래스로 주어진 위치, 회전, 크기를 가진 변환 행렬을 생성
        
        // using문을 사용하면 'IDisposable' 인터페이스를 구현한 개체의 리소스를 사용한 후에 자동으로 해제할 수 있음. using 블록을 벗어나면 'Dispose()' 메서드가 자동으로 호출되어 리소스가 해제됨 따라서 Handles.DrawingScrope를 사용할 때는 해당 범위 내에서 그려진 그래픽 요소들의 좌표 공간을 지정하고, 해당 범위가 끝나면 자동으로 리소스를 해제하여 메모리 누수를 방지할 수 있음
        using (new Handles.DrawingScope(cubeTransform))                                                         // unity 에디터에서 그리기 작업을 수행할 때 사용되는 범위를 지정하는 클래스로 cubeTransform으로 생성한 변환 행렬을 전달하여, 이 범위 내에서 그려지는 그래픽 요소들의 좌표 공간을 설정
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);                                                    // Handles는 unity 에디터에서 사용되는 3D 행들 및 기타 그리기 함수를 포함하는 클래스로 DrawWireCube 메서드는 와이어프레임으로 된 정육면체를 그리는 함수
        }
    }
#endif
}