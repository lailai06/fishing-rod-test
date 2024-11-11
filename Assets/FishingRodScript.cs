using UnityEngine;

public class FishingRodScript : MonoBehaviour
{
    public Transform rodTip; // Ujung pancing
    public Transform hook;   // Hook atau kail
    public LineRenderer lineRenderer; // Tali pancing
    public Transform fish;  // Objek ikan

    private bool isFishCaught = false;

    void Update()
    {
        // Mengatur posisi tali pancing sesuai rodTip dan hook
        lineRenderer.SetPosition(0, rodTip.position);
        lineRenderer.SetPosition(1, hook.position);

        // Lempar tali pancing saat klik kiri mouse
        if (Input.GetMouseButtonDown(0) && !isFishCaught)
        {
            ThrowLine();
        }

        // Tarik ikan jika sudah tertangkap
        if (Input.GetMouseButtonDown(1) && isFishCaught)
        {
            RetrieveFish();
        }

        // Gerakkan hook mengikuti posisi mouse (klik kiri)
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Jarak dari kamera
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Pindahkan hook ke posisi baru berdasarkan mouse
            hook.position = new Vector3(worldPosition.x, worldPosition.y, hook.position.z);
        }
    }

    void ThrowLine()
    {
        // Pindahkan hook ke bawah untuk melempar tali
        hook.position += new Vector3(0, -5f, 0); // Gerakan sederhana untuk melempar
    }

    void RetrieveFish()
    {
        // Tarik ikan ke hook
        fish.position = hook.position;
        isFishCaught = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Ketika kail mengenai ikan
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fish caught!");
            fish = other.transform;
            isFishCaught = true;
        }
    }
}
