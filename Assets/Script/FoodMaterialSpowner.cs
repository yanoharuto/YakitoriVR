using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterialSpowner : MonoBehaviour
{
    [SerializeField] GameObject FoodMaterialPrefab;
    [SerializeField] float teleportX;
    Vector3 TeleportPosition;
    float interval = 0;
    float delta = 0;

    private void Start()
    {
        TeleportPosition = new Vector3(teleportX, 7f, -0.8f);
    }
    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.interval)
        {
            this.delta = 0;
            this.interval = Random.Range(1.0f, 3.0f);
            GameObject Food = Instantiate(FoodMaterialPrefab) as GameObject;
            Food.transform.position = TeleportPosition;
        }
    }
}