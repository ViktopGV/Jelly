using System;
using System.Collections;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public event Action Ate;
    [SerializeField] private float eatenMoveSpeed = 5;
    [SerializeField] private float eatenScaleSpeed = 5;

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Eaten eaten) && eaten.IsAlive)
        {
            float playerXZ = new Vector3(_collider.bounds.size.x, 0, _collider.bounds.size.z).magnitude;
            float objXZ = new Vector3(other.bounds.size.x, 0, other.bounds.size.z).magnitude;
            if (playerXZ >= objXZ)
            {
                transform.localScale += eaten.VectorValue;
                eaten.Eat();
                StartCoroutine(Eat(other.transform));
                Ate?.Invoke();
                //Destroy(other.gameObject);
                //other.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator Eat(Transform eaten)
    {
        while (true)
        {
            // Calculate the direction to move
            if (eaten == null)
                yield break;
            Vector3 moveDirection = transform.position - eaten.position;

            // Move the object towards the center object
            eaten.position += moveDirection.normalized * eatenMoveSpeed * Time.deltaTime;

            // Shrink the object gradually
            eaten.localScale = Vector3.Lerp(eaten.localScale, Vector3.zero, eatenScaleSpeed * Time.deltaTime);

            // Check if the object is very small or at the center
            if (eaten.localScale.magnitude < .1f || eaten.position == transform.position)
            {
                // Destroy the object and exit the Coroutine
                Destroy(eaten.gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
