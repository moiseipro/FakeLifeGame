using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour {

	public Transform target;

	public float targetHeight = 1.7f;
	public float distance = 5.0f;
	public float offsetFromWall = 0.1f;

	public float maxDistance = 20;
	public float minDistance = .6f;

	public float xSpeed = 200.0f;
	public float ySpeed = 200.0f;

	public int yMinLimit = -80;
	public int yMaxLimit = 80;

	public int zoomRate = 40;

	public float rotationDampening = 3.0f;
	public float zoomDampening = 5.0f;

	public LayerMask collisionLayers = -1;

	private float xDeg = 0.0f;
	private float yDeg = 0.0f;
	private float currentDistance;
	private float desiredDistance;
	private float correctedDistance;

	void Start ()
	{
		Vector3 angles = transform.eulerAngles;
		xDeg = angles.x;
		yDeg = angles.y;

		currentDistance = distance;
		desiredDistance = distance;
		correctedDistance = distance;

		// Создание rigidbody без вращения
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}


	void LateUpdate ()
	{
		Vector3 vTargetOffset;

		// Если нет цели для вращения, ничего не делать
		if (!target)
			return;

		// Можно настроить поворот камеры на нажатие клавиши
		//if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
		//{
			xDeg += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
			yDeg -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
		//}
		// Плавное перемещение камеры в направление движения персонажа
		/* if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			float targetRotationAngle = target.eulerAngles.y;
			float currentRotationAngle = transform.eulerAngles.y;
			xDeg = Mathf.LerpAngle (currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
		}*/

		yDeg = ClampAngle (yDeg, yMinLimit, yMaxLimit);
		//Debug.Log (""+yDeg);
		// Установка значения вращения камеры
		Quaternion rotation = Quaternion.Euler (yDeg, xDeg, 0);

		// Расчет расстояния от камеры до цели
		//desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs (desiredDistance);
		desiredDistance = Mathf.Clamp (desiredDistance, minDistance, maxDistance);
		correctedDistance = desiredDistance;

		// Расчет положения камеры относительно цели
		vTargetOffset = new Vector3 (0, -targetHeight, 0);
		Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

		// Проверка наличия столкновения луча с объектом между целью и камерой
		RaycastHit collisionHit;
		Vector3 trueTargetPosition = new Vector3 (target.position.x, target.position.y + targetHeight, target.position.z);

		// Если столкновение произошло, вычислить новую позицию относительно объекта столкновения
		bool isCorrected = false;
		if (Physics.Linecast (trueTargetPosition, position, out collisionHit, collisionLayers.value))
		{
			// Вычисляем расстояние от исходного положения до объекта столкновения.
			// Добавляем смещение от объекта столкновения. И устанавливаем камеру в нужную позицию
			correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - offsetFromWall;
			isCorrected = true;
		}

		// Сглаживание
		currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;

		// Ограничение дистанции камеры
		currentDistance = Mathf.Clamp (currentDistance, minDistance, maxDistance);

		// Установка новой позиции камеры по текущему расстоянию 
		float yDegOff;
		if (yDeg > 0)
			yDegOff = yDeg / 70f;
		else yDegOff = -yDeg / 100f;
		position = target.position - (rotation * Vector3.forward * (currentDistance - yDegOff) + vTargetOffset);

		transform.rotation = rotation;
		transform.position = position;
	}

	private static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}