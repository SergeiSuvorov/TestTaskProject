using Interface;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class HealthPanelController : BaseController
    {
        private readonly HealthPanelView _view;
        private readonly IGetHealthBar _getHealthBarObject;     
        private readonly float healthPanelOffset = 0.6f;

        public readonly CompositeDisposable PanelDisposables;
        public readonly ReactiveCommand<HealthPanelController> NeedDeactivate;
        public GameObject PanelGameObject => _view.gameObject;

        public HealthPanelController(HealthPanelView view,Transform canvas, IGetHealthBar getHealthBarObject)
        {
            PanelDisposables = new CompositeDisposable();
            NeedDeactivate = new ReactiveCommand<HealthPanelController>();

            _view = view;
            _getHealthBarObject = getHealthBarObject;

            _getHealthBarObject.HealthValue.Subscribe(value => { _view.Slider.value = value; }).AddTo(PanelDisposables);
            _getHealthBarObject.OnDeathAction.Subscribe(value => { Deactivate(); }).AddTo(PanelDisposables);


            _view.NameText.text = _getHealthBarObject.Name;
            _view.transform.SetParent(canvas.transform, false);
            _view.Slider.maxValue = _getHealthBarObject.MaxHealth;
            _view.Slider.value = _getHealthBarObject.MaxHealth;
            canvas.GetComponent<HealthBarCanvasView>().AddToCanvas(_view);

            Observable.EveryUpdate().Subscribe(_ => { Update(); }).AddTo(PanelDisposables);        
        }

        private void Deactivate()
        {
            NeedDeactivate?.Execute(this);
        }

        private void Update()
        {

            if (_getHealthBarObject.isVisible != _view.gameObject.activeInHierarchy)
            {
                _view.gameObject.SetActive(_getHealthBarObject.isVisible);
            }

            Vector3 worldPos = new Vector3(_getHealthBarObject.Position.x, _getHealthBarObject.Position.y + healthPanelOffset, _getHealthBarObject.Position.z);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            _view.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

           
        }

        protected override void OnDispose()
        {
            
            PanelDisposables.Clear();
            base.OnDispose();
        }
    }
}