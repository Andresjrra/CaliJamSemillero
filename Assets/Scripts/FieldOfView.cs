using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour
{
    public float fov = 360f;
    public int numAristas = 360;
    public float anguloInicial = 0;
    public float distanciaVision = 8f;
    public float reduccionPorSegundo = 0.1f;
    public float distanciaMinima = 2f;
    public float tiempoVisionCompleta = 5f;
    public LayerMask LayerMask;

    private Mesh mesh;
    private Vector3 origen;
    private float distanciaMaxima;
    private bool visionReduciendose = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origen = Vector3.zero;
        distanciaMaxima = distanciaVision;
    }

    // Update is called once per frame
    void Update()
    {
        GenerarMesh();
        if (visionReduciendose)
        {
            ReducirVision();
        }
    }

    private void GenerarMesh()
    {
        Vector3[] vertices = new Vector3[numAristas + 2];
        int[] triangulos = new int[numAristas * 3];

        float anguloActual = anguloInicial;
        float incrementoAngulo = fov / numAristas;

        vertices[0] = origen;

        int indiceVertices = 1;
        int indiceTriangulos = 0;

        for (int i = 0; i <= numAristas; i++)
        {

            RaycastHit2D raycastHit2D = Physics2D.Raycast(origen, GetVectorFromAngle(anguloActual), distanciaVision, LayerMask);

            Vector3 verticeActual;

            if (raycastHit2D.collider == null)
            {
                verticeActual = origen + GetVectorFromAngle(anguloActual) * distanciaVision;
            }
            else
            {
                verticeActual = raycastHit2D.point;
            }



            vertices[indiceVertices] = verticeActual;

            if (i > 0)
            {
                triangulos[indiceTriangulos] = 0;
                triangulos[indiceTriangulos + 1] = indiceVertices - 1;
                triangulos[indiceTriangulos + 2] = indiceVertices;


                indiceTriangulos += 3;
            }


            indiceVertices++;
            anguloActual -= incrementoAngulo;





        }



        mesh.vertices = vertices;
        mesh.triangles = triangulos;


    }

    void ReducirVision()
    {
        if (distanciaVision > distanciaMinima)
        {
            distanciaVision -= reduccionPorSegundo * Time.deltaTime;

        }
    }

    public void RestaurarVision(float valor)
    { 
    
        distanciaVision = Mathf.Clamp(valor, distanciaMinima, distanciaMaxima);
        visionReduciendose = false;
        StopAllCoroutines();
        StartCoroutine(EsperarAntesDeReducir());

    }

    private IEnumerator EsperarAntesDeReducir()
    {
        yield return new WaitForSeconds(tiempoVisionCompleta);
        visionReduciendose = true;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 newOrigin)
    {
        origen = newOrigin;

    }
}