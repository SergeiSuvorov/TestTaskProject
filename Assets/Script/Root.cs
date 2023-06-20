using Controller;
using UnityEngine;

public sealed class Root : MonoBehaviour
{
    [SerializeField]  private Transform _placeForUi;

    private void Start()
    {
        var mainController = new MainController(_placeForUi); 
    }
}
