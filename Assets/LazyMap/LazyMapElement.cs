using UnityEngine;

public abstract class ILazyMapElement : MonoBehaviour
{
    abstract protected LazyMap.RectZone getLazyMapZoneImpl();
    virtual protected void onEnterToLazyMapFocusZone() { }
    virtual protected void onExitFromLazyMapFocusZone() { }
    public bool isInFocusZone => _isInFocusZone;

    #region ImplementationDetails
    internal LazyMap.RectZone getLazyMapZone() { return getLazyMapZoneImpl(); }
    protected void Start() { register(); }
    protected void OnDestroy() { unregister(); }
    private void register() { manager.registerElement(this); }
    private void unregister() { manager?.unregisterElement(this); }
    virtual internal void processEnterToLazyMapFocusZone() {
        _isInFocusZone = true;
        onEnterToLazyMapFocusZone();
    }
    virtual internal void processExitFromLazyMapFocusZone() {
        _isInFocusZone = false;
        onExitFromLazyMapFocusZone();
    }
    #endregion

    private LazyMapManager manager => Object.FindObjectOfType<LazyMapManager>();

    //Fields
    private bool _isInFocusZone = false;
}
