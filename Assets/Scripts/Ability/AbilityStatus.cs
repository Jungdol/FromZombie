public class AbilityStatus
{
    public AbilityKind abilityKind { get; }
    public string name { get; set; }
    public string description { get; set; }
    public string point { get; set; }

    public AbilityStatus()
    {
    }

    public AbilityStatus(AbilityKind _abilityKind, string _name, string _description, string _point)
    {
        this.abilityKind = _abilityKind;
        this.name = _name;
        this.description = _description;
        this.point = _point;
    }

    public AbilityStatus SetAbilityStatus(AbilityKind abilityKind)
    {
        AbilityStatus abilityStatus = null;

        switch (abilityKind)
        {
            case AbilityKind.A_1: // ����
                abilityStatus = new AbilityStatus(abilityKind, "ü��", "ü�� 10 ����", "1");
                break;
            case AbilityKind.A_2:
                abilityStatus = new AbilityStatus(abilityKind, "ü��", "�������� ���ϴ� ���ذ� 3% ����", "1");
                break;
            case AbilityKind.A_3:
                abilityStatus = new AbilityStatus(abilityKind, "ü��", "�����κ��� �޴� ���ذ� 4% ����", "2");
                break;
            case AbilityKind.B_1: // ü��
                abilityStatus = new AbilityStatus(abilityKind, "������", "������ ������(������)�� \n1 ����", "1");
                break;
            case AbilityKind.B_2:
                abilityStatus = new AbilityStatus(abilityKind, "������", "������ ������(������)�� \n2 ����", "1");
                break;
            case AbilityKind.B_3:
                abilityStatus = new AbilityStatus(abilityKind, "������", "������ ������(������)�� \n3 ����", "2");
                break;
            case AbilityKind.C_1: // ������
                abilityStatus = new AbilityStatus(abilityKind, "���", "����� 10 ����", "1");
                break;
            case AbilityKind.C_2:
                abilityStatus = new AbilityStatus(abilityKind, "���", "ȸ�� ��� 5 ����", "1");
                break;
            case AbilityKind.C_3:
                abilityStatus = new AbilityStatus(abilityKind, "���", "��ų ��� ��� ��뷮 5 ����", "2");
                break;
            case AbilityKind.D_1: // ���
                abilityStatus = new AbilityStatus(abilityKind, "����", "���� 3�� �޺�", "1");
                break;
            case AbilityKind.D_2:
                abilityStatus = new AbilityStatus(abilityKind, "����", "���ݷ� 2 ����", "2");
                break;
            case AbilityKind.D_3:
                abilityStatus = new AbilityStatus(abilityKind, "����", "���� ����", "3");
                break;
        }
        return abilityStatus;
    }
}
