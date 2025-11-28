using UnityEngine;
using SpatialSys.UnitySDK;

public class PlacementZone : MonoBehaviour
{
    [Header("Configuración de la zona")]
    public ObjectType.Type expectedType = ObjectType.Type.None;

    // Si quieres diferenciar fase 1 (decoración en stand) y fase 2 (meter en caja),
    // puedes usar este flag para comportamiento específico (por ejemplo, play sound).
    public bool isPhase2 = false;

    // Si quieres destruir el objeto original agarrado en vez de devolverlo, usa esto.
    public bool destroyOriginalOnSuccess = true;

    // Llamar desde el SpatialInteractable de la zona (evento OnSelect/OnUse)
    public void TryPlace()
    {
        if (PickObject.Instance == null)
        {
            Debug.LogWarning("No hay instancia de PickObject en la escena.");
            return;
        }

        GameObject currentObjectPicked = PickObject.Instance.GetCurrentObject();
        if (currentObjectPicked == null)
        {
            Debug.Log("No hay objeto agarrado para colocar.");
            return;
        }

        ObjectType typeCurrentObject = currentObjectPicked.GetComponent<ObjectType>();
        if (typeCurrentObject == null)
        {
            Debug.LogWarning("El objeto agarrado no tiene ObjectType.");
            return;
        }

        // Si el tipo coincide -> success
        if (typeCurrentObject.type == expectedType)
        {
            OnCorrectPlacement(currentObjectPicked, typeCurrentObject);
        }
        else
        {
            OnWrongPlacement(currentObjectPicked, typeCurrentObject);
        }
    }

    private void OnCorrectPlacement(GameObject currentObjectPicked, ObjectType typeCurrentObject)
    {
        typeCurrentObject.ActivateOriginal();
        if (PickObject.Instance != null)
        {
            PickObject.Instance.Release();
        }
    }

    private void OnWrongPlacement(GameObject held, ObjectType ot)
    {
        Debug.Log($"Objeto {held.name} NO corresponde a esta zona. Esperado: {expectedType}, encontrado: {ot.type}.");

        // Rechazo: devolvemos el objeto al estado original
        if (ot != null)
        {
            ot.ResetTransform();
        }

        // además nos aseguramos que PickObject borre su referencia
        if (PickObject.Instance != null)
        {
            PickObject.Instance.Release();
        }

        // opcional: reproducir feedback de error (sonido / efecto)
    }
}