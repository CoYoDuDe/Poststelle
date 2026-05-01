# Poststelle Classic

`Poststelle Classic` ist ein archiviertes und bereinigtes VB.NET-WinForms-Projekt fuer die Erfassung eingehender Sendungen mit SQLite.

Dieses Repository bewahrt den historischen Stand in einer Form, die wieder nachvollziehbar, umbenannt und fuer eine spaetere Modernisierung vorbereitet ist.

## Stand

- Technologie: VB.NET, WinForms, .NET Framework 4.6.1
- Datenbank: SQLite (`Poststelle.db`)
- Ziel: bestehende Desktop-Anwendung erhalten, entmarken und fuer spaetere Weiterentwicklung vorbereiten

## Was in dieser Fassung bereinigt wurde

- Projekt, Solution und Assembly auf `Poststelle` umbenannt
- alte Build-Artefakte (`bin`, `obj`, `*.user`) entfernt
- neue neutrale Logo- und Icon-Dateien ohne `Logwin`-Branding erzeugt
- Git-Ignore fuer lokale Datenbanken und Build-Dateien hinzugefuegt
- Modernisierungsnotizen dokumentiert

## Projektstruktur

- [Poststelle.sln](/root/poststelle-classic/Poststelle.sln)
- [Poststelle](/root/poststelle-classic/Poststelle)
- [MIGRATION.md](/root/poststelle-classic/MIGRATION.md)

## Build-Hinweise

Zum Bauen wird aktuell eine Windows-Umgebung mit Visual Studio 2017 oder neuer benoetigt, inklusive .NET Framework 4.6.1 Developer Pack.

NuGet-Abhaengigkeiten liegen im klassischen `packages`-Format vor. Vor dem ersten Build sollte eine Paket-Wiederherstellung ausgefuehrt werden.

## Bekannte technische Schulden

- Windows-only durch klassisches WinForms auf .NET Framework
- viele SQL-Statements sind als Stringverkettung implementiert
- `Option Strict Off` erschwert sichere Refactorings
- keine automatisierten Tests
- keine Build-Umgebung im aktuellen Export enthalten

## Empfehlung

Fuer eine spaetere echte Modernisierung ist der sinnvollste naechste Schritt keine Vollmigration in eine andere Sprache auf Verdacht, sondern zuerst:

1. Build unter Windows wiederherstellen
2. Datenbankzugriffe kapseln und parameterisieren
3. das Projekt auf ein modernes .NET-Windows-Target heben
4. erst danach ueber C#-Migration oder komplette Neuentwicklung entscheiden
