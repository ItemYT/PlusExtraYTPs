using System;
using UnityEngine;

namespace MoreYTPs
{
	// Token: 0x0200000A RID: 10
	public class ITM_PlusOrMinusYTP : Item
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002ED0 File Offset: 0x000010D0
		public override bool Use(PlayerManager pm)
		{
			this.value = UnityEngine.Random.RandomRange(Mathf.FloorToInt(-25f), Mathf.FloorToInt(25f));
			Singleton<CoreGameManager>.Instance.AddPoints(this.value, pm.playerNumber, true);
			UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private int value;
	}
}
