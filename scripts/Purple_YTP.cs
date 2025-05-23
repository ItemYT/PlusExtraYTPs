using System;
using UnityEngine;

namespace MoreYTPs
{
	// Token: 0x02000008 RID: 8
	public class ITM_PurpleYTP : Item
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002E98 File Offset: 0x00001098
		public override bool Use(PlayerManager pm)
		{
			Singleton<CoreGameManager>.Instance.AddPoints(this.value, pm.playerNumber, true);
			UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private int value = 40;
	}
}
