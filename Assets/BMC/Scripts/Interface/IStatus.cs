public interface IStatus
{
    public float HP { get; set; }
    public bool IsDead { get; set; }
    public float MoveSpeed { get; set; }
    public float Damage { get; set; }

    public void TakeDamage(float damage);
}