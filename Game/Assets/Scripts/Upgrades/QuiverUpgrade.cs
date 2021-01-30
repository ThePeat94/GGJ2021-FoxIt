﻿public class QuiverUpgrade : IntUpgrade
{

    public QuiverUpgrade(PlayerController playerController, int upgradeValue) : base(playerController, upgradeValue)
    {
    }

    public override void ApplyUpgrade()
    {
        this.m_playerController.QuiverController.IncreaseMaximum(this.m_upgradeValue);
    }
}