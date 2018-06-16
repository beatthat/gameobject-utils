using BeatThat.Pools;
using BeatThat.TransformPathExt;
using UnityEngine;

namespace BeatThat.GameObjectUtil
{
    /// <summary>
    /// Utils for working with GameObjects, layers, flags, searchm etc.
    /// </summary>
    public static class GameObjectUtils
	{

		/// <summary>
		/// Very expensive version of GameObject.FindObjectsOfType
		/// that allows interface types.
		/// Use only in very controlled circumstances.
		/// </summary>
		public static T FindObjectOfType<T>() where T : class
		{
			foreach(Object o in Object.FindObjectsOfType(typeof(Component))) {
				var t = o as T;
				if(t != null) {
					return t;
				}
			}
			return null;
		}


		public static void SetHideFlagsRecursively(this GameObject go, HideFlags flags)
		{
			go.hideFlags = flags;
			foreach(Transform c in go.transform) {
				SetHideFlagsRecursively(c.gameObject, flags);
			}
		}

		// Recursively set the layer of this object and all its children
		public static void SetLayerRecursively(this GameObject go, int newLayer, bool includeInactive = false)
		{
			using(var tmp = ListPool<Transform>.Get()) {

				go.layer = newLayer;

				go.GetComponentsInChildren<Transform>(true, tmp);

				foreach(var c in tmp) {
					c.gameObject.layer = newLayer;
				}
			}
		}

		public static T FindByTag<T>(this GameObject caller, string tag) where T : Component
		{
			if(string.IsNullOrEmpty(tag)) {
				return null;
			}

			var go = GameObject.FindGameObjectWithTag(tag);
			if(go == null) {
				Debug.LogWarning("[" + Time.frameCount + "][" + caller.Path() + "] failed to find " + typeof(T).Name + " by tag '" + tag + "'");
				return null;
			}

			var c = go.GetComponent<T>();
			if(c == null) {
				Debug.LogWarning("[" + Time.frameCount + "][" + caller.Path() + "] object with tag '"
					+ tag + "' is missing " + typeof(T).Name + " component");
			}

			return c;
		}

		/// <summary>
		/// returns TRUE if a Transform is an ancestor (parent or beyond) of the caller.
		/// </summary>
		public static bool IsAncestorOf(this Transform t, Transform t2)
		{
			Transform p = t2;
			while((p = p.parent) != null) {
				if(p == t) {
					return true;
				}
			}
			return false;
		}
	}
}



