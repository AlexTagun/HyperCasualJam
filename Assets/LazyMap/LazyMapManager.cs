using System.Collections.Generic;
using UnityEngine;

public class LazyMapManager : MonoBehaviour
{
    public void setFocusZone(LazyMap.RectZone inZone) {
        _focusZone = inZone;

        elementsFocusZoneUpdate();
    }

    private void FixedUpdate() {
        elementsRegistryUpdate();
        elementsFocusZoneUpdate();

        drawUpdate();
    }

    private void elementsRegistryUpdate() {
        foreach (ILazyMapElement theElementForDeferredRegister in _elementsForDeferredRegister)
            _elements.Add(theElementForDeferredRegister);

        foreach (ILazyMapElement theElementForDeferredUnregister in _elementsForDeferredUnregister)
            _elements.Remove(theElementForDeferredUnregister);

        _elementsForDeferredRegister.Clear();
        _elementsForDeferredUnregister.Clear();
    }

    private List<ILazyMapElement> __regNewElementsInFocusZone = new List<ILazyMapElement>();
    private void elementsFocusZoneUpdate() {
        foreach (ILazyMapElement theElement in _elements) {
            if (theElement.getLazyMapZone().isCollide(_focusZone))
                __regNewElementsInFocusZone.Add(theElement);
        }

        foreach (ILazyMapElement theNewElementInFocusZone in __regNewElementsInFocusZone)
            if (!_elementsInFocusZone.Contains(theNewElementInFocusZone))
                theNewElementInFocusZone.processEnterToLazyMapFocusZone();

        foreach (ILazyMapElement theElementInFocusZone in _elementsInFocusZone)
            if (!__regNewElementsInFocusZone.Contains(theElementInFocusZone))
                theElementInFocusZone.processExitFromLazyMapFocusZone();

        _elementsInFocusZone.Clear();
        _elementsInFocusZone.InsertRange(0, __regNewElementsInFocusZone);

        __regNewElementsInFocusZone.Clear();
    }

    private void drawUpdate() {
        foreach (ILazyMapElement theElement in _elements) {
            Color theDrawColor = getDrawColorForElement(theElement);
            theElement.getLazyMapZone().draw(theDrawColor);
        }

        _focusZone.draw(getColorForFocusZone());
    }

    private Color getDrawColorForElement(ILazyMapElement inElement) {
        return inElement.isInFocusZone ? Color.green : Color.black;
    }

    private Color getColorForFocusZone() { return Color.red; }

    internal void registerElement(ILazyMapElement inElement) {
        _elementsForDeferredRegister.Add(inElement);
    }

    internal void unregisterElement(ILazyMapElement inElement) {
        _elementsForDeferredUnregister.Add(inElement);
    }

    //Fields
    private List<ILazyMapElement> _elements = new List<ILazyMapElement>();
    private List<ILazyMapElement> _elementsForDeferredRegister = new List<ILazyMapElement>();
    private List<ILazyMapElement> _elementsForDeferredUnregister = new List<ILazyMapElement>();

    private List<ILazyMapElement> _elementsInFocusZone = new List<ILazyMapElement>();
    private LazyMap.RectZone _focusZone = new LazyMap.RectZone();
}
