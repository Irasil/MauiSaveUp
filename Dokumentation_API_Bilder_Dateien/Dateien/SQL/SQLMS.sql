DROP Database SaveUp;

CREATE DATABASE SaveUp;

Use SaveUp;

CREATE TABLE Nahrung (
  id INT identity PRIMARY KEY,
  ArtikelName VARCHAR(50),
  Betrag DECIMAL(8,2),
  Kategorie Varchar(20),
  Datum DATE
);

-- Nahrung Tabelle
INSERT INTO Nahrung (ArtikelName, Betrag, Kategorie, Datum)
VALUES
    ('Milch', 10.50, 'Nahrung', '2023-03-18'),
    ('Brot', 20.00, 'Nahrung', '2023-03-19'),
    ('Eier', 15.75, 'Nahrung', '2023-02-20'),
    ('Käse', 5.99, 'Nahrung', '2023-02-21'),
    ('Kino', 50.00, 'Ausgang', '2023-03-18'),
    ('Restaurant', 30.00, 'Ausgang', '2023-03-19'),
    ('Bar', 25.00, 'Ausgang', '2023-02-20'),
    ('Konzert', 15.00, 'Ausgang', '2023-02-21'),
    ('Smartphone', 100.00, 'Elektronik', '2023-03-18'),
    ('Laptop', 200.00, 'Elektronik', '2023-03-19'),
    ('Fernseher', 150.00, 'Elektronik', '2023-02-20'),
    ('Kopfhörer', 75.00, 'Elektronik', '2023-02-21');
