using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Coroutine _coroutine;
    private int _count;

    private void Start()
    {
        _text.SetText("Click mouse button to start");
        ToggleCoroutine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ToggleCoroutine();
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
            _text.SetText(_count.ToString());
            _count++;
            yield return new WaitForSeconds(delay);
        }
    }
}
