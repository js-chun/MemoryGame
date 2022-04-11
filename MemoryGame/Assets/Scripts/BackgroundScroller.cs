using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float backgroundSpeed = 0.2f;
    Material myMaterial;
    Vector2 offset;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(backgroundSpeed, 0f);
    }

    // scrolling background image
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}