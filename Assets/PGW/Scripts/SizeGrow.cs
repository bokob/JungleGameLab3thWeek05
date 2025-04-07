using UnityEngine;

public class SizeGrow : MonoBehaviour
{
    private Vector3 originalScale; // 오브젝트의 원래 크기
    private float duration = 0.5f;  // 크기가 커지는 데 걸리는 시간 (0.5초)
    private float elapsedTime = 0f; // 경과 시간

    void Start()
    {
        // 원래 크기를 저장
        originalScale = transform.localScale;
        // 시작 시 크기를 0으로 설정
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            // 경과 시간 증가
            elapsedTime += Time.deltaTime;
            // 0에서 원래 크기로 부드럽게 보간
            float t = elapsedTime / duration; // 0~1 사이 값
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
        }
        else
        {
            // 시간이 다 지나면 정확히 원래 크기로 고정
            transform.localScale = originalScale;
            // 필요하면 이 스크립트를 비활성화
            // enabled = false;
        }
    }
}
