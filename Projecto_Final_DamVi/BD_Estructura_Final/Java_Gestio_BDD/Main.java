import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        Jugador j = LectorJson.llegirJugador("jugador.json");

        if (j == null) {
            System.out.println("⚠ No s'ha pogut llegir el fitxer JSON.");
            return;
        }

        ArrayList<Jugador> jugadors = new ArrayList<>();
        jugadors.add(j);

        GestorMySQL.guardarJugadors(jugadors);
        GeneradorBson.escriureBson(j, "Jugadors.bson");
    }
}

