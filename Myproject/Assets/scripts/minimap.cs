using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public RectTransform playerInMap;
    public RectTransform map2dEnd;
    public Transform map3dParent;
    public Transform map3dEnd;

    private Vector3 normalized, mapped;

    // Update is called once per frame
    void Update()
    {
        normalized = Divide(
                 map3dParent.InverseTransformPoint(this.transform.position),
                 map3dEnd.position - map3dParent.position
             );
        normalized.y = normalized.z;
        mapped = Multiply(normalized, map2dEnd.localPosition);
        mapped.z = 0;
        playerInMap.localPosition = mapped;
    }
    


private static Vector3 Divide(Vector3 a, Vector3 b)
{
    return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
}

private static Vector3 Multiply(Vector3 a, Vector3 b)
{
    return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
}
}
