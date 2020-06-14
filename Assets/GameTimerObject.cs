using UnityEngine;

public class GameTimerObject : MonoBehaviour
{
    private void FixedUpdate() {
        _text.text = getTimeString(Time.time);
    }

    private string getTimeString(float inTime) {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(inTime);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
   }

    [SerializeField] private UnityEngine.UI.Text _text = null;
}
