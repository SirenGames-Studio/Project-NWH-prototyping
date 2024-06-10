using SGS.Saving;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        Debug.Log("CaptureState");
       return new SerializableTransform(this.transform.position, this.transform.eulerAngles);  
    }

    public void RestoreState(object state)
    {
        SerializableTransform serializableTransform = (SerializableTransform)state;
        SerializableVector3 newPosition= serializableTransform.Position;
        SerializableVector3 newRotation= serializableTransform.Rotation;
        this.transform.position = newPosition.GetVector3();
       this.transform.eulerAngles = newRotation.GetVector3();
       Debug.Log("RestoreState" + transform.position +" " + transform.rotation);
    }
}
