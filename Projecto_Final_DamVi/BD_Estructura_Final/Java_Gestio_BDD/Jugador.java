public class Jugador {
    private String username;
    private int score;
    private float timeSeconds;
    private int shotsFired;
    private int wins;
    private int losses;

    public Jugador(String username, int score, float timeSeconds, int shotsFired, int wins, int losses) {
        this.username = username;
        this.score = score;
        this.timeSeconds = timeSeconds;
        this.shotsFired = shotsFired;
        this.wins = wins;
        this.losses = losses;
    }

    public String getUsername() { return username; }
    public int getScore() { return score; }
    public float getTimeSeconds() { return timeSeconds; }
    public int getShotsFired() { return shotsFired; }
    public int getWins() { return wins; }
    public int getLosses() { return losses; }
}
