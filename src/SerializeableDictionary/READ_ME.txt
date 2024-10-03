SerializableDictionaryPropertyDrawer.cd und UserSerializableDictionaryPropertyDrawers.cs in einen Editor Ordner

Neue Kombination aus Key Value muss als Subklasse von SerializableDictionary erstellt werden:
[System.Serializable]
public class StringStringDict : SerializableDictionary<String, String> { }

Neuer Custom PropertyDrawer in  UserSerializableDictionaryPropertyDrawers.cs:
[CustomPropertyDrawer(typeof(StringStringDict))]
