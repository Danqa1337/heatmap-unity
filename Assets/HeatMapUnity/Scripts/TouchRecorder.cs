using UnityEngine;

public class TouchRecorder : MonoBehaviour
{
    [SerializeField]
    private string dataPath;
    [SerializeField]
    private string eventName;
    [SerializeField]
    private bool createFileIfNonFound;
    private IEventWriter eventWriter;
    private bool _recording;

    void Awake()
    {
        eventWriter = new JSONEventWriter(Application.dataPath + dataPath, createFileIfNonFound);
    }
    private void Update()
    {
        if (_recording)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0)
            {
                var input = Input.mousePosition;
                if(Input.touches.Length > 0)
                {
                    input = (Vector3)Input.touches[0].position;
                }

                BaseEvent baseEvent = new BaseEvent(eventName, new Vector3(input.x, 0, input.y) / 100f);
                eventWriter.SaveEvent(baseEvent);
            } 
        }
    }
    public void StopRecording()
    {
        _recording = false;
    }
    public void ContinueRecording()
    {
        _recording = true;
    }
}
