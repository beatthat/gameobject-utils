using UnityEngine;

namespace BeatThat
{
	/// <summary>
	/// Sets some remote GameObject active/inactive along with self
	/// </summary>
	public class SetActive : MonoBehaviour
	{
		public GameObject m_target;
		public bool m_onDisableSetInactive = true;

		void OnEnable()
		{
			m_target.SetActive(true);
		}

		void OnDisable()
		{
			if(m_onDisableSetInactive) {
				m_target.SetActive(false);
			}
		}

	}
}
