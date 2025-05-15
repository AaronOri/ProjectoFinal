import org.bson.BsonBinaryWriter;
import org.bson.Document;
import org.bson.io.BasicOutputBuffer;
import org.bson.codecs.DocumentCodec;
import org.bson.codecs.EncoderContext;

import java.io.FileOutputStream;
import java.io.OutputStream;

public class GeneradorBson {
    public static void escriureBson(Jugador j, String nomFitxer) {
        try {
            Document doc = new Document("username", j.getUsername())
                    .append("score", j.getScore())
                    .append("timeSeconds", j.getTimeSeconds())
                    .append("shotsFired", j.getShotsFired())
                    .append("wins", j.getWins())
                    .append("losses", j.getLosses());

            BasicOutputBuffer buffer = new BasicOutputBuffer();
            BsonBinaryWriter writer = new BsonBinaryWriter(buffer);
            new DocumentCodec().encode(writer, doc, EncoderContext.builder().isEncodingCollectibleDocument(true).build());
            writer.flush();

            try (OutputStream os = new FileOutputStream(nomFitxer)) {
                os.write(buffer.toByteArray());
                System.out.println("✔ BSON creat: " + nomFitxer);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

