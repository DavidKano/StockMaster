using UnityEngine;
using UnityEngine.UI;

public class ConfirmarEliminarUI : MonoBehaviour
{
    public Button btnConfirmar;
    public Button btnCancelar;

    private System.Action onConfirm;

    public void Mostrar(System.Action callback)
    {
        onConfirm = callback;
        gameObject.SetActive(true);
    }

    public void Ocultar()
    {
        gameObject.SetActive(false);
        onConfirm = null;
    }

    void Start()
    {
        btnCancelar.onClick.AddListener(Ocultar);
        btnConfirmar.onClick.AddListener(() =>
        {
            onConfirm?.Invoke();
            Ocultar();
        });
    }
}
