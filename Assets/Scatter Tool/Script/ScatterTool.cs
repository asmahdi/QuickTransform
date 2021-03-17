using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScatterTool : MonoBehaviour
{
    public GameObject prefab;
    public GameObject borderPoint00, borderPoint10,borderPoint01, borderPoint11;
    public int amountX, amountY;
    public float randomScaleMin = 0.1f, randomScaleMax = 1;
    public bool randomRotaion;

    [Range(0,.5f)]
    public float offsetX, offsetY;

    private float distanceX, distanceY;

    struct ObjTransform
    {
        public Vector3 position;
        public Quaternion rotation;

    };

    private List<GameObject> spawnObjects = new List<GameObject>();
    private List<ObjTransform> objTrans = new List<ObjTransform>();

    private Transform targetParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CalculateTransforms();

        Debug.DrawLine(borderPoint00.transform.position, borderPoint10.transform.position, Color.red);
        Debug.DrawLine(borderPoint01.transform.position, borderPoint11.transform.position, Color.red);
        Debug.DrawLine(borderPoint00.transform.position, borderPoint01.transform.position, Color.blue);
        Debug.DrawLine(borderPoint10.transform.position, borderPoint11.transform.position, Color.blue);
    }

    public void CalculateTransforms()
    {
        RaycastHit hit;

        objTrans.Clear();
        for (int i = 0; i < amountX; i++)
        {
            
            
            for (int j = 0; j < amountY; j++ )
            {

                Vector3 pos;

                float startOffsetX = (borderPoint01.transform.position.x - borderPoint00.transform.position.x) / amountY;
                float endOffsetX = (borderPoint11.transform.position.x - borderPoint10.transform.position.x) / amountY;

                distanceX = (borderPoint10.transform.position.x + endOffsetX * j) - (borderPoint00.transform.position.x + startOffsetX * j);
                float startOffsetY = (borderPoint10.transform.position.z - borderPoint00.transform.position.z) / amountX;
                float endOffsetY = (borderPoint11.transform.position.z - borderPoint01.transform.position.z) / amountX;

                distanceY = (borderPoint01.transform.position.z + endOffsetY * i) - (borderPoint00.transform.position.z + startOffsetY * i);
                float intervalX = distanceX * i  / (amountX-1) ;
                float intervalY = distanceY * j / (amountY-1)  ;

                pos = new Vector3(borderPoint00.transform.position.x + (startOffsetX*j) + intervalX + Random.Range(-offsetX,offsetX), borderPoint00.transform.position.y, borderPoint00.transform.position.z +(startOffsetY * i) + intervalY + Random.Range(-offsetY,offsetY));


                if (Physics.Raycast(  pos, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay( pos, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);

                    ObjTransform objTransform;
                    objTransform.position = hit.point;
                    
                    
                    objTransform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3 (hit.normal.x, hit.normal.y, hit.normal.z));
                    if (randomRotaion)
                    {
                        objTransform.rotation = Quaternion.Euler(objTransform.rotation.eulerAngles.x, objTransform.rotation.eulerAngles.y, objTransform.rotation.eulerAngles.z + Random.Range(0, 360));
                    }
                   
                    targetParent = hit.transform;
                    objTrans.Add(objTransform);
                    
                }


            }
        }


    }



    public void GenerateObjects()
    {
        foreach(ObjTransform ot in objTrans)
        {
            float randScale = Random.Range(randomScaleMin, randomScaleMax);
            GameObject currentObj;
            currentObj = Instantiate(prefab, ot.position, ot.rotation, targetParent);
            currentObj.transform.localScale = new Vector3(randScale, randScale, randScale);
            spawnObjects.Add(currentObj);
            
        }
    }

    public void ClearObjects()
    {
        foreach(GameObject obj in spawnObjects)
        {
            DestroyImmediate(obj);
        }
        spawnObjects.Clear();
    }

    public void BakeObjects()
    {
        spawnObjects.Clear();
    }

    
}
