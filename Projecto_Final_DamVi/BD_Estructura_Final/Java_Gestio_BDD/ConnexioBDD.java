import java.sql.Connection;
import java.sql.DriverManager;

public class ConnexioBDD {
    public static Connection connectar() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");  // Solució error "No suitable driver"

            String url = "jdbc:mysql://mysql.dam.inspedralbes.cat:3306/joc_1942_mayday_ironwings";
            String usuari = "el_teu_usuari";
            String contrasenya = "la_teva_contrasenya";
            return DriverManager.getConnection(url, usuari, contrasenya);

        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}

