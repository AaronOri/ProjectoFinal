import com.google.gson.Gson;
import java.io.FileReader;

public class LectorJson {
    public static Jugador llegirJugador(String nomFitxer) {
        Gson gson = new Gson();
        try {
            return gson.fromJson(new FileReader(nomFitxer), Jugador.class);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}

