using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
	//�̱��� ����

	//���� �Ŵ�����. ���� �� �� ����

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
					Debug.LogError("�Ķ���� ������ ���� �ʽ��ϴ�!");
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
