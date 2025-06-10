using NUnit.Framework;

public class Player
{
    public int Health { get; private set; }

    public Player() => Health = 100;

    public void TakeDamage(int damage) => Health = System.Math.Max(Health - damage, 0);
}

public class PlayerTests
{
    [Test]
    public void TakeDamage_ReducesHealthCorrectly()
    {
        Player player = new Player();
        player.TakeDamage(40);
        Assert.AreEqual(60, player.Health);
    }
}