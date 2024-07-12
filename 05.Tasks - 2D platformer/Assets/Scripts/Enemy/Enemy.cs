//[RequireComponent(typeof(Patrol))]
public class Enemy : BaseUnit
{
    public Patrol Patrol;

    protected override void Awake()
    {
        base.Awake();

        Patrol = GetComponent<Patrol>();
    }
}
