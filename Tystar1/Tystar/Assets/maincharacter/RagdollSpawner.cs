using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform ragdollprefab;
    [SerializeField] private Transform originalRootBone;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //êÿÇËë÷Ç¶ LeftMouseButton
        {
            RagdollSetup ragdollSetup = Instantiate(
                ragdollprefab,
                transform.position,
                transform.rotation).GetComponent<RagdollSetup>();
            ragdollSetup.setup(originalRootBone);
            Destroy(gameObject);

        }
    }
}