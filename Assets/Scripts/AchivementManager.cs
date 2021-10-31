using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
	//싱글톤 패턴

	//업적 매니저임. 방학 때 꼭 제작

	public enum eEventType
	{
		MONSTER_DOWN,
		COLLECT_NEWITEM,
		ARRIVED_NEWMAP,
		LEVEL_UP
	}

	public void OnRefresh(eEventType type, params int[] datas)
	{
		switch (type)
		{
			case eEventType.MONSTER_DOWN:
				OnMonsterDown(datas);
				break;
			case eEventType.LEVEL_UP:
				if (datas.Length < 2)
				{
					Debug.LogError("파라미터 개수가 맞지 않습니다!");
					return;
				}
				OnLevelUp(datas[0], datas[1]);
				break;
		}
	}

	private void OnLevelUp(int prev, int curLv)
    {

    }

	private void OnMonsterDown(params int[] datas)
    {

    }
}
