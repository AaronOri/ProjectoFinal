public class TestConnexio {
    public static void main(String[] args) {
        if (ConnexioBDD.connectar() != null) {
            System.out.println("✅ Connexió correcte a la base de dades.");
        } else {
            System.out.println("❌ No s'ha pogut connectar.");
        }
    }
}

