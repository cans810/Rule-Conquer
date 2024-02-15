using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;

    public static DataPersistanceManager instance {get; private set;}

    private void Awake(){
        if (instance != null){
            Debug.LogError("Error");
        }
        instance = this;
    }

    private void Start(){
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
    }

    public void NewGame(){
        this.gameData = new GameData();
    }

    public void LoadGame(){

    }

    public void SaveGame(){

    }

    private List<IDataPersistance> FindAllDataPersistanceObjects(){
        IEnumerable<IDataPersistance> dataPersistanceObjects = 
        FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

}
