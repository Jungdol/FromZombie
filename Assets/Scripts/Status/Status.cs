public class Status
{
    public UnitCode unitCode { get; } // �ٲ� �� ���� get��
    public string name { get; set; }
    public float maxHp { get; set; }
    public float nowHp { get; set; }
    public int atkDmg { get; set; }
    public float atkSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float atkRange { get; set; }
    public float fieldOfVision { get; set; }
    public float maxEnergy { get; set; }
    public float nowEnergy { get; set; }

    public Status()
    {
    }

    public float playerMaxHp = 100;
    public float playerMaxEnergy = 100;
    public int playerAtkDmg = 10;
    public float playerAtkSpeed = 1f;
    public float playerMoveSpeed = 5.0f;

    public Status(UnitCode unitCode, string name, float maxHp, int atkDmg, float atkSpeed, float moveSpeed, float atkRange, float fieldOfVision, float maxEnergy)
    {
        this.unitCode = unitCode;
        this.name = name;
        this.maxHp = maxHp;
        nowHp = maxHp;
        this.atkDmg = atkDmg;
        this.atkSpeed = atkSpeed;
        this.moveSpeed = moveSpeed;
        this.atkRange = atkRange;
        this.fieldOfVision = fieldOfVision;
        this.maxEnergy = maxEnergy;
        nowEnergy = maxEnergy;
    }

    public Status SetUnitStatus(UnitCode unitCode)
    {
        Status status = null;

        switch (unitCode) // �̸�, �ִ�hp, ���ݵ�����, ���� �ӵ�, �̵� �ӵ�, ���� ����(���͸�), �ν� ����(���͸�), �ִ� ���(�÷��̾)
        {
            case UnitCode.player:
                status = new Status(unitCode, "Player", 100, 10, 1f, 5f, 0, 0, 100);
                break;
            case UnitCode.enemy1:
                status = new Status(unitCode, "Enemy1", 50, 15, 1.5f, 2f, 1f, 5f, 0);
                break;
            case UnitCode.enemy2:
                status = new Status(unitCode, "Enemy2", 30, 9, 3f, 2f, 1.5f, 5f, 0);
                break;
            case UnitCode.enemy3:
                status = new Status(unitCode, "Enemy3", 30, 13, 3f, 2f, 1.5f, 5f, 0);
                break;
            case UnitCode.flyEnemy1:
                status = new Status(unitCode, "FlyEnemy1", 30, 13, 3f, 2f, 1.5f, 7f, 0);
                break;
            case UnitCode.flyEnemy2:
                status = new Status(unitCode, "FlyEnemy2", 30, 13, 3f, 2f, 3.5f, 7f, 0);
                break;
            case UnitCode.boss1:
                status = new Status(unitCode, "Boss1", 500, 13, 3f, 2f, 1.5f, 7f, 0);
                break;
            case UnitCode.boss2:
                status = new Status(unitCode, "Boss2", 300, 13, 3f, 2f, 1.5f, 7f, 0);
                break;
            case UnitCode.boss3:
                status = new Status(unitCode, "Boss3", 350, 13, 3f, 2f, 1.5f, 7f, 0);
                break;
        }
        return status;
    }
}
