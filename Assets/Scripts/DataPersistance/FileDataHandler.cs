using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private string statedataDirPath = "";
    private string statedataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath,string dataFileName,string statedataDirPath, string statedataFileName,bool useEncryption){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
        this.statedataDirPath = statedataDirPath;
        this.statedataFileName = statedataFileName;
    }

    public GameData Load(){
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        GameData loadedData = null;

        if (File.Exists(fullPath)){
            try{
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open)){
                    using (StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                if (useEncryption){
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception)
            {
                Debug.LogError("Error");
            }
        }
        return loadedData;
    }

    public void Save(GameData data){
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption){
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)){
                using (StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception)
        {
            Debug.LogError("Error");
        }
    }

    public GameStateData LoadGameState(){
        string fullPath = Path.Combine(statedataDirPath,statedataFileName);

        GameStateData loadedData = null;

        if (File.Exists(fullPath)){
            try{
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open)){
                    using (StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                if (useEncryption){
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameStateData>(dataToLoad);
            }
            catch (Exception)
            {
                Debug.LogError("Error");
            }
        }
        return loadedData;
    }

    public void SaveGameState(GameStateData data){
        string fullPath = Path.Combine(statedataDirPath,statedataFileName);

        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption){
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)){
                using (StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception)
        {
            Debug.LogError("Error");
        }
    }

    private string EncryptDecrypt(string data){
        string modifiedData = "";
        for (int i=0;i<data.Length;i++){
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
}
