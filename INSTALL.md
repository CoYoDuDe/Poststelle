# Installation

## Download

1. Das aktuelle ZIP aus den GitHub Releases herunterladen.
2. Optional die Datei `SHA256SUMS.txt` herunterladen und die Pruefsumme vergleichen.
3. Das ZIP in einen beliebigen Ordner entpacken.

## Start

1. `Poststelle.exe` starten.
2. Falls Windows SmartScreen warnt:
   - `Weitere Informationen` anklicken
   - `Trotzdem ausfuehren` waehlen

## Warum erscheint die Warnung?

Die Anwendung ist nicht digital signiert. Deshalb zeigt Windows den Herausgeber als unbekannt an. Das ist bei kleinen Open-Source- oder Privatprojekten ohne kostenpflichtiges Code-Signing-Zertifikat normal.

## Datenbank

Die Anwendung verwendet standardmaessig:

- `%LOCALAPPDATA%\Poststelle\Poststelle.db`

Beim ersten Start wird die Datenbank dort angelegt, falls sie noch nicht existiert.
