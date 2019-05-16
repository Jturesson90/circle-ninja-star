
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(EdgeCollider2D))]
public class CircleBorderMesh : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField] private int _degrees = 180;
    [SerializeField] private float _outlineWidth = 0.25f;
    [SerializeField] private float _radius = 1f;
    public float Radius
    {
        get { return _radius; }
        set
        {
            if (_radius != value)
            {
                _radius = value;
                if (_circleMeshCreator != null)
                {
                    _circleMeshCreator.Radius = value;
                }
            }
        }
    }
    public int Degrees
    {
        get { return _degrees; }
        set
        {
            if (_degrees != value)
            {
                _degrees = value;
                if (_circleMeshCreator != null)
                {
                    _circleMeshCreator.Degrees = value;
                }
            }
        }
    }
    [SerializeField] private float _generateColliderThreshold = 2.8f;

    private MeshFilter _meshFilter;
    private EdgeCollider2D _edgeCollider;
    private CircleMeshCreator _circleMeshCreator;

    public float StartRadius { get; private set; }

    public bool IsInitialized
    {
        get { return _circleMeshCreator != null && _meshFilter && _edgeCollider; }
    }

    private void Awake()
    {
        StartRadius = Radius;
    }

    private void Initialize()
    {
        Radius = StartRadius;
        _meshFilter = _meshFilter ?? GetComponent<MeshFilter>();
        _meshFilter.mesh = _meshFilter.mesh ?? new Mesh();
        _edgeCollider = _edgeCollider ?? GetComponent<EdgeCollider2D>();
        _circleMeshCreator = _circleMeshCreator ?? new CircleMeshCreator(_meshFilter.mesh, Degrees, transform.position, Radius, _outlineWidth);
    }

    public void OnEditorSettingsChanged()
    {
        StartRadius = Radius;
        Initialize();
        GenerateCircle();
    }

    public void Generate(int degrees)
    {
        Degrees = degrees;
        Generate();
        UpdateEdgeColliderPoints();
    }

    public void Generate()
    {
        Initialize();
        GenerateCircle();
    }

    public void UpdateRadius(float radius)
    {
        if (!IsInitialized) Initialize();
        Radius = radius;
        GenerateCircle();
    }

    private void GenerateCircle()
    {
        _circleMeshCreator.CreateCircle();
        if (_generateColliderThreshold > Radius)
        {
            UpdateEdgeColliderPoints();
        }
    }
    private void UpdateEdgeColliderPoints()
    {
        _circleMeshCreator.GenerateColliderPoints();
        _edgeCollider.points = _circleMeshCreator.ColliderPoints;
    }

}