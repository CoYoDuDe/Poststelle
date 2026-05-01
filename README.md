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
- SQLite-Zugriffe in gemeinsame Repositories ausgelagert
- Formularlogik in Services fuer Filter, Validierung und Datensatz-Erzeugung aufgeteilt
- Windows-Build mit aktuellem Refactoring erneut erfolgreich geprueft

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
- weiterhin alte Designer- und Event-Strukturen aus WinForms-Zeiten
- `Option Strict Off` erschwert sichere Refactorings
- keine automatisierten Tests
- klassisches `packages.config` statt modernem SDK-/PackageReference-Stil
- keine Build-Umgebung im aktuellen Export enthalten

## Aktueller Modernisierungsstand

Die Anwendung ist wieder in einem brauchbaren Zwischenzustand:

- Datenzugriffe liegen nicht mehr verstreut in `Form1`, `Form2` und `Form3`, sondern in gemeinsamen Repository-Klassen
- wiederholte UI- und Filterlogik wurde in `ApplicationServices.vb` zentralisiert
- der Windows-Build laeuft nach dem Refactoring wieder durch

Das ist noch keine komplette Neuentwicklung, aber ein sinnvoller Zustand fuer weitere Pflege, Releases und spaetere Portierung.

## Empfehlung

Fuer eine spaetere echte Modernisierung ist der sinnvollste naechste Schritt keine Vollmigration in eine andere Sprache auf Verdacht, sondern zuerst:

1. Build unter Windows wiederherstellen
2. Datenbankzugriffe kapseln und parameterisieren
3. `Option Strict` vorbereiten und implizite Konvertierungen abbauen
4. das Projekt auf ein modernes .NET-Windows-Target heben
5. erst danach ueber C#-Migration oder komplette Neuentwicklung entscheiden

Kurz gesagt: jetzt sauber in .NET/VB.NET weiter aufraeumen, nicht sofort die Sprache wechseln. Eine C#-Migration ist sinnvoller als spaeterer Schritt, sobald Verhalten, Build und Datenzugriff stabilisiert sind.
