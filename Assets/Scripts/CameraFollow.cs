using UnityEngine;

public class CameraFollow : MonoBehaviour
{  public Transform target;  // Танк, за которым следует камера
    public float distance = 10f;  // Расстояние камеры от танка
    public float height = 5f;  // Высота камеры над танком
    public float positionSmoothSpeed = 0.1f;  // Скорость плавного перемещения камеры
    public float rotationSmoothSpeed = 2f;  // Скорость плавного поворота камеры

    private Vector3 currentVelocity;  // Вспомогательная переменная для сглаживания движения

    void LateUpdate()
    {
        if (target == null) return;

        // Целевая позиция камеры
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

        // Плавное движение камеры с задержкой
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, positionSmoothSpeed);
        transform.position = smoothedPosition;

        // Целевой поворот камеры
        Quaternion targetRotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position);

        // Плавный поворот камеры с задержкой
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
    }
}