using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Coroutine _coroutine;
    private int _count;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ToggleCoroutine();
        }

        UpdateTextOnCanvas();        
    }

    private void UpdateTextOnCanvas()
    {
        if (_coroutine != null)
        {
            _text.SetText($"Counting - {_count.ToString()}");
        }
        else
        {
            _text.SetText($"Paused - {_count.ToString()}");
        }
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
        while (true)
        {   
            yield return new WaitForSeconds(delay);
            _count += 1;
        }
    }
}
