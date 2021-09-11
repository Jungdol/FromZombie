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
            case AbilityKind.A_1: // 공격
                abilityStatus = new AbilityStatus(abilityKind, "체력", "체력 10 증가", "1");
                break;
            case AbilityKind.A_2:
                abilityStatus = new AbilityStatus(abilityKind, "체력", "좀비한테 당하는 피해가 3% 감소", "1");
                break;
            case AbilityKind.A_3:
                abilityStatus = new AbilityStatus(abilityKind, "체력", "보스로부터 받는 피해가 4% 감소", "2");
                break;
            case AbilityKind.B_1: // 체력
                abilityStatus = new AbilityStatus(abilityKind, "내구도", "무기의 내구도(유지력)이 \n1 증가", "1");
                break;
            case AbilityKind.B_2:
                abilityStatus = new AbilityStatus(abilityKind, "내구도", "무기의 내구도(유지력)이 \n2 증가", "1");
                break;
            case AbilityKind.B_3:
                abilityStatus = new AbilityStatus(abilityKind, "내구도", "무기의 내구도(유지력)이 \n3 증가", "2");
                break;
            case AbilityKind.C_1: // 내구도
                abilityStatus = new AbilityStatus(abilityKind, "기력", "기력이 10 증가", "1");
                break;
            case AbilityKind.C_2:
                abilityStatus = new AbilityStatus(abilityKind, "기력", "회피 기력 5 감소", "1");
                break;
            case AbilityKind.C_3:
                abilityStatus = new AbilityStatus(abilityKind, "기력", "스킬 사용 기력 사용량 5 감소", "2");
                break;
            case AbilityKind.D_1: // 기력
                abilityStatus = new AbilityStatus(abilityKind, "공격", "공중 3단 콤보", "1");
                break;
            case AbilityKind.D_2:
                abilityStatus = new AbilityStatus(abilityKind, "공격", "공격력 2 증가", "2");
                break;
            case AbilityKind.D_3:
                abilityStatus = new AbilityStatus(abilityKind, "공격", "돌진 공격", "3");
                break;
        }
        return abilityStatus;
    }
}
