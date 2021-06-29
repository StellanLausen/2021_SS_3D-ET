using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/*
SaveSystem:		Änderungen vorbehalten. Siehe in "Projekt Datei" für genaue Zeilenangaben.
Name Arbeit:	youtube Video Titel: 	SAVE & LOAD SYSTEM in Unity
Name Autor:		youtube Channel name: 	Brackeys
URL:			https://www.youtube.com/watch?v=XOjd_qU2Ido&t=376s	
Abrufdatum:		22.06.2021
Lizenzmodel:	-
 */
public static class SaveSystem
{
    private static readonly string Path = Application.persistentDataPath + "/records.data";
    //source SaveSystem start -source code changed
    public static void SaveRecords(GameManager gameManager)
    {
        var formatter = new BinaryFormatter();
        var fileStream = new FileStream(Path, FileMode.Create);

        var data = new RecordsData(gameManager);
        
        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static RecordsData LoadRecords()
    {
        if (File.Exists(Path))
        {
            var formatter = new BinaryFormatter();  
            var fileStream = new FileStream(Path, FileMode.Open);
            
            var data = formatter.Deserialize(fileStream) as RecordsData;
            fileStream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Load Records failed from Path " + Path);
            return null;
        }
    }
    //source SaveSystem end
}
