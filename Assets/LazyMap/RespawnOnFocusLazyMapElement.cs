using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class RespawnOnFocusLazyMapElement : ILazyMapElement
{
    override protected LazyMap.RectZone getLazyMapZoneImpl() {
        Vector2 thePosition2D = transform.position;
        return new LazyMap.RectZone(thePosition2D + _offset, _zoneSize);
    }

    override protected void onEnterToLazyMapFocusZone() {
        if (null != _instantiatedObject)
            throw (new System.Exception("Object was instantiated before focusing"));

        _instantiatedObject = Instantiate(_objectToInstantiate);
    }
    
    override protected void onExitFromLazyMapFocusZone() {
        if (null == _instantiatedObject)
            throw (new System.Exception("Object wasn't instantiated after going out of focus"));

        Destroy(_instantiatedObject);
    }

    private void FixedUpdate() {
        updateInstantiatedObjectTransform();
    }

    private void updateInstantiatedObjectTransform() {
        if (null != _instantiatedObject)
            setObject2DTransformFromTransform(_instantiatedObject, transform);
    }

    new private void OnDestroy() {
        base.OnDestroy();
        processDestroyInEditor();
    }

    static private void setObject2DTransformFromTransform(GameObject inObject, Transform inTransform) {
        Vector3 theOldInstantiatedObjectPosition = inObject.transform.position;
        Vector3 thePosition = inTransform.position;
        inObject.transform.position = new Vector3(thePosition.x, thePosition.y, theOldInstantiatedObjectPosition.z);
        inObject.transform.rotation = inTransform.rotation;
    }

 #region EditorFeatures
    private void OnValidate() {
        float kMinSize = 0.01f;
        _zoneSize.x = Mathf.Clamp(_zoneSize.x, kMinSize, float.MaxValue);
        _zoneSize.y = Mathf.Clamp(_zoneSize.y, kMinSize, float.MaxValue);
    }

    private void Update() {
        if (!Application.isPlaying) {
            updateInEditorPreviewSpawn();
            updateInstantiatedObjectTransform();
            updateDraw();
        }
    }

    private void updateInEditorPreviewSpawn() {
        updateInEditorPreviewClearing();
        updateInEditorPreviewSpawning();
        updateInEditorPreviewEditorState();
    }

    private void updateInEditorPreviewClearing() {
        bool theObjectToInstantiateChanged = (_objectToInstantiate != _editorPreview_oldObjectToInstantiate);
        bool theInstantiatedObjectShouldBeRemoved = (!_showPreview || theObjectToInstantiateChanged);
        if (theInstantiatedObjectShouldBeRemoved) {
            DestroyImmediate(_instantiatedObject);
            _instantiatedObject = null;
        }
    }

    private void updateInEditorPreviewSpawning() {
        if (null != _objectToInstantiate && null == _instantiatedObject && _showPreview) {
            _instantiatedObject = (GameObject)PrefabUtility.InstantiatePrefab(_objectToInstantiate);
            _instantiatedObject.hideFlags = _instantiatedObject.hideFlags |
                    HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy | HideFlags.NotEditable;
        }
    }

    private void updateInEditorPreviewEditorState() {
        _editorPreview_oldObjectToInstantiate = _objectToInstantiate;
    }

    private void updateDraw() {
        getLazyMapZoneImpl().draw(Color.white);
    }

    private void processDestroyInEditor() {
        base.OnDestroy();
        if (null != _instantiatedObject)
            DestroyImmediate(_instantiatedObject);
    }
#endregion

    //Fields
    [SerializeField] private GameObject _objectToInstantiate = null;
    private GameObject _instantiatedObject = null;

    [SerializeField] private Vector2 _zoneSize = Vector2.one;
    [SerializeField] private Vector2 _offset = Vector2.zero;

#region EditorFeatures
    [SerializeField] private bool _showPreview = false;
    [SerializeField, HideInInspector] private GameObject _editorPreview_oldObjectToInstantiate = null;
 #endregion
}
