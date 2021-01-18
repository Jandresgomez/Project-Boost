using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVectorRange = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        this.startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Mathf.Epsilon - period) <= Mathf.Epsilon) { return; }

        float currentCycleStep = Time.time / period;
        float rawSin = Mathf.Sin(2 * Mathf.PI * currentCycleStep);
        float offset = (rawSin / 2) + 0.5f;
        this.transform.position = this.startingPosition + offset * movementVectorRange;
    }
}
