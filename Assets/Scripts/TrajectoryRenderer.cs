using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3 coords;
    [SerializeField] LayerMask _lineColissionObjects;
    [SerializeField]private Material _DissolveMaterial;
    [SerializeField]private Material _OclusionMaterial;
    [SerializeField] private GameObject _colissionParticle;
    [SerializeField]private Transform _player;
    [Range(0,2)][SerializeField]private float _DisolveSpeed;
    private int _DissolveID;
    private int _ThresholdID;
    public float _time = 0;
    private void Start()
    {
        _DissolveID = Shader.PropertyToID("_Dissolve");
        _ThresholdID = Shader.PropertyToID("_Threshold");
        _DissolveMaterial.SetFloat(_DissolveID, 1);
        _OclusionMaterial.SetFloat(_ThresholdID, 1);
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        
        Vector3[] points = new Vector3[10];
       
        for (int i = 0; i < points.Length; i++)
        {
            float time = i*0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
        }
        _lineRenderer.positionCount = points.Length;

        _lineRenderer.SetPositions(points);

             RaycastHit hit;
        if (Physics.Raycast(points[points.Length - 1], Vector3.down, out hit,_lineColissionObjects))
        {
            Debug.Log("LineCollide");
            _colissionParticle.transform.position = hit.point;
        }
        else
        {
            _colissionParticle.transform.position = Vector3.zero;
        }

        _time += Time.deltaTime;
        _OclusionMaterial.SetFloat(_ThresholdID, 1);
        _DissolveMaterial.SetFloat(_DissolveID, Mathf.Lerp(1, 0,_time*_DisolveSpeed));

    }
    public void HideTrajectory()
    {
        _colissionParticle.transform.position = Vector3.zero;
        _time += Time.deltaTime;
        _DissolveMaterial.SetFloat(_DissolveID, Mathf.Lerp(0, 1, _time *1));
       _OclusionMaterial.SetFloat(_ThresholdID, 0);
    }
}
