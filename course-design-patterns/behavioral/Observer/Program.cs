using Observer;

Player player = new();

player.Attach(new GameOverScreen(player));
player.Attach(new ScoreUI(player));
player.Attach(new HealthBarUI(player));

player.SetState(health: 100, score: 0);
player.SetState(health: 100, score: 10);
player.SetState(health: 90, score: 20);
player.SetState(health: 80, score: 30);
player.SetState(health: 10, score: 25);
player.SetState(health: 0, score: 25);