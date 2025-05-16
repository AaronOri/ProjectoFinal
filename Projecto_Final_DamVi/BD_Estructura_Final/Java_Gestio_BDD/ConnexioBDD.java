import java.sql.Connection;
import java.sql.DriverManager;

public class ConnexioBDD {
    public static Connection connectar() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");  // Solució error "No suitable driver"

            String url = "jdbc:mysql://dam.inspedralbes.cat/Joc_1942_MayDay_IronWings";
            String usuari = "Joc_1942_root";
            String contrasenya = "P@ssw0rd";
            return DriverManager.getConnection(url, usuari, contrasenya);

        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}

