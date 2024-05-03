using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Coroutine _coroutine;
    private int _count;

    private void Awake()
    {
        _text.SetText($"Press mouse button to start");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ToggleCoroutine();
        }
    }

    private void UpdateTextOnCanvas()
    {
        _text.SetText($"Count = {_count.ToString()}");
    }

    private void ToggleCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        else
        {
            _coroutine = StartCoroutine(Count());
        }
    }

    private IEnumerator Count(float delay = 0.5f)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _count += 1;
            UpdateTextOnCanvas();

            yield return wait;
        }
    }
}
