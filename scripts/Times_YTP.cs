using System;
using UnityEngine;

namespace MoreYTPs
{
	// Token: 0x02000006 RID: 6
	public class ITM_TimesYTP : Item
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002E1C File Offset: 0x0000101C
		public override bool Use(PlayerManager pm)
		{
			int pointsThisLevel = Singleton<CoreGameManager>.Instance.GetPointsThisLevel(pm.playerNumber);
			Singleton<CoreGameManager>.Instance.AddPoints(pointsThisLevel * this.multiplier, pm.playerNumber, true);
			UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private int multiplier = 2;
	}
}
