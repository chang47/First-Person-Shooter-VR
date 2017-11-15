using UnityEngine;

public class MouseCameraContoller : MonoBehaviour
{
    public float Sensitivity = 10.0f;
    public float Smoothing = 10.0f;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    private GameObject _player;

    void Start()
    {
        _player = transform.parent.gameObject;
    }

	// Update is called once per frame
	void Update () {
        Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

	    mouseDirection.x *= Sensitivity * Smoothing;
	    mouseDirection.y *= Sensitivity * Smoothing;

        _smoothV.x = Mathf.Lerp(_smoothV.x, mouseDirection.x, 1f / Smoothing);
	    _smoothV.y = Mathf.Lerp(_smoothV.y, mouseDirection.y, 1f / Smoothing);

	    _mouseLook += _smoothV;
	    _mouseLook.y = Mathf.Clamp(_mouseLook.y, -90, 90);

	    transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        _player.transform.rotation = Quaternion.AngleAxis(_mouseLook.x, Vector3.up);
	}
}
