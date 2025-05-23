using System;
using UnityEngine;

namespace MoreYTPs
{
	// Token: 0x02000007 RID: 7
	public class ITM_DiamondYTP : Item
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002E60 File Offset: 0x00001060
		public override bool Use(PlayerManager pm)
		{
			Singleton<CoreGameManager>.Instance.AddPoints(this.value, pm.playerNumber, true);
			UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private int value = 125;
	}
}
