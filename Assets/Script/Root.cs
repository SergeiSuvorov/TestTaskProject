using Controller;
using UnityEngine;

public sealed class Root : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private Transform _parentGameTransform;

    private void Start()
    {
        var mainController = new MainController(_placeForUi, _parentGameTransform); 
    }
}
