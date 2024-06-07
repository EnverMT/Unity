public static class AnimatorParams
{
    public static class Attack
    {
        public const string IsAttacking = nameof(IsAttacking);
        public const string Attacking = nameof(Attacking);
    }

    public static class Movement
    {
        public const string HorizontalSpeed = nameof(HorizontalSpeed);
    }

    public static class Jump
    {
        public const string VerticalSpeed = nameof(VerticalSpeed);
        public const string OnGround = nameof(OnGround);
    }
}