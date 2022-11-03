using UnityEngine;

public class RecordingScreenManager : MonoBehaviour
{
    string gameObjectName = "UnityReceiveMessage";
    string methodName = "CurrentStatus";

    private void start()
    {
        
    }
    public void StartRecording()
    {
        //BrainCheck.ScreenRecorderBridge.SetUnityGameObjectNameAndMethodName(gameObjectName, methodName);
        //BrainCheck.ScreenRecorderBridge.startRecording();
    }

    public void StopRecording()
    {
        //BrainCheck.ScreenRecorderBridge.SetUnityGameObjectNameAndMethodName(gameObjectName, methodName);
        //BrainCheck.ScreenRecorderBridge.stopRecording();
    }

    public void SaveRecordings()
    {
        //BrainCheck.ScreenRecorderBridge.SetUnityGameObjectNameAndMethodName(gameObjectName, methodName);
        //BrainCheck.ScreenRecorderBridge.shareRecording();
    }
}
