using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FakeSaveManager : MonoBehaviour
{
	void Start () {
	    /*if (PlayerPrefs.HasKey("FakeScore"))
	    {
	        int savedScore = PlayerPrefs.GetInt("FakeScore");
	        print("We have a score from a different session " + savedScore);
	    }
	    else
	    {
	        PlayerPrefs.SetInt("FakeScore", 1337);
	        print("We don't have a score yet, so we're adding one in");
	    }*/
        SaveSerializer();
        LoadWithSerializer();
    }

    private void SaveSerializer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        print("Save location: " + Application.persistentDataPath);
        FileStream fileStream = File.Create(Application.persistentDataPath + "/save.dat");
        
        formatter.Serialize(fileStream, 1337);
        fileStream.Close();
    }

    private void LoadWithSerializer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream= File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

        print("serializer got back: " + formatter.Deserialize(fileStream));
        fileStream.Close();
    }
}
