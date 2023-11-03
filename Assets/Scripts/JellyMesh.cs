using UnityEngine;

public class JellyMesh : MonoBehaviour
{
    [SerializeField] private float intensity = 1;
    [SerializeField] private float mass = 1;
    [SerializeField] private float stiffness = 1;
    [SerializeField] private float damping = .75f;

    private Mesh originalMesh, meshClone;
    private MeshRenderer meshRenderer;
    private JellyVertex[] jv;
    private Vector3[] vertexArray;

    private void Awake()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        meshRenderer = GetComponent<MeshRenderer>();
        jv = new JellyVertex[meshClone.vertices.Length];
        for (int i = 0; i < meshClone.vertices.Length; ++i)
            jv[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
    }

    private void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;
        for(int i = 0; i < jv.Length; ++i)
        {
            Vector3 target = transform.TransformPoint(vertexArray[jv[i].ID]);
            float localIntensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * intensity;
            jv[i].Shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(jv[i].Position);
            vertexArray[jv[i].ID] = Vector3.Lerp(vertexArray[jv[i].ID], target, localIntensity);
        }
        meshClone.vertices = vertexArray;
    }
}

public class JellyVertex
{
    public int ID;
    public Vector3 Position;
    public Vector3 Velocity, Force;

    public JellyVertex(int id, Vector3 pos)
    {
        ID = id;
        Position = pos;
    }

    public void Shake(Vector3 target, float m, float s, float d)
    {
        Force = (target - Position) * s;
        Velocity = (Velocity + Force / m) * d;
        Position += Velocity;
        if((Velocity + Force + Force / m).magnitude < 0.001f)
                Position = target;
    }
}