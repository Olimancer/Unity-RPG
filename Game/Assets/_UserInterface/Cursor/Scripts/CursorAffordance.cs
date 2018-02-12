using UnityEngine;
using Game.Cameras;

public class CursorAffordance : MonoBehaviour {

   [SerializeField] private Texture2D  _handCursor    = null; 
   [SerializeField] private Texture2D  _enemyCursor   = null; 
   [SerializeField] private Texture2D  _errorCursor   = null; 
   [SerializeField] private Vector2    _cursorHotspot = new Vector2(0, 0);

   // TODO: Add option to change the cursor size dynamically (between 24 and 46 pixel)

   void Start () {
   //	Camera.main.GetComponent<CameraRaycaster>()._notifyLayerChange += OnLayerChange;
   }

   // Called everytime the cursor switch over another layer
   void OnLayerChange(int layerHit) {
      // Update is called once per frame
      switch(layerHit) {
         case (Layer.Enemy):
            Cursor.SetCursor(_enemyCursor, _cursorHotspot, CursorMode.ForceSoftware);
            break;
         case (Layer.Default):
         case (Layer.Walkable):
            Cursor.SetCursor(_handCursor, _cursorHotspot, CursorMode.ForceSoftware);
            break;
         default:
            Cursor.SetCursor(_errorCursor, _cursorHotspot, CursorMode.ForceSoftware);
            return;
      }
   }
}

