CREATE DATABASE IF NOT EXISTS joc_1942_mayday_ironwings;
USE joc_1942_mayday_ironwings;

CREATE TABLE IF NOT EXISTS jugadors (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50),
    score INT,
    timeSeconds FLOAT,
    shotsFired INT,
    wins INT,
    losses INT
);
