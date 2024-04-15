using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public string filePath;

    // Start is called before the first frame update
    void Awake()
    {
        filePath = Application.dataPath + filePath;
        string jsonContent = File.ReadAllText(filePath);

        ClientData data = JsonUtility.FromJson<ClientData>(jsonContent);

        //Debug.Log("Client Name: " + data.clientName);
        //Debug.Log("Age: " + data.age);
    }
}

