USE Joc_1942_MayDay_IronWings;

CREATE TABLE IF NOT EXISTS jugadors (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50),
    score INT,
    timeSeconds FLOAT,
    shotsFired INT,
    wins INT,
    losses INT
);
