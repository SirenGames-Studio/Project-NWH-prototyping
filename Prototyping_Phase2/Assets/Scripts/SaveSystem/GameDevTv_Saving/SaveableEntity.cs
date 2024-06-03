using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SaveableEntity : MonoBehaviour
{

   [SerializeField] string uniqueIdentifier = "";

   public string GetUniqueIdentifier()
   {
       return uniqueIdentifier;
   }

   public object CaptureState()
   {
       //return new SerializableVector3(transform.position);;
        print("Capturing state for " + GetUniqueIdentifier());
        return null;

   }

   public void RestoreState(object state)
   {
    //    SerializableVector3 position = (SerializableVector3)state;
    //    transform.position = position.GetVector3();

    print ("Restoring state for " + GetUniqueIdentifier()); 
   }

   private void Update()
  {
        if (Application.IsPlaying(gameObject)) return;

        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier)");
        if (property.stringValue == "")
        {
            property.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
        }
   }
}
