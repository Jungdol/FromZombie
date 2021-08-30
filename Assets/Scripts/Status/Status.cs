public class Status
{
    public UnitCode unitCode { get; } // �ٲ� �� ���� get��
    public string name { get; set; }
    public int maxHp { get; set; }
    public int nowHp { get; set; }
    public int atkDmg { get; set; }
    public float atkSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float atkRange { get; set; }
    public float fieldOfVision { get; set; }

    public Status()
    {
    }

    public int playerMaxHp = 50;
    public int playerAtkDmg = 10;
    public float playerAtkSpeed = 1f;
    public float playerMoveSpeed = 5.0f;

    public Status(UnitCode unitCode, string name, int maxHp, int atkDmg, float atkSpeed, float moveSpeed, float atkRange, float fieldOfVision)
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
    }

    public Status SetUnitStatus(UnitCode unitCode)
    {
        Status status = null;

        switch (unitCode)
        {
            case UnitCode.player:
                status = new Status(unitCode, "Player", 100, 10, 1f, 5f, 0, 0);
                break;
            case UnitCode.enemy1:
                status = new Status(unitCode, "Enemy1", 50, 15, 1.5f, 2f, 1.5f, 7f);
                break;
            case UnitCode.enemy2:
                status = new Status(unitCode, "Enemy2", 30, 9, 3f, 2f, 1.5f, 7f);
                break;
            case UnitCode.enemy3:
                status = new Status(unitCode, "Enemy3", 30, 13, 3f, 2f, 1.5f, 7f);
                break;
            case UnitCode.flyEnemy1:
                status = new Status(unitCode, "flyEnemy1", 30, 13, 3f, 2f, 1.5f, 7f);
                break;
            case UnitCode.flyEnemy2:
                status = new Status(unitCode, "flyEnemy2", 30, 13, 3f, 2f, 1.5f, 7f);
                break;
            case UnitCode.boss1:
                status = new Status(unitCode, "boss1", 500, 13, 3f, 2f, 1.5f, 7f);
                break;
            case UnitCode.boss2:
                status = new Status(unitCode, "boss2", 300, 13, 3f, 2f, 1.5f, 7f);
                break;
            case UnitCode.boss3:
                status = new Status(unitCode, "boss3", 350, 13, 3f, 2f, 1.5f, 7f);
                break;
        }
        return status;
    }
}
