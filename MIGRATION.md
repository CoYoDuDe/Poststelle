# Modernisierungsplan

## Kurzbewertung

Die Anwendung ist klein genug, um weiter gepflegt zu werden, aber alt genug, dass eine Vollmigration in einem Schritt unnoetig riskant ist.

## Linux gegen Windows

Das Repository laesst sich unter Linux sinnvoll analysieren und teilweise bauen, aber nicht zuverlaessig fertig kompilieren:

- Debian 12 + `mono-devel` + `xbuild` verarbeitet Solution, Ressourcen und Referenzen
- der VB.NET-Kompilierschritt scheitert, weil in der aktuellen Mono-Distribution kein brauchbarer VB-Compiler mehr enthalten ist
- fuer echte Build-Freigaben und Designer-Arbeit bleibt daher Windows mit Visual Studio die richtige Umgebung

Fazit: Pflege und Refactoring koennen auf Linux weiterlaufen, finale Build-Pruefungen sollten aber unter Windows stattfinden.

## Empfohlene Reihenfolge

1. Eine reproduzierbare Windows-Build-Umgebung herstellen.
2. Das klassische `packages.config` in ein modernes Paketmanagement ueberfuehren.
3. Datenbankzugriffe zentralisieren und alle Schreib-/Lesezugriffe auf parametrisierte SQL-Commands umstellen.
4. `Option Strict On` vorbereiten und implizite Konvertierungen schrittweise entfernen.
5. UI und Datenzugriff trennen, damit spaetere Portierungen einfacher werden.
6. Danach entscheiden:
   - konservativ: VB.NET beibehalten, aber auf modernes Windows-.NET umstellen
   - offensiv: nach C# oder in eine neue Desktop-/Web-Anwendung migrieren

## Sprachentscheidung

Aktuell ist ein Sprachwechsel nicht der beste erste Schritt.

- VB.NET behalten:
  - geringstes Risiko
  - Designer-Dateien und Ereignislogik bleiben stabil
  - sinnvoll, solange das Ziel Erhalt und Bereinigung ist
- spaeter nach C# migrieren:
  - bessere Langzeitwartbarkeit
  - leichtere Entwicklerfindung
  - aber erst sinnvoll, wenn Build, Datenzugriff und Verhalten abgesichert sind

Empfehlung: jetzt bei .NET bleiben und den Altcode sauber machen; C# erst als zweite Phase angehen.

## Naechste konkrete Kandidaten

- `Form1.vb`: Hauptfenster, Filterlogik, Druckfunktion, direkte SQL-Verwendung
- `Form2.vb`, `Form3.vb`: Pflege- und Einstellungsdialoge
- `DataGridViewPrinter.vb`: Drucklayout und Branding

## Risiken

- Designer-Dateien reagieren empfindlich auf grosse manuelle Umbauten
- SQLite-Datei liegt lokal und wird direkt im Arbeitsverzeichnis erwartet
- Business-Regeln sind fast vollstaendig in Event-Handlern versteckt
