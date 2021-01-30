﻿public class MovementSpeedUpgrade : FloatUpgrade
{
    public MovementSpeedUpgrade(PlayerController playerController, float upgradeValue) : base(playerController, upgradeValue)
    {
    }

    public override void ApplyUpgrade()
    {
        this.m_playerController.MovementSpeed += this.m_upgradeValue;
    }
}