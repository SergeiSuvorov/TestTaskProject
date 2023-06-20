using Interface;
using Tools;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class HealthBarManagerController : BaseController
    {
        private readonly ViewServices _viewServices;
        private readonly Transform _placeForUi;
        private readonly string _healthBarPoolId = "HealthBar";

        public HealthBarManagerController(ViewServices viewServices, Transform placeForUi)
        {
            _viewServices = viewServices;
            _placeForUi = placeForUi;
        }

        private HealthPanelView GetHealthPanelView()
        {
            var getView = _viewServices.Create(ResourcePathConstants.HealthBarResourcePath, _healthBarPoolId).TryGetComponent(out HealthPanelView view);
            if (getView)
                return view;
            else
                return null;
        }

        public void SetHealthBarToObject(IGetHealthBar getHealthBarObject)
        {
            var view = GetHealthPanelView();
            var controller = new HealthPanelController(view, _placeForUi, getHealthBarObject);

            controller.NeedDeactivate.Subscribe(value => { DestroyHealthPanelController(value); }).AddTo(controller.PanelDisposables);
            AddController(controller);
        }

        private void DestroyHealthPanelController(HealthPanelController controller)
        {
            _viewServices.Destroy(controller.PanelGameObject, _healthBarPoolId);
            DestroyController(controller);
        }

    }
}