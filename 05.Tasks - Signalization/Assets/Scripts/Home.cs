using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Home : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            _alarm.Activate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            _alarm.Deactivate();
    }
}
