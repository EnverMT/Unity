using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

#nullable enable
    public delegate void CountHandler();
    public event CountHandler? CountChanged;
#nullable disable

    private Coroutine _coroutine;
    private int _count;

    private void Awake()
    {
        _text.SetText($"Press mouse button to start");
    }

    private void OnEnable()
    {
        CountChanged += UpdateTextOnCanvas;
    }

    private void OnDisable()
    {
        if (_coroutine != null) ToggleCoroutine();
        CountChanged -= UpdateTextOnCanvas;
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

        while (true)
        {
            _count += 1;
            CountChanged?.Invoke();

            yield return wait;
        }
    }
}
