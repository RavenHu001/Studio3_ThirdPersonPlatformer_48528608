using UnityEngine;

public class CoinModel: MonoBehaviour
{
    [SerializeField] private float roateRate = 90f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * roateRate * Time.deltaTime);
    }
}
