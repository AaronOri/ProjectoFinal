import java.sql.Connection;
import java.sql.PreparedStatement;
import java.util.List;

public class GestorMySQL {
    public static void guardarJugadors(List<Jugador> jugadors) {
        try {
            Connection conn = ConnexioBDD.connectar();
            if (conn == null) {
                System.out.println("❌ Connexió fallida");
                return;
            }

            String sql = "INSERT INTO jugadors (username, score, timeSeconds, shotsFired, wins, losses) VALUES (?, ?, ?, ?, ?, ?)";
            PreparedStatement ps = conn.prepareStatement(sql);

            for (Jugador j : jugadors) {
                ps.setString(1, j.getUsername());
                ps.setInt(2, j.getScore());
                ps.setFloat(3, j.getTimeSeconds());
                ps.setInt(4, j.getShotsFired());
                ps.setInt(5, j.getWins());
                ps.setInt(6, j.getLosses());
                ps.executeUpdate();
            }

            System.out.println("✅ Jugadors desats correctament a la BDD.");
            conn.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

