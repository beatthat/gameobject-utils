using UnityEngine;

namespace BeatThat.GameObjectUtil
{
    /// <summary>
    /// Sets some remote GameObject inactive in OnEnable
    /// </summary>
    public class SetInactive : MonoBehaviour
	{
		public GameObject m_target;

		void OnEnable()
		{
			m_target.SetActive(false);
		}


	}
}

