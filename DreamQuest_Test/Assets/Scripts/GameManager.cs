using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameObject cubeObject;
    
    [HideInInspector]
    public GameObject tempCube;
    private bool isDraggingCube;
    private Vector3 initializedPos;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();// resume game if it has a save file
    }

    // Update is called once per frame
    void Update()
    {
        //dargging a cube
        if (Input.GetMouseButton(0) && isDraggingCube)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.collider.name == "Ground")
                {
                    Vector3 pos = new Vector3(hit.point.x, initializedPos.y, hit.point.z);
                    isDraggingCube = true;
                    tempCube.GetComponent<Rigidbody>().MovePosition(pos);
                }
                else if (hit.collider.tag == "CubeObject")
                {
                    if (hit.collider.name != tempCube.name && hit.normal.y > 0)
                    {
                        //put a cube on the top of the other cube while dragging
                        tempCube.GetComponent<Rigidbody>().MovePosition(new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z));
                        
                    }
                    tempCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                tempCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        //place a cube
        else if (Input.GetMouseButtonUp(0) && isDraggingCube)
        {
           
            if (tempCube.name.Contains("red"))
            {
                UIManager.Instance.decreaseCounter(0);
            }
            else if (tempCube.name.Contains("green"))
            {
                UIManager.Instance.decreaseCounter(1);
            }
            else if (tempCube.name.Contains("blue"))
            {
                UIManager.Instance.decreaseCounter(2);
            }
            GameObject cube = GameObject.Instantiate(tempCube);
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            cube.GetComponent<Rigidbody>().useGravity = true;
            isDraggingCube = false;
            tempCube = null;
            AudioManager.Instance.PlayPlaceSound();

        }
        //gather a cube
        else if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.collider.tag == "CubeObject")
                {
                    if (hit.collider.name.Contains("red"))
                    {
                        UIManager.Instance.increaseCounter(0);
                    }
                    else if (hit.collider.name.Contains("green"))
                    {
                        UIManager.Instance.increaseCounter(1);
                    }
                    else if (hit.collider.name.Contains("blue"))
                    {
                        UIManager.Instance.increaseCounter(2);
                    }
                    Destroy(hit.collider.gameObject);
                    AudioManager.Instance.PlayGatherSound();
                }
            }
        }
        //save and quit game
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SaveGame();
            Application.Quit();
        }
    }
   
    public void CreateCube(int colorCode)
    {
        isDraggingCube = true;
        tempCube = GameObject.Instantiate(cubeObject);
        initializedPos = tempCube.transform.position;
        
        Debug.Log("color code: " + colorCode);
        switch (colorCode)
        {
            case 0:
                tempCube.GetComponent<Renderer>().material.color = Color.red;
                tempCube.name = "cube red" + ++count;
                break;
            case 1:
                tempCube.GetComponent<Renderer>().material.color = Color.green;
                tempCube.name = "cube green" + ++count;
                break;
            case 2:
                tempCube.GetComponent<Renderer>().material.color = Color.blue;
                tempCube.name = "cube blue" + ++count;
                break;
        }
    }
    public void CancelCreatingCube()
    {
        if (tempCube != null)
        {
            Destroy(tempCube);
        }
    }
    //save a game file using XML
    public void SaveGame()
    {
        XmlDocument xmlDocument = new XmlDocument();
        XmlElement root = xmlDocument.CreateElement("DreamQuestConfig");
        root.SetAttribute("version", "1.0");
        XmlElement elementCubeObjects = xmlDocument.CreateElement("CubeObjects");
        GameObject[] cubeObjects = GameObject.FindGameObjectsWithTag("CubeObject");
        for (int i = 0; i < cubeObjects.Length; i++)
        {
            XmlElement elementCubeObject = xmlDocument.CreateElement("CubeObject");
            elementCubeObject.SetAttribute("Name", cubeObjects[i].name);
            elementCubeObject.SetAttribute("PosX", cubeObjects[i].transform.position.x.ToString());
            elementCubeObject.SetAttribute("PosY", cubeObjects[i].transform.position.y.ToString());
            elementCubeObject.SetAttribute("PosZ", cubeObjects[i].transform.position.z.ToString());

            elementCubeObject.SetAttribute("RotX", cubeObjects[i].transform.rotation.eulerAngles.x.ToString());
            elementCubeObject.SetAttribute("RotY", cubeObjects[i].transform.rotation.eulerAngles.y.ToString());
            elementCubeObject.SetAttribute("RotZ", cubeObjects[i].transform.rotation.eulerAngles.z.ToString());
            elementCubeObjects.AppendChild(elementCubeObject);
        }
        root.AppendChild(elementCubeObjects);
        xmlDocument.AppendChild(root);
        xmlDocument.Save("DreamQuestConfig.txt");
          
    }
    //Load the game file
    public void LoadGame()
    {
        
        if (File.Exists("DreamQuestConfig.txt"))
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("DreamQuestConfig.txt");
            XmlNodeList cubeObjects = xmlDocument.GetElementsByTagName("CubeObject");
            Debug.Log(cubeObjects.Count);
            for (int i = 0; i < cubeObjects.Count; i++)
            {
                string name = cubeObjects[i].Attributes[0].InnerText;
                float posX = float.Parse(cubeObjects[i].Attributes[1].InnerText);
                float posY = float.Parse(cubeObjects[i].Attributes[2].InnerText);
                float posZ = float.Parse(cubeObjects[i].Attributes[3].InnerText);
                float rotX = float.Parse(cubeObjects[i].Attributes[4].InnerText);
                float rotY = float.Parse(cubeObjects[i].Attributes[5].InnerText);
                float rotZ = float.Parse(cubeObjects[i].Attributes[6].InnerText);
            
                Vector3 pos = new Vector3(posX, posY,posZ);
                Quaternion quar = Quaternion.Euler(rotX,rotY,rotZ);
                GameObject obj = GameObject.Instantiate(cubeObject,pos,quar);
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                obj.name = name;
                
                if (obj.name.Contains("red"))
                {
                    obj.GetComponent<Renderer>().material.color = Color.red;
                    UIManager.Instance.decreaseCounter(0);
                }
                else if (obj.name.Contains("green"))
                {
                    obj.GetComponent<Renderer>().material.color = Color.green;
                    UIManager.Instance.decreaseCounter(1);
                }
                else if (obj.name.Contains("blue"))
                {
                    obj.GetComponent<Renderer>().material.color = Color.blue;
                    UIManager.Instance.decreaseCounter(2);
                }
            }
        }
    }
}
