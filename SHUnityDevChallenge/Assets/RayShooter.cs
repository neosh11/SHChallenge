using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Most of this code is adapted from a previously built projects
public class RayShooter : MonoBehaviour
{
    // underscrore to prevent hiding inherited component
    private Camera _camera;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed = 20;

    private bool showX;
    private static int crossHairSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        showX = false;
    }

    void OnGUI()
    {
        // Calculated here to not be affected by screensize changes
        float posX = _camera.pixelWidth / 2 - crossHairSize / 4;
        float posY = _camera.pixelHeight / 2 - crossHairSize / 2;
        GUI.Label(new Rect(posX, posY, crossHairSize, crossHairSize), "X");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !EventSystem.current.IsPointerOverGameObject())
        {
            showX = true;
            Debug.Log("FIRE");
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.
            pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                StartCoroutine("ShootProjectile", new ProjInput(transform.position, transform.rotation, hit.point));
            }
        }

    }

    struct ProjInput
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 hit;

        public ProjInput(Vector3 pos, Quaternion rot, Vector3 hit)
        {
            this.pos = pos;
            this.rot = rot;
            this.hit = hit;
        }
    }

    IEnumerator ShootProjectile(ProjInput pI)
    {
        GameObject proj = Instantiate(projectile, pI.pos, pI.rot);
        proj.GetComponent<Rigidbody>().velocity = (pI.hit - transform.position).normalized * speed;
        yield return new WaitForSeconds(2f);

        if (proj != null)
            Destroy(proj);
    }
}
