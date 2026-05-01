# Poststelle

`Poststelle` ist ein archiviertes und bereinigtes VB.NET-WinForms-Projekt fuer die Erfassung eingehender Sendungen mit SQLite.

Dieses Repository bewahrt den historischen Stand in einer Form, die wieder nachvollziehbar, umbenannt und fuer eine spaetere Modernisierung vorbereitet ist.

## Stand

- Technologie: VB.NET, WinForms, .NET Framework 4.6.1
- Datenbank: SQLite (`Poststelle.db`)
- Ziel: bestehende Desktop-Anwendung erhalten, entmarken und fuer spaetere Weiterentwicklung vorbereiten

## Was in dieser Fassung bereinigt wurde

- Projekt, Solution und Assembly auf `Poststelle` umbenannt
- alte Build-Artefakte (`bin`, `obj`, `*.user`) entfernt
- neue neutrale Logo- und Icon-Dateien ohne Alt-Branding erzeugt
- Git-Ignore fuer lokale Datenbanken und Build-Dateien hinzugefuegt
- Modernisierungsnotizen dokumentiert

## Projektstruktur

- [Poststelle.sln](/root/poststelle/Poststelle.sln)
- [Poststelle](/root/poststelle/Poststelle)
- [MIGRATION.md](/root/poststelle/MIGRATION.md)

## Build-Hinweise

Zum Bauen wird aktuell eine Windows-Umgebung mit Visual Studio 2017 oder neuer benoetigt, inklusive .NET Framework 4.6.1 Developer Pack.

NuGet-Abhaengigkeiten liegen im klassischen `packages`-Format vor. Vor dem ersten Build sollte eine Paket-Wiederherstellung ausgefuehrt werden.

## Linux-Status

Eine partielle Build-Pruefung unter Debian 12 mit Mono wurde getestet.

- `mono-devel` und `xbuild` lassen sich installieren
- Ressourcen, Projektdatei und Referenzen werden verarbeitet
- der eigentliche VB.NET-Compile-Schritt scheitert unter Debian-Mono, weil Mono hier keinen funktionierenden Visual-Basic-Compiler (`vbnc.exe`) mehr mitliefert

Das bedeutet: Linux ist fuer statische Analyse, Refactoring und Projektpflege brauchbar, aber fuer einen verlaesslichen Release-Build dieses alten VB.NET-WinForms-Projekts aktuell nicht die richtige Zielumgebung.

Wenn die temporaer installierte Linux-Build-Umgebung spaeter wieder entfernt werden soll:

```bash
apt-get remove -y mono-devel
apt-get autoremove -y
```

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

Kurz gesagt: jetzt sauber in .NET/VB.NET weiter aufraeumen, nicht sofort die Sprache wechseln. Eine C#-Migration ist sinnvoller als spaeterer Schritt, sobald Verhalten, Build und Datenzugriff stabilisiert sind.
