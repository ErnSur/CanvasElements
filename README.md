Collection of small useful classes.

### `Container` and `PoolContainer`
A serializable object that holds a prefab and a `Transform` reference, implements `IList<T>`.
`AddNew` method creates a new instance of prefab inside the transform.
Because of `IList<T>`, it functions as a collection with additional behavior of easy object instantiation.
`PoolContainer` has the same interface but uses object pooling.
Cuts much of the boilerplate code with UI scripting. 

### `GameObjectPool`
Object pool pattern implementation with Unity specific API.
* Serializable, can be configured from editor.
* Prototype as prefab.
* Configurable transform parent.

### `CanvasElement`
A very simple interface that forces the user to initialize UI components in a certain way.
