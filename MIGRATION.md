# Modernisierungsplan

## Kurzbewertung

Die Anwendung ist klein genug, um weiter gepflegt zu werden, aber alt genug, dass eine Vollmigration in einem Schritt unnoetig riskant ist.

## Empfohlene Reihenfolge

1. Eine reproduzierbare Windows-Build-Umgebung herstellen.
2. Das klassische `packages.config` in ein modernes Paketmanagement ueberfuehren.
3. Datenbankzugriffe zentralisieren und alle Schreib-/Lesezugriffe auf parametrisierte SQL-Commands umstellen.
4. `Option Strict On` vorbereiten und implizite Konvertierungen schrittweise entfernen.
5. UI und Datenzugriff trennen, damit spaetere Portierungen einfacher werden.
6. Danach entscheiden:
   - konservativ: VB.NET beibehalten, aber auf modernes Windows-.NET umstellen
   - offensiv: nach C# oder in eine neue Desktop-/Web-Anwendung migrieren

## Naechste konkrete Kandidaten

- `Form1.vb`: Hauptfenster, Filterlogik, Druckfunktion, direkte SQL-Verwendung
- `Form2.vb`, `Form3.vb`: Pflege- und Einstellungsdialoge
- `DataGridViewPrinter.vb`: Drucklayout und Branding

## Risiken

- Designer-Dateien reagieren empfindlich auf grosse manuelle Umbauten
- SQLite-Datei liegt lokal und wird direkt im Arbeitsverzeichnis erwartet
- Business-Regeln sind fast vollstaendig in Event-Handlern versteckt
