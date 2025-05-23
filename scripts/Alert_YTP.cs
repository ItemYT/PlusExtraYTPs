using System;
using UnityEngine;

namespace MoreYTPs
{
	// Token: 0x02000009 RID: 9
	public class ITM_AlertYTP : Item
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000211F File Offset: 0x0000031F
		public override bool Use(PlayerManager pm)
		{
			pm.ec.MakeNoise(pm.transform.position, 100);
			Singleton<CoreGameManager>.Instance.AddPoints(this.value, pm.playerNumber, true);
			UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private int value = 30;
	}
}
