using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [Header("Objetos que orbitarán")]
    public List<GameObject> objetos = new List<GameObject>();

    [Header("Centro de la órbita")]
    public Transform centro;

    [Header("Distancia de la órbita")]
    public float distancia = 3f;

    [Header("Velocidad de órbita (grados por segundo)")]
    public float velocidad = 50f;

    private List<float> angulos;

    public bool active = false;

    private void Start()
    {
        centro = SpatialBridge.actorService.localActor.avatar.GetAvatarBoneTransform(HumanBodyBones.Hips);
        // Inicializamos un ángulo para cada objeto
        angulos = new List<float>();
        for (int i = 0; i < objetos.Count; i++)
        {
            angulos.Add((360f / objetos.Count) * i); // reparto uniforme
        }
    }

    private void Update()
    {
        if (active)
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                if (objetos[i] == null) continue;

                // Incrementar el ángulo
                angulos[i] += velocidad * Time.deltaTime;

                // Convertir ángulo a radianes
                float rad = angulos[i] * Mathf.Deg2Rad;

                // Posición en círculo
                Vector3 nuevaPos = new Vector3(
                    Mathf.Cos(rad) * distancia,
                    0,
                    Mathf.Sin(rad) * distancia
                );

                // Actualizar posición del objeto
                objetos[i].transform.position = centro.position + nuevaPos;
            }
        }
        
    }

    public void Activate()
    {
        active = true;
    }
}
