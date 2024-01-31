using UnityEngine;

namespace Gameplay
{
	public class OutlineHandler: MonoBehaviour
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        protected Outline.Mode _mode = Outline.Mode.OutlineAll;
        private Color _color = Color.green;
        private const float _width = 6f;
        private Outline _outline = null;
        private Mesh _mesh = null;
        ///  PRIVATE METHODS           ///
        private void Start()
        {
            _mesh = GetComponent<MeshFilter>().sharedMesh;
        }
        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///
        protected void OutlineDisable()
        {
            if (!_outline)
            {
                return;
            }
            _outline.OutlineColor = Color.clear;
        }
        protected void OutlineEnable()
        {
            if (_mesh == null )
            {
                return;
            }
            if (!_outline)
            {
                _outline = gameObject.AddComponent<Outline>();
            }
            _outline.OutlineColor = _color;
            _outline.OutlineWidth = _width;
            _outline.OutlineMode = _mode;
            
        }

    }
}
