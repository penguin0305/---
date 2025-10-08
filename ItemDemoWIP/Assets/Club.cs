using UnityEngine;

public class Club : MonoBehaviour
{
    [SerializeField] private float angle = 45f;     // 회전 각도(+ 시계, - 반시계)
    [SerializeField] private float duration = 0.1f; // 편도 시간(초)

    private bool swinging;
    private Quaternion baseRot;

    private void Start()
    {
        // 이 스크립트는 ClubPivot(회전축)에 붙입니다.
        baseRot = transform.localRotation; // 보통 (0,0,0)
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !swinging)
            StartCoroutine(Swing());
    }

    private System.Collections.IEnumerator Swing()
    {
        swinging = true;

        Quaternion start = baseRot;
        Quaternion end = baseRot * Quaternion.Euler(0, 0, angle);

        // 앞으로
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(start, end, t / duration);
            yield return null;
        }

        // 복귀
        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(end, start, t / duration);
            yield return null;
        }

        transform.localRotation = baseRot;
        swinging = false;
    }
}
