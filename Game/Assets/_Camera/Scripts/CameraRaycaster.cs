using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// FIXME: When two GameObject belonging to the same layer are overlapping the second one is ignored. Make sure the one closest to the camera is always the one kept in those cases.

namespace Game.Cameras {
   public class CameraRaycaster : MonoBehaviour {
      [SerializeField] private int[]  _layerPriorities;
      private float                   _maxRaycastDepth    = 100f;
      private RaycastHit?             _lastRaycastHit     = null;
      private int                     _lastLayerHit       = Layer.Default;

      public delegate void OnLayerChange(int newLayer); // When the cursor has moved over another layer
      public event OnLayerChange _notifyLayerChange;

      public delegate void OnPriorityLayerClicked(RaycastHit raycastHit, int layerHit); // Whenever the player click
      public event OnPriorityLayerClicked _notifyMouseClicked;

      // Assert that the layer hit is deferent than the last and (if true) notify the change to the observers
      void NotifyLayerChangeIfNew(int currentLayer) {
         if (currentLayer != _lastLayerHit) {
            _lastLayerHit = currentLayer;
            _notifyLayerChange(currentLayer);
         }
      }

      // Check if pointer is over an interractable UI element
      void SeekUIElement() {
         if (EventSystem.current.IsPointerOverGameObject()) {
            NotifyLayerChangeIfNew(Layer.UI);
            return;
         }
      }

      RaycastHit? FindTopPriorityHit(RaycastHit[] raycastHits) {
         List<int> layersHit = new List<int>();

         // Populate the list with the layers' number of hit colliders
         foreach (RaycastHit hit in raycastHits) {
            layersHit.Add(hit.collider.gameObject.layer);
         }

         // Step through the layers to find the one with top priority
         foreach (int layerInt in _layerPriorities) {
            foreach (RaycastHit hit in raycastHits) {
               if (hit.collider.gameObject.layer == layerInt) {
                  NotifyLayerChangeIfNew(layerInt);
                  return hit;
               }
            }
         }
         return null; // Did not find anything within the priority list
      }

      RaycastHit? SeekCurrentPriorityLayer() {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit[] raycastHits = Physics.RaycastAll(ray, _maxRaycastDepth);

         return FindTopPriorityHit(raycastHits);
      }

// -- Game loops

      void Update() {
         SeekUIElement();
         _lastRaycastHit = SeekCurrentPriorityLayer();
         if (Input.GetMouseButton(0)) {
            if (_lastRaycastHit != null) { _notifyMouseClicked(_lastRaycastHit.Value, _lastLayerHit); }
         }
      }
   }
}