using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Target;

    [Space]
    [SerializeField] private float _speed = 8f;

    private void Update()
    {
        if(Target == null) { return; }

        var targetPos = new Vector3(Target.position.x, 0f, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _speed);
    }
}