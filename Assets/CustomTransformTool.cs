using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class CustomTransformTool : Editor
{

    private bool grabbed = false;
    private bool axisX, axisY, axisZ;
    private float speed = .01f;
    private Vector2 currentMousePosition, mouseDelta;

    private GameObject selectedObject;
    void OnSceneGUI ()
    {
        
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    if (Event.current.keyCode == (KeyCode.G))
                    {
                        grabbed = true;
                        currentMousePosition = e.mousePosition;
                        selectedObject = Selection.gameObjects[0];

                        Tools.current = Tool.None;
                    }
                    if (Event.current.keyCode == (KeyCode.Escape))
                    {
                        grabbed = false;
                        
                    }

                    if (e.keyCode == KeyCode.X)
                    {
                        axisX = true;
                        axisY = false;
                        axisZ = false;
                        Tools.current = Tool.None;
                        

                    }
                    if (e.keyCode == KeyCode.Y)
                    {
                        axisX = false;
                        axisY = true;
                        axisZ = false;
                        
                    }
                    if (e.keyCode == KeyCode.Z)
                    {
                        axisX = false;
                        axisY = false;
                        axisZ = true;
                        Tools.current = Tool.None;
                    }
                    break;
                }
        }

        

        if (e.isMouse && grabbed)
        {
            mouseDelta = currentMousePosition - e.mousePosition;
            if (!axisX && !axisY && !axisZ)
            {
                selectedObject.transform.position = new Vector3(mouseDelta.x * speed, mouseDelta.y * speed, selectedObject.transform.position.z);
            }
            
             if (axisX)
            {
                
                Tools.current = Tool.None;
                selectedObject.transform.position = new Vector3(mouseDelta.x * speed, selectedObject.transform.position.y, selectedObject.transform.position.z);
            }
            else if (axisY)
            {
                Tools.current = Tool.None;
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, mouseDelta.y * speed, selectedObject.transform.position.z);
            }
            else if (axisZ)
            {
                Tools.current = Tool.None;
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, -mouseDelta.x * speed);
            }
        }

        if (Selection.count  == 0)
        {
            grabbed = false;
            axisX = axisY = axisZ = false;
        }

        if (e.keyCode == KeyCode.Mouse0 || e.keyCode == KeyCode.Mouse1)
        {
            grabbed = false;
        }

        if (axisX)
        {
            Handles.color = Color.red;
            Handles.DrawLine(selectedObject.transform.position + new Vector3(1000, 0, 0), selectedObject.transform.position + new Vector3(-1000, 0, 0));
        }
        if(axisY)
        {
            Handles.color = Color.green;
            Handles.DrawLine(selectedObject.transform.position + new Vector3(0, 1000, 0), selectedObject.transform.position + new Vector3(0, -1000, 0));
        }
        if(axisZ)
        {
            Handles.color = Color.blue;
            Handles.DrawLine(selectedObject.transform.position + new Vector3(0, 0, 1000), selectedObject.transform.position + new Vector3(0, 0, -1000));
        }

    }

    
}
